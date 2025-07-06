using LocalReads.Shared.DataTransfer.GoogleBooks;

namespace LocalReads.API.Services;

public interface IGoogleService
{
    Task<IEnumerable<GoogleBook>> GetRandomBooks();
    Task<IEnumerable<GoogleBook>> GetBySearch(string search);
    Task<Root> GetBooksRange(string search, int index, int limit);
}
