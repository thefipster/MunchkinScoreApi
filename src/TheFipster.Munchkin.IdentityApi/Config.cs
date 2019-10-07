// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TheFipster.Munchkin.IdentityApi
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new ApiResource[]
            {
                new ApiResource("game-api", "Munchking Game API"),
                new ApiResource("sample-api", "Munchking Sample API")
            };

        public static IEnumerable<Client> GetClients() =>
            new Client[]
            {
                new Client
                {
                    ClientId = "console-client",
                    ClientName = "Munchkin Console Client",
                    AllowedScopes = { "sample-api" },
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
                    RedirectUris =           { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins =     { "http://localhost:4200" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sample-api",
                        "game-api",
                    }
                }
            };
    }
}