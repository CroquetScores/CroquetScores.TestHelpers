using System;
using OpenQA.Selenium;

namespace CroquetScores.TestHelpers.Selenium.Services
{
    public class WebDriverManager : IDisposable
    {
        internal readonly IWebDriver WebDriver;
        private bool _isDisposed;

        public WebDriverManager(IWebDriver webDriver, TimeSpan? implicitlyWait)
        {
            WebDriver = webDriver;
            webDriver.Manage().Timeouts().ImplicitWait = implicitlyWait ?? TimeSpan.FromSeconds(5);
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
                if (WebDriver != null)
                {
                    // Dispose unmanaged objects and override Finalize() below.
                }
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