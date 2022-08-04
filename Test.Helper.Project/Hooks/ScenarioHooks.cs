using TechTalk.SpecFlow;
using System.Net;
using OpenQA.Selenium;
using BoDi;

namespace Test.Helper.Project.Hooks
{
	[Binding]
	public class ScenarioHooks : Steps
	{
		public static CookieContainer GlobalCookieContainer = new CookieContainer();
		public static CookieContainer UserCookieContainer = new CookieContainer();
		private readonly IObjectContainer ObjectContainer;
		private IWebDriver WebDriver;

		public static IHttpClientFactory? TestHttpClientFactory { get; internal set; }

		public ScenarioHooks(IObjectContainer objectContainer)
		{
			ObjectContainer = objectContainer;
		}

		[BeforeScenario]
		public void BeforeScenario()
		{
			LogHelper.Log("[BeforeScenario] start");

			var browser = TestConfiguration.Get("Browser");
			var url = TestConfiguration.Get("BaseUrl");
			var method = TestConfiguration.Get("TestMethod");

			if (method.ToLower() != "api")
			{
				WebDriver = WebDriverFactory.CreateLocal(browser);
				ObjectContainer.RegisterInstanceAs(new PageContext(WebDriver));

				TestStateContext.Get(ScenarioContext).BaseWindow = WebDriver.CurrentWindowHandle;
				TestStateContext.Get(ScenarioContext).Driver = WebDriver;
				WebDriver.Navigate().GoToUrl(url);
			}

			LogHelper.Log("[BeforeScenario] end");
		}

		[AfterScenario]
		public void AfterScenario()
		{
			var method = TestConfiguration.Get("TestMethod");

			LogHelper.Log("[AfterScenario] start");
			if (method.ToLower() != "api")
			{
				WebDriver.Quit();
				WebDriver.Dispose();
			}

			LogHelper.Log("[AfterScenario] performing teardown");
			ConductTeardown();

			LogHelper.Log("[AfterScenario] end");
		}

		private void ConductTeardown()
        {
			// Now iterate on things in the stack
			var actionStack = TestStateContext.Get(ScenarioContext).TeardownActions;

			while (actionStack.Count > 0)
			{
				var nextAction = actionStack.Pop();
				nextAction();
			}
		}
	}
}