using LocalReads.Models;
using LocalReads.Shared.DataTransfer;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LocalReads.Services;

public class HttpRequest : HttpClient, IHttpRequest
{
    private readonly HttpClient _httpClient;
    public HttpRequest(HttpClient  httpClient)
    {
        _httpClient = httpClient;
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


    public async Task<SimpleHttpResponse> SimplePost<T>(T entity, string path)
    {
        var httpResult = new SimpleHttpResponse();
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

        try
        {
            var result = await _httpClient.PostAsync(path, jsonContent);
            var stringResult = await result.Content.ReadAsStringAsync();
            httpResult.StatusCode = result.StatusCode;
            httpResult.Success = (int)result.StatusCode >= 200 && (int)result.StatusCode < 300;

            if (!httpResult.Success) 
            {
                httpResult.ErrorMessage = await result.Content.ReadAsStringAsync();
            }

            return httpResult;
        }
        catch (Exception e)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = e.Message;
            return httpResult;
            throw;
        }
    }

    public async Task<HttpResponse<T>> Get<T>(string path)
    {
        var httpResult = new HttpResponse<T>();
        try
        {
            var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
            var result = await _httpClient.GetAsync(path);
            var stringResult = await result.Content.ReadAsStringAsync();
            httpResult.Content = JsonSerializer.Deserialize<T>(stringResult, options)!;
            return httpResult;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<SimpleHttpResponse> Delete(string path)
    {
        var httpResult = new SimpleHttpResponse();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var result = await _httpClient.DeleteAsync(path);
        httpResult.Success = result.IsSuccessStatusCode; 
        return httpResult;
    }

    public async Task<SimpleHttpResponse> SimplePut<T>(T entity, string path)
    {
        var httpResult = new SimpleHttpResponse();
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

        try
        {
            var result = await _httpClient.PutAsync(path, jsonContent);
            var stringResult = await result.Content.ReadAsStringAsync();
            httpResult.StatusCode = result.StatusCode;
            httpResult.Success = (int)result.StatusCode >= 200 && (int)result.StatusCode < 300;

            if (!httpResult.Success) 
            {
                httpResult.ErrorMessage = await result.Content.ReadAsStringAsync();
            }

            return httpResult;
        }
        catch (Exception e)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = e.Message;
            return httpResult;
            throw;
        }
    }

    public async Task<SimpleHttpResponse> SimplePatch<T>(T entity, string path)
    {
        var httpResult = new SimpleHttpResponse();
        using StringContent jsonContent = new(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

        try
        {
            var result = await _httpClient.PatchAsync(path, jsonContent);
            var stringResult = await result.Content.ReadAsStringAsync();
            httpResult.StatusCode = result.StatusCode;
            httpResult.Success = (int)result.StatusCode >= 200 && (int)result.StatusCode < 300;

            if (!httpResult.Success)
            {
                httpResult.ErrorMessage = await result.Content.ReadAsStringAsync();
            }

            return httpResult;
        }
        catch (Exception e)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = e.Message;
            return httpResult;
            throw;
        }
    }
}
