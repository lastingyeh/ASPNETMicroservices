using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.Configuration
{
    public class Config
    {
        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl) =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "aspnetrunbasicsClient",
                    ClientName = "AspNetRunBasics",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("AspNetRunBasicsSecrets".Sha256())
                    },
                    ClientUri = $"{clientsUrl["AspNetRunBasics"]}",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    RedirectUris = {$"{clientsUrl["AspNetRunBasics"]}/signin-oidc"},
                    PostLogoutRedirectUris = {$"{clientsUrl["AspNetRunBasics"]}/signout-callback-oidc"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "basket.checkout",
                    },
                    // for dev need to set 'false' [GrantTypes.Hybrid]
                    RequirePkce = false,
                },
                new Client
                {
                    ClientId = "basketapiClient",
                    ClientName = "Basket.API",
                    ClientSecrets = { new Secret("BasketAPISecrets".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"basket.checkout"},
                },
            };
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("basket.checkout"),
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("Basket.API", "Basket Service"){ Scopes = { "basket.checkout" }},
            };
    }
}