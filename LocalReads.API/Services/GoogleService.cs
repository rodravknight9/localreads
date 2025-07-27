using LocalReads.Shared.DataTransfer.GoogleBooks;
using System.Text.Json;

namespace LocalReads.API.Services;

public class GoogleService : IGoogleService
{
    private readonly HttpClient _httpClient;
    private readonly string[] _randomTerms;
    public GoogleService()
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
    }
    public async Task<IEnumerable<GoogleBook>> GetRandomBooks()
    {
        Random rnd = new Random();
        var randomSearch = rnd.Next(_randomTerms.Length);
        var rawResponse = await _httpClient.GetStringAsync($"?q={_randomTerms[randomSearch]}");
        var root = JsonSerializer.Deserialize<Root>(rawResponse);
        return root!.Items;
    }
    public async Task<IEnumerable<GoogleBook>> GetBySearch(string search)
    {
        var rawResponse = await _httpClient.GetStringAsync($"?q={search}");
        var root = JsonSerializer.Deserialize<Root>(rawResponse);
        return root!.Items;
    }
    public async Task<Root> GetBooksRange(string search, int index, int limit)
    {
        var rawResponse = await _httpClient.GetStringAsync($"?q={search}&startIndex={index}&maxResults={limit}");
        var root = JsonSerializer.Deserialize<Root>(rawResponse);
        return root!;
    }

    public async Task<GoogleBook> GetBookByCode(string bookCode)
    {
        using var client = new HttpClient();
        var rawResponse = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes/{bookCode}");
        var book = JsonSerializer.Deserialize<GoogleBook>(rawResponse);
        return book!;
    }
}
