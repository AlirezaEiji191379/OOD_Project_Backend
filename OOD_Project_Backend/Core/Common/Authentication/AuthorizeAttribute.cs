using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Core.Common.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = (int)context.HttpContext.Items["User"];
        if (userId == null)
        {
            Console.WriteLine("on authorization!");
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized,Value = "the user does not have id"};
        }
    }
}