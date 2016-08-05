using System;
using System.Diagnostics;

namespace Scorelines.TestHelpers.Services
{
    public static class WebSite
    {
        public static void StartIfNotRunning(string webserverScheme = "https", int webserverPort = 44301, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
        {
            WindowsAzureStorageEmulator.StartIfNotRunning();
            RavenServer.StartIfNotRunning(8080);
            WebServer.StartIfNotRunning(webserverScheme, webserverPort, maximumWaitTimeForProcessToRespond);
        }
    }
}
