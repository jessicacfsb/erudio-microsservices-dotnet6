using NUnit.Framework;
using System.Net.Http.Headers;

namespace Geekshopping.CartAPI.Test.StepDefinitions
{
    [Binding]
    public class GetUserIDStepDefinitions
    {
        private string URL = "https://localhost:4440/api/v1/Product";
        private readonly ScenarioContext _scenarioContext;

        public GetUserIDStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que o id usuário seja '([^']*)'")]
        public void GivenQueOIdUsuarioSeja(string userId)
        {
            _scenarioContext["User"] = userId;
        }

        [Given(@"o método http '([^']*)'")]
        public void GivenOMetodoHttp(string method)
        {
            _scenarioContext["Method"] = method;
            string userId = (string)_scenarioContext["User"];
            URL += $"find-cart/{userId}";
        }

        [When(@"chamar o serviço")]
        public async Task WhenChamarOServico()
        {
            var client = new HttpClient();
            var result = await client.GetAsync(URL);
            _scenarioContext["Response"] = result;
        }

        [Then(@"statuscode de resposta deverá ser OK")]
        public void ThenStatuscodeDeRespostaDeveraSer()
        {
            var response = _scenarioContext["Response"];
            Assert.IsTrue(response != null);
        }
    }
}
