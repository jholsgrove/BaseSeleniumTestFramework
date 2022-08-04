using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Test.Helper.Project
{
	public class WebDriverFactory
	{
		public static IWebDriver CreateLocal(string browser)
		{
			switch (browser.ToLower())
			{
				case "chrome":
					return CreateChromeDriver();
				default:
					throw new ApplicationException($"Unsupported browser! '{browser}'");
			}
		}

		public static RemoteWebDriver CreateDistributed(string browser, string gridUrl)
		{
			switch (browser.ToLower())
			{
				case "chrome":
					return SetupChromeGrid(gridUrl);
				default:
					throw new ApplicationException($"Unsupported browser! '{browser}'");
			}
		}

		public static ChromeDriver CreateChromeDriver()
		{
			var service = ChromeDriverService.CreateDefaultService();
			var options = new ChromeOptions();

			options.SetLoggingPreference(LogType.Browser, LogLevel.Info);
			options.AddArgument("no-sandbox");
			options.AddArgument("disable-infobars");
			options.AddArgument("start-maximized");

			var isHeadless = TestConfiguration.Get("browser_headless");
			if (bool.Parse(isHeadless))
			{
				options.AddArgument("--headless");
			}

			return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
		}

		public static RemoteWebDriver SetupChromeGrid(string gridHub)
		{
			var options = new ChromeOptions();
			options.SetLoggingPreference(LogType.Browser, LogLevel.Info);
			options.AcceptInsecureCertificates = true;
			options.AddArgument("--ignore-ssl-errors=yes");
			options.AddArgument("--ignore-certificate-errors");
			options.AddArgument("--no-sandbox");

			var isHeadless = TestConfiguration.Get("browser_headless");
			if (bool.Parse(isHeadless))
			{
				options.AddArgument("--headless");
			}

			var timespan = TimeSpan.FromMinutes(3);
			return new RemoteWebDriver(new Uri(gridHub), options.ToCapabilities(), timespan);
		}
	}
}