using LocalReads.Shared.Enums;
namespace LocalReads.Shared.DataTransfer.Favorites;

public class UpdateFavoriteState
{
    public int FavoriteId { get; set; }
    public BookState BookState { get; set; } 
}
