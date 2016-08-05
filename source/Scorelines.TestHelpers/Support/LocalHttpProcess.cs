using System;
using System.Diagnostics;

namespace Scorelines.TestHelpers.Support
{
    internal abstract class LocalHttpProcess : HttpProcess
    {
        protected LocalHttpProcess(int port, ProcessStartInfo startInfo, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
            : this("http", port, startInfo, maximumWaitTimeForProcessToRespond)
        {
        }

        protected LocalHttpProcess(string scheme, int port, ProcessStartInfo startInfo, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
            : base(new Uri($"{scheme}://localhost:{port}"), startInfo, maximumWaitTimeForProcessToRespond)
        {
        }
    }
}