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
        var authors = book.VolumeInfo.Authors?
            .Aggregate((a, b) => $"{a}, {b}") ?? "";
        return new BookDto()
        {
            Title = book.VolumeInfo.Title.Length >= 20 
                ? book.VolumeInfo.Title.Substring(0, 20) + "..."
                : book.VolumeInfo.Title,
            Authors = authors.Length >= 20 
                ? authors.Substring(0, 20) 
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