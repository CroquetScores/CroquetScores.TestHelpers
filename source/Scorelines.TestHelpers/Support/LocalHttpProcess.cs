using System;
using System.Diagnostics;

namespace Scorelines.TestHelpers.Support
{
    internal abstract class LocalHttpProcess : HttpProcess
    {
        protected LocalHttpProcess(int port, ProcessStartInfo startInfo)
            : this("http", port, startInfo)
        {
        }

        protected LocalHttpProcess(string scheme, int port, ProcessStartInfo startInfo)
            : base(new Uri($"{scheme}://localhost:{port}"), startInfo)
        {
        }
    }
}