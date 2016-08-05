using System;
using System.Net.Http;

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
            catch (HttpRequestException)
            {
                return false;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerExceptions.Count == 1 && aggregateException.InnerException.GetType() == typeof(HttpRequestException))
                {
                    return false;
                }
                throw;
            }
        }
    }
}