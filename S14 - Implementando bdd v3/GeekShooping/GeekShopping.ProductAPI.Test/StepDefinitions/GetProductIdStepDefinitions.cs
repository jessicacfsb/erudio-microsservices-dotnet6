using System;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;

namespace GeekShopping.ProductAPI.Test.StepDefinitions
{
    [Binding]
    public class GetProductIdStepDefinitions
    {
        private static MediaTypeHeaderValue contentType
            = new MediaTypeHeaderValue("application/json");
        private string URL = "https://localhost:4440/api/v1/Product";
        private readonly ScenarioContext _scenarioContext;

        public GetProductIdStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que o id do produto seja (.*)")]
        public void GivenQueOIdDoProdutoE(int p0)
        {
            throw new PendingStepException();
        }

        [Given(@"o token seja token")]
        public void GivenOTokenSejaToken()
        {
            throw new PendingStepException();
        }

        [Given(@"o metodo http '([^']*)'")]
        public void GivenOMetodoHttp(string gET)
        {
            throw new PendingStepException();
        }

        [When(@"chamar o serviço")]
        public void WhenChamarOServico()
        {
            throw new PendingStepException();
        }

        [Then(@"statuscode de resposta deverá ser OK")]
        public void ThenStatuscodeDeRespostaDeveraSerOK()
        {
            throw new PendingStepException();
        }
    }
}
