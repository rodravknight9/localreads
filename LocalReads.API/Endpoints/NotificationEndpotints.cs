using LocalReads.API.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LocalReads.API.Endpoints;

public static class NotificationEndpotints
{
    public static void MapNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/notifications", async (LocalReadsContext db, HttpContext context) =>
        {
            var userId = (int)context.Items["UserId"]!;
            var test = db.NotificationReads.AsNoTracking()
                .Where(n => n.UserId == userId && n.AlreadyRead == false)
                .Select(n => n.LogActionId)
                .ToList();

            return Results.Ok();
        }).RequireAuthorization();

        app.MapGet("/notifications/week", async (LocalReadsContext db) =>
        {
            var notifications = db.LogActions.AsNoTracking()
                .Where(n => n.ActionTime >= DateTime.UtcNow.AddDays(-7))
                .ToList();

            return Results.Ok(notifications);
        });

        app.MapPost("/notification/read", async (LocalReadsContext db) =>
        {

        }).RequireAuthorization(); 
    }
}