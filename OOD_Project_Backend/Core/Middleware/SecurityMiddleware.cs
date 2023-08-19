using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OOD_Project_Backend.User.Business.Contracts;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace OOD_Project_Backend.Core.Middleware;

public class SecurityMiddleware : IMiddleware
{
    private readonly IAuthenticator _authenticator;
    
    public SecurityMiddleware(IAuthenticator authenticator)
    {
        _authenticator = authenticator;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<IAuthorizationFilter>() != null)
        {
            if (!context.Request.Headers.ContainsKey("X-Auth-Token"))
            {
                Log.Logger.Information("the X-Auth-Token not setted!");
                var token1 = context.Request.Headers["X-Auth-Token"].FirstOrDefault();
                Log.Logger.Information(token1);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var message = new { Message = "you do not have token! login again!" };
                var response = JsonConvert.SerializeObject(message);
                context.Response.ContentType = "application/json";
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(response);
                await context.Response.WriteAsync(response);
                return;
            }

            var token = context.Request.Headers["X-Auth-Token"].FirstOrDefault();
            var isValidToken = await _authenticator.ValidateToken(token);
            if (!isValidToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var message = new { Message = "invalid token! login again!" };
                var response = JsonConvert.SerializeObject(message);
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(response);
                await context.Response.WriteAsync(response);
                return;
            }
            context.Items["User"] = _authenticator.FindUserId(token);
        }
        await next(context);
    }
}