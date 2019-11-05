using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TheFipster.Munchkin.Identity.Api.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get(IConfiguration config) => new[]
        {
            new Client
            {
                ClientId = "console-client",
                ClientName = "Munchkin Console Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "stash-api"
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
                RedirectUris = { $"{config["RedirectUri"]}/callback" },
                PostLogoutRedirectUris = { config["RedirectUri"] },
                AllowedCorsOrigins = { config["RedirectUri"] },
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
