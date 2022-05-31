using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using RestSharp;
using System.Net.Http.Headers;

namespace GeekShopping.ProductAPI.Test.StepDefinitions
{


    [Binding]
    public class GetProductIdStepDefinitions
    {
        private static MediaTypeHeaderValue contentType
            = new MediaTypeHeaderValue("application/json");
        private string URL = "https://localhost:4440/api/v1/Product/";
        private readonly ScenarioContext _scenarioContext;

        public GetProductIdStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que o id do produto seja (.*)")]
        public void GivenQueOIdDoProdutoE(string id)
        {
            _scenarioContext["ItemId"] = id;
        }

        [Given(@"que para autenticação seja recebido um token")]
        public async Task GivenQueParaAutenticacaoSejaRecebidoUmToken()
        {
            var client = new RestClient("https://localhost:4435/");
            //client.Authenticator = new HttpBasicAuthenticator("A");
            var request = new RestRequest("connect/token", Method.Post);
            request.AddParameter("client_id", "client");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_secret", "my_super_secret");
            request.AddObject("geek_shopping", "openid", "profile");
            request.AddHeader("content_type", "application/json");
            
            var response = await client.PostAsync(request);

            _scenarioContext["Response"] = response.StatusDescription;
        }


        [Given(@"o metodo http '([^']*)'")]
        public void GivenOMetodoHttp(string method)
        {
            _scenarioContext["Method"] = method;
            string id = (string)_scenarioContext["ItemId"];
            URL += $"{id}";
        }

        [When(@"chamar o serviço")]
        public async Task WhenChamarOServico()
        {
            string token = (string)_scenarioContext["Reponse"];
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            //var result = await client.GetAsync(URL);
            //_scenarioContext["Response"] = result;
            _scenarioContext["Response"] = token;
        }

        [Then(@"statuscode de resposta deverá ser OK")]
        public void ThenStatuscodeDeRespostaDeveraSerOK()
        {
            var response = _scenarioContext["Response"];
            Assert.IsTrue(response != null);
        }

   
    }
}
