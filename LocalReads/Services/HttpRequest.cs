using System.Text.Json;
using System.Text;
using LocalReads.Models;

namespace LocalReads.Services;

public class HttpRequest : IHttpRequest
{
    private readonly HttpClient _httpClient;
    public HttpRequest()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5033")
        };
    }

    public async Task<HttpResponse<T>> Post<T, Y>(Y entity, string path)
    {
        var httpResult = new HttpResponse<T>();
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

        try
        {
            var result = await _httpClient.PostAsync(path, jsonContent);
            var stringResult = await result.Content.ReadAsStringAsync();
            httpResult.StatusCode = result.StatusCode;
            httpResult.Content = JsonSerializer.Deserialize<T>(stringResult)!;
            httpResult.Success = (int)result.StatusCode >= 200 && (int)result.StatusCode < 300;
            return httpResult;
        }
        catch (Exception e)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = e.Message;
            return httpResult;
        }
    }

    public async Task Post<T>(T entity, string path)
    {
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(path, jsonContent);
    }
}
