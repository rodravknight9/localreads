using LocalReads.Models;

namespace LocalReads.DTO;

public class BookDto
{
    public string BookGoogleId { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public string PublishedDate { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public int PageCount { get; set; }
    public string Categories { get; set; }
    public string ImageLink { get; set; }
    public string Language { get; set; }

    //TODO: too much ambiguity in Books classes
    public static BookDto FromBook(LocalReads.Models.Book book)
    {
        return new BookDto()
        {
            Title = book.VolumeInfo.Title,
            Authors = book.VolumeInfo.Authors?.Aggregate((a, b) => $"{a}, {b}") ?? "",
            Publisher = book.VolumeInfo?.Publisher ?? "",
            PublishedDate = book.VolumeInfo?.PublishedDate ?? "",
            Description = book.VolumeInfo?.Description ?? "",
            PageCount = book.VolumeInfo?.PageCount ?? 0,
            Categories = book.VolumeInfo.Categories?.Aggregate((a, b) => $"{a}, {b}") ?? "",
            ImageLink = GetImageUrl(book.VolumeInfo.ImageLinks),
            Language = book.VolumeInfo?.Language ?? "",
            BookGoogleId = book.Id
        };
    }

    public static Shared.Domain.Book ToBookDomain(BookDto book)
    {
        return new Shared.Domain.Book
        {
            Authors = book.Authors,
            Categories = book.Categories,
            Description = book.Description,
            ImageLink = book.ImageLink,
            BookGoogleId = book.BookGoogleId,
            Language = book.Language,
            PageCount = book.PageCount,
            PublishedDate = book.PublishedDate,
            Publisher = book.Publisher,
            Title = book.Title
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