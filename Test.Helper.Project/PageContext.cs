using OpenQA.Selenium;

namespace Test.Helper.Project
{
    public class PageContext
    {
		public IWebDriver Driver { get; set; }
		public PageContext(IWebDriver driver)
		{
			Driver = driver;
		}
	}
}