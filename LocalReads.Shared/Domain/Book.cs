namespace LocalReads.Shared.Domain;

public class Book
{
    public int Id { get; set; }
    public string BookGoogleId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Authors { get; set; } = string.Empty;
    public string? PublishedDate { get; set; } = string.Empty;
    public string? Publisher { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string? Categories { get; set; } = string.Empty;
    public string? ImageLink { get; set; } = string.Empty;
    public string? Language { get; set; } = string.Empty;
    public string? LargestImageLink { get; set; } = string.Empty;
}