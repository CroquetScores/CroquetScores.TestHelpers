using System;
using OpenQA.Selenium;
using Scorelines.TestHelpers.Support;

namespace Scorelines.TestHelpers.Services
{
    public static class WebDriver
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly WebDriverManager Manager;

        public static readonly IWebDriver Instance;

        static WebDriver()
        {
            WebSite.StartIfNotRunning();

            // A copy of WebDriverManager must be held by this class so its finalizer is called 
            // after all tests are run, not just the current test.
            Manager = new WebDriverManager(TimeSpan.FromSeconds(1));
            Instance = Manager.WebDriver;
        }
    }
}