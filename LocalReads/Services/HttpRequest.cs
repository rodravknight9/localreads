using System.Text.Json;
using System.Text;

namespace LocalReads.Services;

public class HttpRequest : IHttpRequest
{
    private readonly HttpClient _httpClient;
    public HttpRequest()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7223")
        };
    }

    public async Task<T> Post<T, Y>(Y entity, string path)
    {
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(path, jsonContent);
        var stringResult = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(stringResult)!;
    }

    public async Task Post<T>(T entity, string path)
    {
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(path, jsonContent);
    }
}
