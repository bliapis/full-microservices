using IdentityModel.Client;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface IAuthService
    {
        public Task<TokenResponse> AuthorizeAsync();
    }
}