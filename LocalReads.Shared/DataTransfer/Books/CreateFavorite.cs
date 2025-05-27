using LocalReads.Shared.Domain;

namespace LocalReads.Shared.DataTransfer.Books;

public class CreateFavorite
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int State { get; set; }
    public int Progress { get; set; }
    public DateTime ReadTime { get; set; }
    public Book Book { get; set; }
}
