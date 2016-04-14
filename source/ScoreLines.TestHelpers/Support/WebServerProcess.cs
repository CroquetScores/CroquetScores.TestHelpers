using System;
using System.Diagnostics;
using System.IO;

namespace Scorelines.TestHelpers.Support
{
    internal class WebServerProcess : LocalHttpProcess
    {
        internal WebServerProcess(string scheme, int port)
            : this(GetExecutable(), Solution.IISExpressConfigFile, scheme, port)
        {
        }

        internal WebServerProcess(FileInfo executable, FileInfo iisExpressConfigFile, string scheme, int port)
            : base(scheme, port, GetProcessStartInfo(executable, iisExpressConfigFile))
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
