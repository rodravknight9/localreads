using LocalReads.API.Configurations;
using LocalReads.API.Context;
using LocalReads.Shared.DataTransfer.User;
using LocalReads.Shared.Domain;
using LocalReads.Shared.Enums;
using MapsterMapper;
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

        app.MapGet("/users", (LocalReadsContext db, IMapper mapper) =>
        {
            var users = db.Users.AsNoTracking().ToList();
            var usersResponse = users.Select(u => 
            {
                var mappedUser = mapper.Map<UserResponse>(u);
                mappedUser.CurrentlyReading = 
                    db.Favorites.Count(fav => fav.User.Id == u.Id && fav.State == (int)BookState.InProgress);
                mappedUser.FavoriteBooksCount = db.Favorites.Count(fav => fav.User.Id == u.Id);
                return mappedUser;
            });
            return usersResponse;
        });

        app.MapGet("/user/{userId}", async (int userId, LocalReadsContext db, IMapper mapper) =>
        {
            var user = await db.Users.SingleAsync(u => u.Id == userId);

            var userResponse = mapper.Map<UserResponse>(user);
            userResponse.FavoriteBooksCount = 
                db.Favorites.Count(fav => fav.User.Id == userId && fav.Rating > 0);
            if (userResponse.FavoriteBooksCount > 0)
            { 
                userResponse.AverageRating = 
                    db.Favorites.Where(fav => fav.User.Id == userId && fav.Rating > 0).Average(fav => fav.Rating);
            }
            return userResponse;
        });

        app.MapPut("/user", async (User user, LocalReadsContext db) =>
        {
            var existingUser = await db.Users.AsNoTracking().SingleAsync(u => u.Id == user.Id);
            user.Password = existingUser.Password;
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return Results.NoContent();
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
