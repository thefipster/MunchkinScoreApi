using System.Collections.Generic;
using IdentityServer4.Models;

namespace TheFipster.Munchkin.IdentityApi.Config
{
    public static class Apis
    {
        public static IEnumerable<ApiResource> Get() => new []
        {
            new ApiResource("game-api", "Munchkin Game API"),
            new ApiResource("sample-api", "Munchkin Sample API")
        };
    }
}
