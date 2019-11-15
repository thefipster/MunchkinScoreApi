using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace TheFipster.Munchkin.Identity.Api.Extensions
{
    public static class ExternalProviders
    {
        public static void AddExternalProviders(this IServiceCollection services, IConfiguration config) =>
            services.AddAuthentication()
                    .AddGoogle(options =>
                     {
                         options.ClientId = config[LoginProvider.Google.ClientId];
                         options.ClientSecret = config[LoginProvider.Google.ClientSecret];
                     })
                    .AddFacebook(options =>
                     {
                         options.ClientId = config[LoginProvider.Facebook.ClientId];
                         options.ClientSecret = config[LoginProvider.Facebook.ClientSecret];
                     })
                    .AddMicrosoftAccount(options =>
                     {
                         options.ClientId = config[LoginProvider.Microsoft.ClientId];
                         options.ClientSecret = config[LoginProvider.Microsoft.ClientSecret];
                     })
                    .AddTwitter(options =>
                     {
                         options.ConsumerKey = config[LoginProvider.Twitter.ConsumerKey];
                         options.ConsumerSecret = config[LoginProvider.Twitter.ConsumerSecret];
                     });
    }

    public static class LoginProvider
    {
        public static class Google
        {
            public const string ClientId = "ExternalProviders:Google:ClientId";
            public const string ClientSecret = "ExternalProviders:Google:ClientSecret";
        }
        public static class Facebook
        {
            public const string ClientId = "ExternalProviders:Facebook:ClientId";
            public const string ClientSecret = "ExternalProviders:Facebook:ClientSecret";
        }
        public static class Microsoft
        {
            public const string ClientId = "ExternalProviders:Microsoft:ClientId";
            public const string ClientSecret = "ExternalProviders:Microsoft:ClientSecret";
        }
        public static class Twitter
        {
            public const string ConsumerKey = "ExternalProviders:Twitter:ConsumerKey";
            public const string ConsumerSecret = "ExternalProviders:Twitter:ConsumerSecret";
        }
    }
}
