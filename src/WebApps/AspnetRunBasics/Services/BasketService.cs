using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;
        private readonly IAuthService _authService;

        public BasketService(HttpClient client,
            IAuthService authService)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

            Authorize(_client);
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/Basket/{userName}");

            return await response.ReadContentAs<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            var response = await _client.PostAsJson($"/Basket", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<BasketModel>();
            }
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            var response = await _client.PostAsJson($"/Basket/Checkout", model);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        private async void Authorize(HttpClient _client)
        {
            var token = await _authService.AuthorizeAsync();

            _client.SetBearerToken(token.AccessToken);
        }
    }
}
