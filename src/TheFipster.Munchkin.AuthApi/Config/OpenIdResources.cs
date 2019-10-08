using System.Collections.Generic;
using IdentityServer4.Models;

namespace TheFipster.Munchkin.AuthApi.Config
{
    public static class OpenIdResources
    {
        public static IEnumerable<IdentityResource> Get() => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
}
