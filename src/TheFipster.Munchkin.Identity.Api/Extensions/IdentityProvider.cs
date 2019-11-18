using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheFipster.Munchkin.Identity.Api.Config;
using TheFipster.Munchkin.Identity.Api.Models;

namespace TheFipster.Munchkin.Identity.Api.Extensions
{
    public static class IdentityProvider
    {
        public static void AddIdentityServer(this IServiceCollection services, 
            IConfiguration config, 
            IHostEnvironment env) => services
            .AddIdentityServer(
                options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
            .AddInMemoryIdentityResources(Resources.Get())
            .AddInMemoryApiResources(Apis.Get())
            .AddInMemoryClients(Clients.Get(config))
            .AddAspNetIdentity<ApplicationUser>()
            .AddSigningCredentials(env);
    }
}
