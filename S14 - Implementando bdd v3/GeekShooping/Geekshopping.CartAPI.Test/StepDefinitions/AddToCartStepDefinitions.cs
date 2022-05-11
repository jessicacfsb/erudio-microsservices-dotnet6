using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Model;
using GeekShopping.Web.Utils;
using NUnit.Framework;
using System;
using System.Net.Http.Headers;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace Geekshopping.CartAPI.Test.StepDefinitions
{
    [Binding]
    public class AddToCartStepDefinitions
    {
        private static MediaTypeHeaderValue contentType
            = new MediaTypeHeaderValue("application/json");
        private string URL = "https://localhost:4445/api/v1/Cart/";
        private readonly ScenarioContext _scenarioContext;

        public AddToCartStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"que o userId do carrinho seja '([^']*)'")]
        public void GivenQueOUserIdDoCarrinhoSeja(string userId)
        {
            _scenarioContext["User"] = userId;
        }

        [Given(@"o metodo '([^']*)'")]
        public void GivenOMetodo(string method)
        {
            _scenarioContext["Method"] = method;
            string userId = (string)_scenarioContext["User"];
            URL += $"add-cart/{userId}";

        }

        [When(@"o metodo for executado")]
        public async Task WhenOMetodoForExecutado()
        {
            var cartDetail = new List<CartDetailVO>();
            var userId = (string)_scenarioContext["User"];
            cartDetail.Add(new CartDetailVO
            {
                Id = 11,
                CartHeaderId = 13,
                CartHeader = new CartHeaderVO()
                {
                    Id = 13,
                    UserId = userId,
                    CuponCode = ""
                },
                ProductId = 2,
                Product = new ProductVO()
                {
                    Id = 2,
                    Name = "Camiseta No Internet",
                    Price = 69,
                    Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.",
                    CategoryName = "T-Shirt",
                    ImageURL = "https://github.com/jessicacfsb/erudio-microsservices-dotnet6/blob/main/S08%20-%20Organizando%20Arquitetura/GeekShooping/ShoppingImages/2_no_internet.jpg?raw=true"
                },
                Count = 7
            });

            var cart = new CartVO()
            {
                CartHeader = new CartHeaderVO()
                {
                    Id = 0,
                    UserId = userId,
                    CuponCode = ""
                },
                CartDetails = cartDetail,
            };
            var client = new HttpClient();
            var dataAsString = JsonSerializer.Serialize(cart);
            var content = new StringContent(dataAsString);
            var result = await client.PostAsync(URL, content);
            _scenarioContext["Response"] = result;
        }

        [Then(@"statuscode da resposta devera ser OK")]
        public void ThenStatuscodeDaRespostaDeveraSer()
        {
            var response = _scenarioContext["Response"];
            Assert.IsTrue(response != null);
        }
    }
}
