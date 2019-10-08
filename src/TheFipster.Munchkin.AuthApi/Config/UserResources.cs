using System.Collections.Generic;
using IdentityServer4.Test;

namespace TheFipster.Munchkin.AuthApi.Config
{
    public static class UserResources
    {
        public static List<TestUser> Get() => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "2FA43F59-70A7-42B1-9AAD-542A0DC2C141",
                Username = "alice",
                Password = "password"
            },
            new TestUser
            {
                SubjectId = "D3C599D6-FD2E-4512-B1AC-AD4C318AE313",
                Username = "bob",
                Password = "password"
            }
        };
    }
}
