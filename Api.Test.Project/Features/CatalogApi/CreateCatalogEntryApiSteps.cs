using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using TechTalk.SpecFlow;
using Test.Helper.Project;
using Test.Helper.Project.DTOs;

namespace Api.Test.Project.Features.CatalogApi
{
    [Binding]
    internal sealed class CreateCatalogEntryApiSteps
    {
        private ScenarioContext ScenarioContext { get; }
        private TestStateContext Context => TestStateContext.Get(ScenarioContext);

        public CreateCatalogEntryApiSteps(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        [Then(@"the item gets created")]
        public void ThenTheItemGetsCreated()
        {
            Assert.That(Context.RecordedStatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(Context.PostedCatalogItem.Name, Is.EqualTo(Context.CatalogItem.Name));
            Assert.That(Context.PostedCatalogItem.Price, Is.EqualTo(Context.CatalogItem.Price));
        }
    }
}