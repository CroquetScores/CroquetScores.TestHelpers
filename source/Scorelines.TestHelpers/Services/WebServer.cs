using System;
using System.Collections.Concurrent;
using Scorelines.TestHelpers.Support;

namespace Scorelines.TestHelpers.Services
{
    internal class WebServer
    {
        private static readonly ConcurrentDictionary<int, HttpProcess> Servers = new ConcurrentDictionary<int, HttpProcess>();

        internal static void StartIfNotRunning(string scheme, int port, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
        {
            Servers.GetOrAdd(port, p => new WebServerProcess(scheme, port, maximumWaitTimeForProcessToRespond).StartIfNotRunning());
        }
    }
}
