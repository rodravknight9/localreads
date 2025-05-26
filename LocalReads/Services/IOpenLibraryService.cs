using LocalReads.Models;

namespace LocalReads.Services
{
    public interface IOpenLibraryService
    {
        public Task<IEnumerable<Book>> GetRandomBooks();
        public Task<IEnumerable<Book>> GetBySearch(string search);
    }
}
