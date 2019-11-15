using Microsoft.AspNetCore.Builder;

namespace TheFipster.Munchkin.Api.Common
{
    public static class EndpointExtensions
    {
        public static void UseControllers(this IApplicationBuilder app) => app
           .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    }
}
