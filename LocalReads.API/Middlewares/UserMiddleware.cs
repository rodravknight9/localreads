using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LocalReads.API.Middlewares;

public class UserMiddleware
{
    private readonly RequestDelegate _next;
    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

                if (userId != null)
                {
                    context.Items["UserId"] = new { userId = userId };
                }
            }
            catch
            {
                await _next(context);
            }
        }

        await _next(context);
    }
}
