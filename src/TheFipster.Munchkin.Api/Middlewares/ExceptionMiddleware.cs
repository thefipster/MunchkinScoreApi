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
            HttpStatusCode code;

            switch (ex)
            {
                case UnknownGameException _:
                case UnknownPlayerException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case ArgumentNullException _:
                case ArgumentException _:
                case InvalidModifierException _:
                case ProtocolEmptyException _:
                case InvalidGameMessageException _:
                case InvalidActionException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotImplementedException _:
                    code = HttpStatusCode.NotImplemented;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result).ConfigureAwait(false);
        }
    }
}
