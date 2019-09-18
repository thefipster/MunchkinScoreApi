using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    return;
                }

                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code = getCodeFrom(ex);
            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result).ConfigureAwait(false);
        }

        private static HttpStatusCode getCodeFrom(Exception ex)
        {
            switch (ex)
            {
                case UnknownGameException _:
                case UnknownPlayerException _:
                    return HttpStatusCode.NotFound;
                case ArgumentNullException _:
                case ArgumentException _:
                case InvalidModifierException _:
                case ProtocolEmptyException _:
                case InvalidGameMessageException _:
                case InvalidActionException _:
                    return HttpStatusCode.BadRequest;
                case NotImplementedException _:
                    return HttpStatusCode.NotImplemented;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
