﻿using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using OOD_Project_Backend.User.Business.Contracts;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "text/plain";
                context.Response.ContentLength = Encoding.UTF8.GetByteCount("no token");
                await context.Response.WriteAsync("no token");
                return;
            }

            var token = context.Request.Headers["X-Auth-Token"].FirstOrDefault();
            var isValidToken = await _authenticator.ValidateToken(token);
            if (!isValidToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "text/plain";
                context.Response.ContentLength = Encoding.UTF8.GetByteCount("invalid jwt token");
                await context.Response.WriteAsync("invalid jwt token");
                return;
            }
            context.Items["User"] = _authenticator.FindUserId(token);
        }
        await next(context);
    }
}