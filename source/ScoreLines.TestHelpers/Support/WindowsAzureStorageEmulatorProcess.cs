using System;
using System.Diagnostics;
using System.IO;

namespace Scorelines.TestHelpers.Support
{
    internal class WindowsAzureStorageEmulatorProcess : HttpProcess
    {
        internal WindowsAzureStorageEmulatorProcess()
            : this(GetExecutable())
        {
        }

        internal WindowsAzureStorageEmulatorProcess(FileInfo executable)
            : base(new Uri("http://127.0.0.1:10002/devstoreaccount1/"), GetProcessStartInfo(executable), TimeSpan.FromSeconds(30))
        {
        }

        private static ProcessStartInfo GetProcessStartInfo(FileInfo executable)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = executable.FullName,
                Arguments = "start"
            };

            return startInfo;
        }

        private static FileInfo GetExecutable()
        {
            var executable = new FileInfo(@"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\AzureStorageEmulator.exe");

            if (executable.Exists)
            {
                return executable;
            }

            throw new FileNotFoundException("Cannot find Windows Azure Storage Emulator executable.", executable.FullName);
        }
    }
}
