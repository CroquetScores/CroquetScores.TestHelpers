using System;
using OpenQA.Selenium;
using Scorelines.TestHelpers.Selenium.Support;

namespace Scorelines.TestHelpers.Selenium.Services
{
    public static class WebDriver
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private static readonly WebDriverManager Manager;

        public static readonly IWebDriver Instance;

        static WebDriver()
        {
            // A copy of WebDriverManager must be held by this class so its finalizer is called 
            // after all tests are run, not just the current test.
            Manager = new WebDriverManager(TimeSpan.FromSeconds(1));
            Instance = Manager.WebDriver;
        }
    }
}