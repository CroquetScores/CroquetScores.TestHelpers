using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Scorelines.TestHelpers.Support
{
    internal class WebDriverManager : IDisposable
    {
        private bool _isDisposed;
        internal readonly IWebDriver WebDriver;

        internal WebDriverManager(TimeSpan implicitlyWait)
        {
            WebDriver = new FirefoxDriver();
            WebDriver.Manage().Timeouts().ImplicitlyWait(implicitlyWait);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code.
            // Put cleanup code in Dispose(bool disposing).
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases unmanaged and optionally managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        // ReSharper disable once UnusedParameter.Local
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                // Dispose unmanaged objects and override Finalize() below.
                WebDriver?.Close();
            }

            _isDisposed = true;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="WebDriverManager" /> class.
        /// </summary>
        ~WebDriverManager()
        {
            // Do not change this code.
            // Put cleanup code in Dispose(bool disposing).
            Dispose(false);
        }
    }
}