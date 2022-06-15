using Geekshopping.CartAPI.Data.ValueObjects;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Geekshopping.CartAPI.Repository
{
    public class CuponRepository : ICuponRepository
    {

        private readonly HttpClient _client;

        public CuponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CuponVO> GetCupon(string cuponCode, string token)
        {
            //"api/v1/cupon"
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"/api/v1/cupon/{cuponCode}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode || response.StatusCode != HttpStatusCode.OK)
                return new CuponVO();
            return JsonSerializer.Deserialize<CuponVO>(content,
               new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
