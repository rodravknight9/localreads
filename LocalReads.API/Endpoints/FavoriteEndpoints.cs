using LocalReads.API.Context;
using LocalReads.Shared.DataTransfer.Books;
using LocalReads.Shared.Domain;
using LocalReads.Shared.Enums;

namespace LocalReads.API.Endpoints;

public static class FavoriteEndpoints
{
    public static void MapFavoriteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/favorite", async (CreateFavorite favorite, LocalReadsContext db) =>
        {
            var bookExists = db.Books
                .First(b => b.BookGoogleId == favorite.Book.BookGoogleId);

            if(bookExists != null)
                await db.Books.AddAsync(bookExists);

            var newFavorite = new Favorite
            {
                BookId = bookExists.Id,
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
                case (int)BookState.Wishlist:
                    newFavorite.State = (int)BookState.Wishlist;
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
    }
}
