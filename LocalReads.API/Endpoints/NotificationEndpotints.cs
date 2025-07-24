using LocalReads.API.Context;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LocalReads.API.Endpoints;

public static class NotificationEndpotints
{
    public static void MapNotificationsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/googlebooks/random", async (LocalReadsContext db) =>
        {
            
            return Results.Ok();
        });
    }
}