using System;

namespace CroquetScores.TestHelpers.Services
{
    public static class WebSite
    {
        public static void StartIfNotRunning(string webserverScheme = "https", int webserverPort = 44301, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
        {
            WebServer.StartIfNotRunning(webserverScheme, webserverPort, maximumWaitTimeForProcessToRespond);
        }
    }
}