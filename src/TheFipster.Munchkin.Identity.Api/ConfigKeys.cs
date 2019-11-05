namespace TheFipster.Munchkin.Identity.Api
{
    public static class ConfigKeys
    {
        public const string CorsOriginPolicyName = "default";
        public const string IdentityContextConfigName = "IdentityConnection";

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
}
