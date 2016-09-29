using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Scorelines.TestHelpers.Selenium.Services
{
    public static class WebDriver
    {
        private static WebDriverManager _manager;

        // A copy of WebDriverManager must be held by this class so its finalizer is called 
        // after all tests are run, not just the current test.
        public static WebDriverManager Manager
        {
            get { return _manager ?? (_manager = new WebDriverManager(new FirefoxDriver(), null)); }
            set
            {
                if (_manager != null)
                {
                    throw new InvalidOperationException("Manager may not be changed.");
                }
                _manager = value;
            }
        }

        public static IWebDriver Instance => Manager.WebDriver;
    }
}