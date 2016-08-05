using System;
using System.Net.Http;
using Anotar.NLog;

namespace Scorelines.TestHelpers.Extensions
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
                LogTo.InfoException($"{uri} is not responding. {exception.Message}", exception);
                return false;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerExceptions.Count != 1 || aggregateException.InnerException.GetType() != typeof(HttpRequestException))
                {
                    throw;
                }

                LogTo.InfoException($"{uri} is not responding. {aggregateException.InnerException.Message}", aggregateException.InnerException);
                return false;
            }
        }
    }
}