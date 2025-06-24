using LocalReads.Models;

namespace LocalReads.DTO;

public class BookDto
{
    public string Title { get; set; }
    public string Authors { get; set; }
    public string PublishedDate { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public int PageCount { get; set; }
    public string Categories { get; set; }
    public string ImageLink { get; set; }
    public string Language { get; set; }

    public static BookDto FromBook(Book book)
    {
        const int limit = 50;
        var authors = book.VolumeInfo.Authors?
            .Aggregate((a, b) => $"{a}, {b}") ?? "";
        return new BookDto()
        {
            Title = book.VolumeInfo.Title.Length >= limit 
                ? book.VolumeInfo.Title.Substring(0, limit) + "..."
                : book.VolumeInfo.Title,
            Authors = authors.Length >= limit 
                ? authors.Substring(0, limit) 
                : authors + "...",
            Publisher = book.VolumeInfo?.Publisher ?? "",
            PublishedDate = book.VolumeInfo?.PublishedDate ?? "",
            Description = book.VolumeInfo?.Description ?? "",
            PageCount = book.VolumeInfo.PageCount,
            Categories = book.VolumeInfo.Categories?.Aggregate((a, b) => $"{a}, {b}") ?? "",
            ImageLink = GetImageUrl(book.VolumeInfo.ImageLinks),
            Language = book.VolumeInfo?.Language ?? ""
        };
    }

    private static string GetImageUrl(ImageLinks imageLinks)
    {
        if(imageLinks is null)
            return string.Empty;
        
        if (imageLinks.Medium is not null)
        {
            return imageLinks.Medium;
        }
        else if (imageLinks.Small is not null)
        {
            return imageLinks.Small;
        }
        else if (imageLinks.Thumbnail is not null)
        {
            return imageLinks.Thumbnail;
        }
        
        return String.Empty;
    }
}