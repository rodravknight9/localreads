namespace LocalReads.Shared.Domain;

public class Star
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Value { get; set; }
    public User User { get; set; }
    public Book Book { get; set; }
}
