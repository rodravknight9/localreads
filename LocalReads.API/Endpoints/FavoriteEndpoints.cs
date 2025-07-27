using LocalReads.API.Context;
using LocalReads.Shared.Constants;
using LocalReads.Shared.DataTransfer;
using LocalReads.Shared.DataTransfer.Books;
using LocalReads.Shared.DataTransfer.Favorites;
using LocalReads.Shared.Domain;
using LocalReads.Shared.Enums;
using LocalReads.Shared.Errors;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalReads.API.Endpoints;

public static class FavoriteEndpoints
{
    public static void MapFavoriteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/favorite", async (CreateFavorite favorite, LocalReadsContext db, HttpContext context) =>
        {
            string bookList = string.Empty;
            var addFavResponse = new AddFavortiteResponse();
            var httpLocalReadsResponse = new HttpLocalReadsResponse<AddFavortiteResponse>
            {
                Data = addFavResponse
            };

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
                    bookList = nameof(BookState.InProgress);
                    newFavorite.State = (int)BookState.InProgress;
                    newFavorite.Progress = favorite.Progress;
                    break;
                case (int)BookState.AlreadyRead:
                    bookList = nameof(BookState.AlreadyRead);
                    newFavorite.State = (int)BookState.AlreadyRead;
                    newFavorite.Progress = 100;
                    newFavorite.ReadTime = favorite.ReadTime;
                    break;
                case (int)BookState.WantToRead:
                    bookList = nameof(BookState.WantToRead);
                    newFavorite.State = (int)BookState.WantToRead;
                    newFavorite.Progress = 0;
                    break;
                case (int)BookState.Abandoned:
                    bookList = nameof(BookState.Abandoned);
                    newFavorite.State = (int)BookState.Abandoned;
                    break;
            }

            if (favorite.OverrideState)
            { 
                var oldFavorite = db.Favorites
                    .First(fav => fav.UserId == favorite.UserId 
                        && fav.Book.BookGoogleId == bookExists.BookGoogleId);

                oldFavorite.State = favorite.State;
                oldFavorite.Progress = favorite.Progress;
                oldFavorite.ReadTime = favorite.ReadTime;

                db.Favorites.Update(oldFavorite);
                db.SaveChanges();

                httpLocalReadsResponse.HasServerFeedback = false;
                httpLocalReadsResponse.IsSuccess = true;
                httpLocalReadsResponse.Data.FavortiteId = oldFavorite.Id;

                return Results.Ok(httpLocalReadsResponse);
            }

            bool isFavorite = db.Favorites
                    .Any(fav => fav.UserId == favorite.UserId 
                        && fav.Book.BookGoogleId == bookExists.BookGoogleId);

            if (isFavorite)
            {
                var overrideBook = db.Favorites
                    .Where(fav => fav.UserId == favorite.UserId 
                        && fav.Book.BookGoogleId == bookExists.BookGoogleId)
                    .AsNoTracking()
                    .Select(fav => (BookState)fav.State)
                    .FirstOrDefault();

                httpLocalReadsResponse.HasServerFeedback = true;
                httpLocalReadsResponse.IsSuccess = false;
                httpLocalReadsResponse.Code = LocalReadsErrors.AlreadyFavorited;
                httpLocalReadsResponse.ServerMessage = $"You already have this book in your '{bookList}' list." +
                    $"Do you want to move it to the '{nameof(newFavorite.State)}'?";

                return Results.BadRequest(httpLocalReadsResponse);
            }

            // create a new favorite entry
            await db.AddAsync(newFavorite);
            await db.SaveChangesAsync();

            var logAction = new LogAction
            {
                Action = string.Format(LogActionConstants.FavoriteAdded, 
                    context.Items["UserName"], favorite.Book.Title, bookList),
                Table = nameof(Favorite),
                RecordId = newFavorite.Id,
                ActionTime = DateTime.Now,
                UserId = favorite.UserId
            };

            db.LogActions.Add(logAction);
            db.SaveChanges();

            httpLocalReadsResponse.HasServerFeedback = false;
            httpLocalReadsResponse.IsSuccess = true;
            httpLocalReadsResponse.Data.FavortiteId = newFavorite.Id;

            return Results.Ok(httpLocalReadsResponse);

        }).RequireAuthorization();

        app.MapGet("/favorites", ([FromQuery] string type, LocalReadsContext db, HttpContext context, IMapper mapper) =>
        {
            var filterType = type switch
            {
                nameof(BookState.InProgress) => (int)BookState.InProgress,
                nameof(BookState.AlreadyRead) => (int)BookState.AlreadyRead,
                nameof(BookState.Abandoned) => (int)BookState.Abandoned,
                nameof(BookState.WantToRead) => (int)BookState.WantToRead,
                _ => 0
            };

            var userId = (int)context.Items["UserId"]!;

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
        }).RequireAuthorization();

        app.MapPost("/favorite/rate", async (RateBook bookRate, LocalReadsContext db, HttpContext context) =>
        {
            var favorite = await db.Favorites.SingleAsync(fav => fav.UserId == bookRate.UserId && fav.Id == bookRate.FavoriteId);
            if (favorite != null)
            { 
                favorite.Rating = bookRate.Rating;
                db.SaveChanges();
            }

            var user = context.Items["userId"];

            return Results.Ok();
        }).RequireAuthorization();

        app.MapDelete("/favorite/{favoriteId}", async (int favoriteId, LocalReadsContext db) =>
        {
            var fav = await db.Favorites.SingleAsync(fav => fav.Id == favoriteId);
            db.Favorites.Remove(fav);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).RequireAuthorization();

        app.MapPatch("/favorite", async (SaveProgress progress, LocalReadsContext db) =>
        {
            var fav = await db.Favorites
                .AsNoTracking()
                .SingleAsync(fav
                    => fav.UserId == progress.UserId && fav.Id == progress.FavoriteId);

            fav.Progress = progress.Progress;
            db.Favorites.Update(fav);
            db.SaveChanges();
        }).RequireAuthorization();

        app.MapGet("/favorite/inprogress", (LocalReadsContext db, HttpContext context) =>
        {
            var userId = (int)context.Items["UserId"]!;

            return db.Favorites
                .AsNoTracking()
                .Include(fav => fav.Book)
                .Where(fav => fav.UserId == userId && fav.State == (int)BookState.InProgress);
        }).RequireAuthorization();
    }
}
