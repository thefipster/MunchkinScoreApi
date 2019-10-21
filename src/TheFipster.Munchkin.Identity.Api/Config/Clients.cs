using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TheFipster.Munchkin.Identity.Api.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get() => new[]
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
                    "stash-api"
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
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
                RequireConsent = false,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "http://localhost:4200/callback" },
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
