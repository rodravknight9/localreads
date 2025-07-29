using LocalReads.Shared.Domain;

namespace LocalReads.Shared.DataTransfer.Books;

public class ServerBook
{
    public bool IsFavorite { get; set; }
    public double AverageRating { get; set; }
    public int RatingsCount { get; set; }
    public Book Book { get; set; } = new Book();
    public Favorite? Favorite { get; set; } = new Favorite(); 
    public int FavoriteCount { get; set; }
}
