namespace LocalReads.Shared.DataTransfer.Books;

public class PopularBooks
{
    public List<ServerBook> MostReadBooks { get; set; } = [];
    public List<ServerBook> MostRatedBooks { get; set; } = [];
}
