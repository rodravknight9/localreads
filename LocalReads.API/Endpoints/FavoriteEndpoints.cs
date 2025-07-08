using LocalReads.API.Context;
using LocalReads.Shared.DataTransfer.Books;
using LocalReads.Shared.DataTransfer.Favorites;
using LocalReads.Shared.Domain;
using LocalReads.Shared.Enums;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalReads.API.Endpoints;

public static class FavoriteEndpoints
{
    public static void MapFavoriteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/favorite", async (CreateFavorite favorite, LocalReadsContext db) =>
        {
            var bookExists = db.Books
                .FirstOrDefault(b => b.BookGoogleId == favorite.Book.BookGoogleId);

            if (bookExists == null) 
            {
                await db.Books.AddAsync(favorite.Book);
                await db.SaveChangesAsync();
                bookExists = db.Books
                    .FirstOrDefault(b => b.BookGoogleId == favorite.Book.BookGoogleId);
            }

            var newFavorite = new Favorite
            {
                BookId = bookExists!.Id,
                UserId = favorite.UserId                
            };

            switch (favorite.State) 
            {
                case (int)BookState.InProgress:
                    newFavorite.State = (int)BookState.InProgress;
                    newFavorite.Progress = favorite.Progress;
                    break;
                case (int)BookState.AlreadyRead:
                    newFavorite.State = (int)BookState.AlreadyRead;
                    newFavorite.Progress = 100;
                    newFavorite.ReadTime = favorite.ReadTime;
                    break;
                case (int)BookState.WantToRead:
                    newFavorite.State = (int)BookState.WantToRead;
                    newFavorite.Progress = 0;
                    break;
                case (int)BookState.Abandoned:
                    newFavorite.State = (int)BookState.Abandoned;
                    break;
            }

            await db.AddAsync(newFavorite);
            db.SaveChanges();

            return Results.Created();
        });

        app.MapGet("/favorites/{userId}", async ([FromQuery] string type, [FromRoute] int userId, LocalReadsContext db, IMapper mapper) =>
        {
            var filterType = type switch
            {
                nameof(BookState.InProgress) => (int)BookState.InProgress,
                nameof(BookState.AlreadyRead) => (int)BookState.AlreadyRead,
                nameof(BookState.Abandoned) => (int)BookState.Abandoned,
                nameof(BookState.WantToRead) => (int)BookState.WantToRead,
                _ => 0
            };

            var favorites = db.Favorites
                .AsNoTracking()
                .Include(fav => fav.Book)
                .Where(fav => fav.State == filterType && fav.UserId == userId);

            var favsInServer = db.Favorites
                .AsNoTracking()
                .Where(favInSrvr => favorites.Any(fav => fav.BookId.Equals(favInSrvr.BookId)));

            var favsToRespond = mapper.Map<IEnumerable<GetFavorite>>(favorites).ToList();

            foreach (var fav in favsToRespond)
            {
                fav.AverageRating = favsInServer.Where(favsIn => favsIn.BookId == fav.BookId).Average(favsIn => favsIn.Rating);
            }

            return favsToRespond;
        });

        app.MapPost("/favorite/rate", async (RateBook bookRate, LocalReadsContext db) =>
        {
            var favorite = await db.Favorites.SingleAsync(fav => fav.UserId == bookRate.UserId && fav.Id == bookRate.FavoriteId);
            if (favorite != null)
            { 
                favorite.Rating = bookRate.Rating;
                db.SaveChanges();
            }

            return Results.Ok();
        });

        app.MapDelete("/favorite/{favoriteId}", async (int favoriteId, LocalReadsContext db) =>
        {
            var fav = await db.Favorites.SingleAsync(fav => fav.Id == favoriteId);
            db.Favorites.Remove(fav);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapPatch("/favorite", async (SaveProgress progress, LocalReadsContext db) =>
        {
            var fav = await db.Favorites
                .AsNoTracking()
                .SingleAsync(fav
                    => fav.UserId == progress.UserId && fav.Id == progress.FavoriteId);

            fav.Progress = progress.Progress;
            db.Favorites.Update(fav);
            db.SaveChanges();
        });

        app.MapGet("/favorite/inprogress/{userId}", (int userId, LocalReadsContext db) =>
        {
            return db.Favorites
                .AsNoTracking()
                .Include(fav => fav.Book)
                .Where(fav => fav.UserId == userId && fav.State == (int)BookState.InProgress);
        });
    }
}
