namespace TheFipster.Munchkin.Api.Common
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.HttpOverrides;

    public static class ProxyExtensions
    {
        public static void UseProxy(this IApplicationBuilder appBuilder) => appBuilder
           .UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
    }
}
