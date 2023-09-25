using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Carter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Features;

public record LoginRequest(string Email, string Password);

public class Security: ICarterModule
{
    private readonly JwtOptions _jwtOptions;
    public Security(IOptions<JwtOptions> options) => _jwtOptions = options.Value;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var appGroup = app.MapGroup("security");
        
        appGroup.MapPost("/login", async (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, [FromBody]LoginRequest request) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Results.BadRequest();

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if(result.Succeeded is false)
                return Results.BadRequest();
            
            var token = GetToken(user, _jwtOptions);
            return Results.Ok(token);
        }).AllowAnonymous();
    }

    private static string GetToken(IdentityUser user, JwtOptions jwtOptionsValue)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsValue.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: jwtOptionsValue.Issuer,
            audience: jwtOptionsValue.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(720),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return jwt;
    }
}