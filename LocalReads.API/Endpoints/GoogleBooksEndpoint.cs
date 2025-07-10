using LocalReads.API.Context;
using LocalReads.API.Services;
using LocalReads.Shared.DataTransfer.Books;
using LocalReads.Shared.Domain;
using MapsterMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        app.MapGet("/googlebooks/book/{bookCode}", async (string bookCode, IGoogleService googleService, LocalReadsContext db, IMapper mapper) => {

            var serverBook = new ServerBook();

            var book = await db.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookGoogleId == bookCode);

            if (book != null)
            {
                serverBook.Book = book;
                serverBook.IsFavorite = true;
                serverBook.RatingsCount = db.Favorites.Count(fav => fav.Rating > 0);
                if (serverBook.RatingsCount > 0)
                { 
                    serverBook.AverageRating = db.Favorites
                        .Where(fav => fav.Rating > 0)
                        .Average(fav => fav.Rating);
                }
                return Results.Ok(serverBook);
            }

            var googleBook = await googleService.GetBookByCode(bookCode);
            serverBook.Book = mapper.Map<Book>(googleBook);

            return Results.Ok(googleBook);
        });

    }
}
