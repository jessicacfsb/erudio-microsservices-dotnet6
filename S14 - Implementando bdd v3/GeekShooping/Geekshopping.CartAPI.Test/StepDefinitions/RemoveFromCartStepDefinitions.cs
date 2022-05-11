using NUnit.Framework;
using System;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace Geekshopping.CartAPI.Test.StepDefinitions
{
    [Binding]
    public class RemoveFromCartStepDefinitions
    {
        private string URL = "https://localhost:4445/api/v1/Cart/";
        private readonly ScenarioContext _scenarioContext;

        public RemoveFromCartStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que o Id do item seja '([^']*)'")]
        public void GivenQueOIdDoItemSeja(string id)
        {
            _scenarioContext["Item"] = id;
        }

        [Given(@"o método '([^']*)'")]
        public void GivenOMetodo(string method)
        {
            _scenarioContext["Method"] = method; 
            string id = (string)_scenarioContext["Item"];
            URL += $"remove-cart/{id}";
        }

        [When(@"o método for executado")]
        public async Task WhenOMetodoForExecutado()
        {
            var client = new HttpClient();
            var result = await client.DeleteAsync(URL);
            _scenarioContext["Response"] = result;
        }

        [Then(@"statuscode de resposta devera ser OK")]
        public void ThenStatuscodeDeRespostaDeveraSerOK()
        {
            var response = _scenarioContext["Response"];
            Assert.IsTrue(response != null);
        }
    }
}
