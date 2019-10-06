// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace TheFipster.Munchkin.AuthApi
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
                }
            };

        public static List<TestUser> GetUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "2FA43F59-70A7-42B1-9AAD-542A0DC2C141",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "D3C599D6-FD2E-4512-B1AC-AD4C318AE313",
                    Username = "bob",
                    Password = "password"
                }
            };
    }
}