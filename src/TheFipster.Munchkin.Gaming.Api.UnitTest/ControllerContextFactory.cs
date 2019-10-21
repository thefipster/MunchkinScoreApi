using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace TheFipster.Munchkin.Gaming.Api.UnitTest
{
    public class ControllerContextFactory
    {
        public static ControllerContext CreateWithUserContext(string externalId, string userName)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, externalId),
                new Claim("userId", externalId.ToString()),
            }, "mock"));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}
