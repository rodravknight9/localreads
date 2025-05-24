
namespace OpenLibraryBlazorApp.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private readonly HttpClient _httpClient;
        public OpenLibraryService()
        {
            _httpClient = new HttpClient() 
            {
                BaseAddress = new Uri("https://openlibrary.org")
            };
        }
        public Task GetRandomBooks()
        {
            throw new NotImplementedException();
        }
    }
}
