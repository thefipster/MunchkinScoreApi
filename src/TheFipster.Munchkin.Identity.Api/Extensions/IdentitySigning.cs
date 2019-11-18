using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheFipster.Munchkin.Identity.Api.Extensions
{
    public static class IdentitySigning
    {
        public static void AddSigningCredentials(this IIdentityServerBuilder identityServerBuilder, IHostEnvironment env)
        {
            if (env.IsDevelopment())
                identityServerBuilder.AddDeveloperSigningCredential();
            else
                throw new Exception("need to configure key material");
        }
    }
}
