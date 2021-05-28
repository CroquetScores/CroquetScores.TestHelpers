Feature: WebDriver

Scenario: Open website
	When WebDriver.Instance
	Then the result should be a ChromeDriver
	And the result can navigate to a working website
