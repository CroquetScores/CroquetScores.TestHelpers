using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Scorelines.TestHelpers.Selenium.Services;
using TechTalk.SpecFlow;

namespace Scorelines.TestHelpers.Selenium.Specifications.Features
{
    [Binding]
    public class WebDriverSteps
    {
        private IWebDriver _webDriver;

        [When(@"WebDriver\.Instance")]
        public void WhenWebDriver_Instance()
        {
            WebDriver.Manager = new WebDriverManager(new ChromeDriver(), null);
            _webDriver = WebDriver.Instance;
        }

        [Then(@"the result should be a ChromeDriver")]
        public void ThenTheResultShouldBeAChromeDriver()
        {
            _webDriver.Should().BeOfType<ChromeDriver>();
        }

        [Then(@"the result can navigate to a working website")]
        public void ThenTheResultCanNavigateToAWorkingWebsite()
        {
            _webDriver.Navigate().GoToUrl("https://google.com");

            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.ElapsedMilliseconds < 10000)
            {
                if (_webDriver.PageSource.Contains("<title>Google</title>"))
                {
                    return;
                }
                Thread.Sleep(0);
            }

            throw new Exception("Cannot find google title in: " + _webDriver.PageSource);
        }
    }
}