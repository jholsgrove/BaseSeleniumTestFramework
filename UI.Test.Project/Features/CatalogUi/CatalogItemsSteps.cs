using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Test.Helper.Project;

namespace UI.Test.Project.Features.CatalogUi
{
    [Binding]
    internal class CatalogItemsSteps
    {
        private ScenarioContext ScenarioContext { get; }
        private TestStateContext Context => TestStateContext.Get(ScenarioContext);
        public CatalogItemsSteps(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        [Then(@"the (.*) is listed at a price of (.*)")]
        public void ThenTheItemIsListedAtAPriceOfX(string itemName, string priceOfItem)
        {
            // Refresh, the browser navigated on the scenario hook and THEN a new item was posted.
            Context.Driver.Navigate().Refresh();

            // Get the element on the page by the tag PRE
            var content = Context.Driver.FindElement(By.TagName("pre"));

            // Check we have a Bow priced at 18
            Assert.That(content.Text.Contains($"\"name\":\"{itemName}\",\"price\":{priceOfItem}"));
        }
    }
}