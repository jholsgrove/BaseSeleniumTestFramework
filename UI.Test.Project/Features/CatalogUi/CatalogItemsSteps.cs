using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Reqnroll;
using Test.Helper.Project;
using Test.Helper.Project.DTOs;

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
        public void ThenTheItemIsListedAtAPriceOfX(string itemName, int priceOfItem)
        {
            bool bowIsPresent = false;
            int bowPrice = 0;

            // Refresh, the browser navigated on the scenario hook and THEN a new item was posted.
            Context.Driver.Navigate().Refresh();

            // Get the element on the page by the tag PRE
            string pageContent = Context.Driver.FindElement(By.TagName("pre")).Text;

            // map the json on the page to a list of catalog items
            var shopContent = RestManager.Deserialise<List<CatalogItemDto>>(pageContent);

            // look for the item we are interested in (the Bow)
            foreach (var item in shopContent)
            {
                if(item.Name == itemName) 
                {
                    bowIsPresent = true;
                    bowPrice = priceOfItem;
                }
            }

            // Check the name and the price
            Assert.That(bowIsPresent, Is.True, "Bow was not listed in shop");
            Assert.That(bowPrice, Is.EqualTo(priceOfItem));
        }
    }
}