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
                var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

                if (userId != null && int.TryParse(userId, out int userIdNum))
                {
                    context.Items["UserId"] = userIdNum;
                }
                else
                {
                    context.Items["UserId"] = 0;
                }

                if (userName != null)
                {
                    context.Items["UserName"] = userName;
                }
                else
                {
                    context.Items["UserName"] = "Unknown User";
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
