﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        private static Client authorizationCodeFlowClient;

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource()
                {
                    Name = "api1",
                    DisplayName = "Protected Produce API",
                    Scopes =
                    {
                        "api1.full_access",
                        "api1.read_only"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1.read_only"),
                new ApiScope("api1.full_access"),
            };

        public static IEnumerable<Client> GetClients()
        {
            authorizationCodeFlowClient = new Client
            {
                ClientId = "spa",
                ClientName = "Produce SPA React App",
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:5010/callback" },
                PostLogoutRedirectUris = { "https://localhost:5010" },
                AllowedCorsOrigins = { "https://localhost:5010" },

                AllowedScopes =
                {
                    "openid",
                    "profile",
                    "api1.read_only",
                    "api1.full_access"
                }
            };

            return new[] { authorizationCodeFlowClient };
        }
    }
}