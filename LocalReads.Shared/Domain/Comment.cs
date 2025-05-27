namespace LocalReads.Shared.Domain;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int CommentId { get; set; }
    public string Text { get; set; } = string.Empty;
    public Book Book { get; set; }
    public User User { get; set; }
}
