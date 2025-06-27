namespace LocalReads.Shared.Domain;

public class Favorite
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int State { get; set; }
    public DateTime ReadTime { get; set; }
    public int Progress { get; set; }
    public double Rating { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
}
