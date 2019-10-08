using IdentityServer4.Models;
using System.Collections.Generic;

namespace TheFipster.Munchkin.IdentityApi.Config
{
    public static class Resources
    {
        public static IEnumerable<IdentityResource> Get() =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email() 
            };
    }
}
