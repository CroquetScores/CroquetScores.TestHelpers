using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CroquetScores.TestHelpers.Extensions;

namespace CroquetScores.TestHelpers.Support
{
    internal abstract class HttpProcess : IDisposable
    {
        private readonly Uri _endpoint;
        private readonly TimeSpan _maximumWaitTimeForProcessToRespond;
        private readonly ProcessStartInfo _startInfo;
        private bool _isDisposed;

        private Process _process;

        protected HttpProcess(Uri endpoint, ProcessStartInfo startInfo, TimeSpan maximumWaitTimeForProcessToRespond = default(TimeSpan))
        {
            _endpoint = endpoint;
            _startInfo = startInfo;
            _maximumWaitTimeForProcessToRespond = maximumWaitTimeForProcessToRespond == default(TimeSpan) ? TimeSpan.FromSeconds(5) : maximumWaitTimeForProcessToRespond;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing).
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal bool IsRunning()
        {
            return _endpoint.IsResponding();
        }

        internal void Start()
        {
            _process = StartProcess();
            WaitForProcessToRespond();
        }

        internal HttpProcess StartIfNotRunning()
        {
            if (IsRunning())
            {
                return this;
            }

            Start();

            return this;
        }

        private Process StartProcess()
        {
            try
            {
                var process = new Process {StartInfo = _startInfo};
                process.Start();

                Task.Factory.StartNew(process.WaitForExit);

                return process;
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot start '{GetType().Name}' at '{_endpoint}'.", exception);
            }
        }

        private void WaitForProcessToRespond()
        {
            var stopwatch = Stopwatch.StartNew();

            while (!_endpoint.IsResponding())
            {
                if (stopwatch.Elapsed > _maximumWaitTimeForProcessToRespond)
                {
                    throw new TimeoutException($"'{GetType().Name}' at '{_endpoint}' did not respond with '{_maximumWaitTimeForProcessToRespond}'.");
                }
                Thread.Sleep(0);
            }
        }

        /// <summary>
        ///     Releases unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (_process != null)
                {
                    _process.Kill();
                    _process = null;
                }
            }

            _isDisposed = true;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="HttpProcess" /> class.
        /// </summary>
        ~HttpProcess()
        {
            // Do not change this code.
            // Put cleanup code in Dispose(bool disposing).
            Dispose(false);
        }
    }
}