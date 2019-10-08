using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace TheFipster.Munchkin.IdentityApi.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get() => new []
        {
            new Client
            {
                ClientId = "console-client",
                ClientName = "Munchkin Console Client",
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "sample-api"
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            },
            new Client
            {
                ClientId = "client-web",
                ClientName = "Munchkin MVC Client",
                AllowedGrantTypes = GrantTypes.Hybrid,
                AllowOfflineAccess = true,
                RedirectUris = { "https://localhost:4001/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:4001/signout-callback-oidc" },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "sample-api"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                }
            },
            new Client
            {
                ClientId = "game-spa",
                ClientName = "Gaming Angular Frontend",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "game-api"
                }
            }
        };
    }
}
