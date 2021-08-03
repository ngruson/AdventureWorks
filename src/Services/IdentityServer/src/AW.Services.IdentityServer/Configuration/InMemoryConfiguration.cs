using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AW.Services.IdentityServer.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("customer-api", "Customer API")
                {
                    Scopes = { "customer-api.read" }
                },
                new ApiResource("basket-api", "Basket API")
                {
                    Scopes = { "basket-api.read", "basket-api.write", "basket-api.checkout" }
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new[]
            {
                new ApiScope("customer-api.read", "Reads customers"),

                new ApiScope("basket-api.read", "Reads shopping basket"),
                new ApiScope("basket-api.write", "Writes shopping basket"),
                new ApiScope("basket-api.checkout", "Checks out shopping basket")
            };
        }

        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "store",
                    ClientSecrets = new[] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "customer-api.read", "basket-api.read"
                    }
                },
                new Client
                {
                    ClientId = "internal",
                    ClientSecrets = new[] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "customer-api.read", "basket-api.read"
                    },
                    AllowOfflineAccess = true,
                    RedirectUris = new[] { "http://localhost:40610/signin-oidc" },
                    PostLogoutRedirectUris = new[] { "http://localhost:40610/signout-callback-oidc" }
                }
            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "nils",
                    Password = "Welkom01!",
                    Claims = new []
                    {
                        new Claim("email", "nils@congruent-it.nl")
                    }
                }
            };
        }
    }
}