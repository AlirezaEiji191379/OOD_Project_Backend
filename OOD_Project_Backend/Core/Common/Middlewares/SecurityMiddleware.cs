﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using OOD_Project_Backend.User.Business.Abstractions;

namespace OOD_Project_Backend.Core.Common.Middlewares;

public class SecurityMiddleware : IMiddleware
{
    private readonly Authenticator _authenticator;

    public SecurityMiddleware(Authenticator authenticator)
    {
        _authenticator = authenticator;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<IAuthorizationFilter>() != null)
        {
            if (!context.Request.Headers.ContainsKey("X-Auth-Token"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var token = context.Request.Headers["X-Auth-Token"].FirstOrDefault();
            var isValidToken = _authenticator.ValidateToken(token);
            if (!isValidToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            context.Items["User"] = _authenticator.FindUserId(token);
        }
        await next(context);
    }
}