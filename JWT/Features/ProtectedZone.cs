using System.Security.Claims;
using Carter;
using Microsoft.AspNetCore.Authorization;

namespace JWT.Features;

public class ProtectedZone: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var appGroup = app.MapGroup("protected");
        
        appGroup.MapGet("/me", (HttpContext context) =>
        {
            var email = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return Results.Ok($"Hello {email}");
        }).RequireAuthorization();
    }
}