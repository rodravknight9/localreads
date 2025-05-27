using System.Text.Json;
using LocalReads.Models;
using LocalReads.State;

namespace LocalReads.Services;

public class OpenLibraryService : IOpenLibraryService
{
    private readonly HttpClient _httpClient;
    private readonly string[] _randomTerms;
    private readonly AppState _appState;
    public OpenLibraryService(AppState appState)
    {
        _httpClient = new HttpClient() 
        {
            BaseAddress = new Uri("https://www.googleapis.com/books/v1/volumes")
        };
        _randomTerms =
        [
            "Albert Camus", "James Joyce", "JK Rowling", "Humberto Eco",
            "Harry Potter", "Libro de la Selva", "Maestro y Margarita",
            "George Orwell"
        ];
        _appState = appState;
    }
    public async Task<IEnumerable<Book>> GetRandomBooks()
    {
        Random rnd = new Random();
        var randomSearch = rnd.Next(rnd.Next(_randomTerms.Length));
        _appState.SearchResults.TermSearch = _randomTerms[randomSearch];
        var rawResponse = await _httpClient.GetStringAsync($"?q={_randomTerms[randomSearch]}");
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
