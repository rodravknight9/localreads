using LocalReads.API.Services;

namespace LocalReads.API.Endpoints;

public static class GoogleBooksEndpoint
{
    public static void MapGoogleBooksEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/googlebooks/random", async (IGoogleService googleService) => {
            return await googleService.GetRandomBooks();
        });

        app.MapGet("/googlebooks/search/{search}", async (string search, IGoogleService googleService) => {
            return await googleService.GetBySearch(search);
        });

        app.MapGet("/googlebooks/range/{search}/{index}/{limit}", async (string search, int index, int limit, IGoogleService googleService) => {
            return await googleService.GetBooksRange(search, index, limit);
        });
    }
}
