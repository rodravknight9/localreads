using System.Text.Json;
using LocalReads.Models;

namespace LocalReads.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private readonly HttpClient _httpClient;
        public OpenLibraryService()
        {
            _httpClient = new HttpClient() 
            {
                BaseAddress = new Uri("https://www.googleapis.com/books/v1/volumes")
            };
        }
        public async Task<IEnumerable<Book>> GetRandomBooks()
        {
            var rawResponse = await _httpClient.GetStringAsync("?q=harry");
            var root = JsonSerializer.Deserialize<Root>(rawResponse);
            return root!.Items;
        }

        public async Task<IEnumerable<Book>> GetBySearch(string search)
        {
            var rawResponse = await _httpClient.GetStringAsync($"?q={search}");
            var root = JsonSerializer.Deserialize<Root>(rawResponse);
            return root!.Items;
        }
    }
}
