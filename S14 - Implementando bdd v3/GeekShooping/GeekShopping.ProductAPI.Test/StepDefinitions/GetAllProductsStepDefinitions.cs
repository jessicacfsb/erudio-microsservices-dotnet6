using NUnit.Framework;
using System;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace GeekShopping.ProductAPI.Test.StepDefinitions
{
    [Binding]
    public class GetAllProductsStepDefinitions
    {
        private static MediaTypeHeaderValue contentType
            = new MediaTypeHeaderValue("application/json");
        private string URL = "https://localhost:4440/";
        private readonly ScenarioContext _scenarioContext;

        public GetAllProductsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"que a url recebida seja route")]
        public void GivenQueAUrlSeja()
        {
            _scenarioContext["Route"] = "api/v1/Product";
        }

        [Given(@"que o método http seja '([^']*)'")]
        public void GivenQueOMetodoHttpSeja(string method)
        {
            _scenarioContext["Method"] = method;
            string route = (string)_scenarioContext["Route"];
            URL += $"{route}";
        }

        [When(@"chamar o método")]
        public async Task WhenChamarOMetodo()
        {
            var client = new HttpClient();
            string route = (string)_scenarioContext["Route"];
            var result = await client.GetAsync(URL);
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
