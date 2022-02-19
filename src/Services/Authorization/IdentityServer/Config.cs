using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "ecommerceClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "basketAPI" }
                },
                new Client
                {
                    ClientId = "ecommerceAPI",
                    ClientName = "Ecommerce client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "http://localhost:5002/signin"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "http://localhost:5002/signout-callback"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("basketAPI", "Basket API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "7b9e7f33-396d-4960-9cea-792e089dae4d",
                    Username = "bruno",
                    Password = "bruno",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Bruno"),
                        new Claim(JwtClaimTypes.FamilyName, "Lima")
                    }
                }
            };
    }
}