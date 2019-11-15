namespace TheFipster.Munchkin.Api.Common
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class AuthExtensions
    {
        private const string AuthenticationScheme = "Bearer";

        public static void AddJwtAuth(this IServiceCollection services, IConfiguration config) => services
            .AddAuthentication(AuthenticationScheme)
            .AddJwtBearer(AuthenticationScheme, options =>
            {
               options.Authority = config.GetAuthority();
               options.Audience = config.GetAudience();
            });

    }
}
