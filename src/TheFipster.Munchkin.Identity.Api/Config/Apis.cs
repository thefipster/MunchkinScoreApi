using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace TheFipster.Munchkin.Identity.Api.Config
{
    public static class Apis
    {
        public static IEnumerable<ApiResource> Get() => new[]
        {
            new ApiResource("game-api", "Munchkin Game API")
            {
                UserClaims =
                {
                    JwtClaimTypes.ClientId,
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.PreferredUserName
                }
            },
            new ApiResource("sample-api", "Munchkin Sample API")
        };
    }
}
