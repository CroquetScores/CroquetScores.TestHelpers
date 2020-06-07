using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Scorelines.TestHelpers.Support
{
    internal class RavenServerProcess : LocalHttpProcess
    {
        internal RavenServerProcess(int port, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
            : this(GetExecutable(), Projects.CroquetScoresWeb.Directory, port, maximumWaitTimeForProcessToRespond)
        {
        }

        internal RavenServerProcess(FileInfo executable, DirectoryInfo projectDirectory, int port, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
            : base(port, GetProcessStartInfo(executable, projectDirectory, port), maximumWaitTimeForProcessToRespond)
        {
        }

        private static ProcessStartInfo GetProcessStartInfo(FileInfo executable, DirectoryInfo projectDirectory, int port)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = executable.FullName,
                Arguments = string.Format("--ram --set=raven/port=={0}", port)
            };

            return startInfo;
        }

        private static FileInfo GetExecutable()
        {
            var possibleExecutables = new[]
            {
                new FileInfo(@"C:\Program Files (No Install)\RavenDB 3.5\Server\Raven.Server.exe"),
                new FileInfo(@"C:\RavenDB\Raven.Server.exe")
            };

            var executable = possibleExecutables.SingleOrDefault(f => f.Exists);

            if (executable != null)
            {
                return executable;
            }

            throw new FileNotFoundException(string.Format(
                "Cannot find RavenDB Server executable.{0}Looked for:{0}{1}.",
                Environment.NewLine,
                string.Join(Environment.NewLine, possibleExecutables.Select(f => string.Format("- {0}", f.FullName)))));
        }
    }
}