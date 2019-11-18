using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheFipster.Munchkin.Identity.Api.Data;
using TheFipster.Munchkin.Identity.Api.Models;

namespace TheFipster.Munchkin.Identity.Api.Extensions
{
    public static class IdentityStorage
    {
        public const string IdentityContextConfigName = "IdentityConnection";

        public static void AddIdentityContext(this IServiceCollection services, IConfiguration config) => services
            .AddDbContext<ApplicationDbContext>(options => options
            .UseSqlite(config
            .GetConnectionString(IdentityContextConfigName)))
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}
