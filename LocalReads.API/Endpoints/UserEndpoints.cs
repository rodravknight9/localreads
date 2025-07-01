using LocalReads.API.Configurations;
using LocalReads.API.Context;
using LocalReads.Shared.DataTransfer.User;
using LocalReads.Shared.Domain;
using LocalReads.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocalReads.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/register", async (RegisterUser request, LocalReadsContext db) =>
        {
            var existingUsers = db.Users.Where(u => u.UserName == request.UserName);
            if (existingUsers.Any())
                return Results.BadRequest("User already exists");

            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new User
            {
                UserName = request.UserName,
                MemberSince = DateTime.Today,
                Name = request.Name,
                Password = password
            };
            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();

            return Results.Created();
        });

        app.MapPost("/users/login", async (Login request, LocalReadsContext db, IOptions<JwtSettings> jwtSettings) =>
        {
            var user = await db.Users
                .Where(u => u.UserName == request.UserName)
                .FirstOrDefaultAsync();

            if (user == null)
                return Results.BadRequest("User does not exist");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Results.BadRequest("Wrong password");


            var response = new AuthResponse 
            {
                UserName = user.UserName,
                Id = user.Id,
                Name = user.Name,
                Jwt = GenerateJwtToken(user, jwtSettings.Value)
            };
            return Results.Created($"/users/{user.Id}", response);
        });

        app.MapGet("/users", (LocalReadsContext db) =>
        {
            var users = db.Users.AsNoTracking().ToList();
            var userResponse = users.Select(u => new UserResponse
            {
                Id = u.Id,
                BirthDate = u.BirthDate,
                Name = u.Name,
                Location = u.Location,
                MemberSince = u.MemberSince,
                PersonalIntroduction = u.PersonalIntroduction,
                UserName = u.UserName,
                CurrentlyReading = db.Favorites.Count(fav => fav.User.Id == u.Id && fav.State == (int)BookState.InProgress),
                FavoriteBooksCount = db.Favorites.Count(fav => fav.User.Id == u.Id)
            });
            return userResponse;
        });
    }

    private static string GenerateJwtToken(User user, JwtSettings jwtSettings)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            }),
            //Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
