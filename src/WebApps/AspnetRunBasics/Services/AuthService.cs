using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<TokenResponse> AuthorizeAsync()
        {
            var tokenResponse = await _client.RequestClientCredentialsTokenAsync(GetCredentials);

            if (tokenResponse.IsError)
            {
                return null;
            }

            return tokenResponse;
        }

        private ClientCredentialsTokenRequest GetCredentials => new ClientCredentialsTokenRequest
        {
            Address = $"{_client.BaseAddress}connect/token",

            ClientId = "ecommerceClient",
            ClientSecret = "secret",

            Scope = "basketAPI"
        };
    }
}