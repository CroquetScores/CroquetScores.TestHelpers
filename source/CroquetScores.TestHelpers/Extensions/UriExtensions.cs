using System;
using System.Linq;
using System.Net.Http;

namespace CroquetScores.TestHelpers.Extensions
{
    internal static class UriExtensions
    {
        private static HttpResponseMessage GetResponse(this Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                return httpClient.GetAsync(uri).Result;
            }
        }

        internal static bool IsResponding(this Uri uri)
        {
            try
            {
                using (uri.GetResponse())
                {
                    return true;
                }
            }
            catch (HttpRequestException exception)
            {
                return HandleHttpRequestException(uri, exception);
            }
            catch (AggregateException aggregateException)
            {
                HttpRequestException exception = null;

                if (aggregateException.InnerExceptions.Any())
                {
                    exception = aggregateException.InnerException as HttpRequestException;
                }

                if (exception == null)
                {
                    throw;
                }
                return HandleHttpRequestException(uri, exception);
            }
        }

        private static bool HandleHttpRequestException(Uri uri, HttpRequestException exception)
        {
            if (IsCertificateException(exception))
            {
                return true;
            }

            return false;
        }

        private static bool IsCertificateException(HttpRequestException exception)
        {
            return exception.InnerException.Message == "The underlying connection was closed: Could not establish trust relationship for the SSL/TLS secure channel.";
        }
    }
}