using System.Collections.Concurrent;
using CroquetScores.TestHelpers.Support;

namespace CroquetScores.TestHelpers.Services
{
    public class RavenServer
    {
        private static readonly ConcurrentDictionary<int, HttpProcess> Servers = new ConcurrentDictionary<int, HttpProcess>();

        public static bool IsRunning(int port)
        {
            return GetOrAddServer(port).IsRunning();
        }

        public static void Start(int port)
        {
            GetOrAddServer(port).Start();
        }

        public static void StartIfNotRunning(int port)
        {
            GetOrAddServer(port).StartIfNotRunning();
        }

        private static RavenServerProcess GetOrAddServer(int port)
        {
            return (RavenServerProcess)Servers.GetOrAdd(port, p => new RavenServerProcess(port));
        }
    }
}