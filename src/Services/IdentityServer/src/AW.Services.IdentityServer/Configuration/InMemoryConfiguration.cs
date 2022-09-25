using Duende.IdentityServer;
using Entities = Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Test;
using Microsoft.Extensions.Configuration;

namespace AW.Services.IdentityServer.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<Entities.ApiResource> ApiResources()
        {
            return new[]
            {
                new Entities.ApiResource
                {
                    Name = "customer-api",
                    DisplayName = "Customer API",
                    Scopes = { new ApiResourceScope { Scope = "customer-api.read" } }
                },
                new Entities.ApiResource
                {
                    Name = "basket-api",
                    DisplayName = "Basket API",
                    Scopes = 
                    {
                        new ApiResourceScope { Scope = "basket-api.read" },
                        new ApiResourceScope { Scope = "basket-api.write" },
                        new ApiResourceScope { Scope = "basket-api.checkout" },
                    }
                }
            };
        }

        public static IEnumerable<Entities.ApiScope> ApiScopes()
        {
            return new[]
            {
                new Entities.ApiScope { Name = "customer-api.read", DisplayName = "Reads customers" },
                new Entities.ApiScope { Name = "basket-api.read", DisplayName = "Reads shopping basket" },                
                new Entities.ApiScope { Name = "basket-api.write", DisplayName = "Writes shopping basket" },
                new Entities.ApiScope { Name = "basket-api.checkout", DisplayName = "Checks out shopping basket" }
            };
        }

        public static IEnumerable<Entities.IdentityResource> IdentityResources()
        {
            return new Entities.IdentityResource[]
            {
                new Entities.IdentityResource { Name = "openid" },
                new Entities.IdentityResource { Name = "profile" },
                new Entities.IdentityResource { Name = "email" }
            };
        }

        public static IEnumerable<Entities.Client> Clients()
        {
            return new Entities.Client[]
            {
                new Entities.Client
                {
                    ClientId = "store",
                    ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() } },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials
                        .Select(grantType => new ClientGrantType { GrantType = grantType })
                        .ToList(),
                    AllowedScopes = new List<ClientScope>
                    {
                        new ClientScope { Scope = IdentityServerConstants.StandardScopes.OpenId },
                        new ClientScope { Scope = IdentityServerConstants.StandardScopes.Profile },
                        new ClientScope { Scope = IdentityServerConstants.StandardScopes.OfflineAccess },
                        new ClientScope { Scope = "customer-api.read" },
                        new ClientScope { Scope = "basket-api.read" }
                    }
                },
                new Entities.Client
                {
                    ClientId = "internal",
                    ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() } },
                    AllowedGrantTypes = GrantTypes.Code
                        .Select(grantType => new ClientGrantType { GrantType = grantType })
                        .ToList(),
                    AllowedScopes = new List<ClientScope>
                    {
                        new ClientScope { Scope = IdentityServerConstants.StandardScopes.OpenId },
                        new ClientScope { Scope = IdentityServerConstants.StandardScopes.Profile },
                        new ClientScope { Scope = IdentityServerConstants.StandardScopes.Email },
                        new ClientScope { Scope = "customer-api.read" },
                        new ClientScope { Scope = "basket-api.read" }
                    },
                    AllowOfflineAccess = true,
                    RedirectUris = new List<ClientRedirectUri>
                    {
                        new ClientRedirectUri { RedirectUri = "http://localhost:40610/signin-oidc" }
                    },
                    PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri> 
                    { 
                        new ClientPostLogoutRedirectUri { PostLogoutRedirectUri = "http://localhost:40610/signout-callback-oidc" }
                    }
                }
            };
        }

        public static IEnumerable<TestUser> Users(IConfiguration configuration)
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = configuration["TestUser:UserName"],
                    Password = configuration["TestUser:Password"],
                    Claims = new []
                    {
                        new Claim("email", configuration["TestUser:Email"])
                    }
                }
            };
        }
    }
}