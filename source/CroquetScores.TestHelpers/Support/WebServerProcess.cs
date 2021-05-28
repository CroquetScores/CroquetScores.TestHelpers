using System;
using System.Diagnostics;
using System.IO;

namespace CroquetScores.TestHelpers.Support
{
    internal class WebServerProcess : LocalHttpProcess
    {
        internal WebServerProcess(string scheme, int port, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
            : this(GetExecutable(), Solution.IISExpressConfigFile, scheme, port, maximumWaitTimeForProcessToRespond)
        {
        }

        internal WebServerProcess(FileInfo executable, FileInfo iisExpressConfigFile, string scheme, int port, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
            : base(scheme, port, GetProcessStartInfo(executable, iisExpressConfigFile), maximumWaitTimeForProcessToRespond)
        {
        }

        private static ProcessStartInfo GetProcessStartInfo(FileInfo executable, FileInfo iisExpressConfigFile)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = executable.FullName,
                // String.Format("/path:\"{0}\" /port:{1} /clr:v4.0 /systray:true", projectDirectory.FullName, port)
                Arguments = $"/systray:true /config:\"{iisExpressConfigFile.FullName}\""
            };

            return startInfo;
        }

        private static FileInfo GetExecutable()
        {
            var executable = new FileInfo(@"C:\Program Files (x86)\IIS Express\iisexpress.exe");

            if (executable.Exists)
            {
                return executable;
            }

            throw new FileNotFoundException("Cannot find WebServer executable.", executable.FullName);
        }
    }
}