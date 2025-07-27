using LocalReads.Models;
using LocalReads.Shared.DataTransfer;
using LocalReads.Shared.DataTransfer.User;
using LocalReads.Shared.Errors;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LocalReads.Services;

public class HttpRequest : IHttpRequest
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _js;
    private readonly NavigationManager _navigationManager;
    private readonly ISnackbar _snackbar;

    public HttpRequest(HttpClient httpClient, IJSRuntime js, NavigationManager navigationManager, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _js = js;
        _navigationManager = navigationManager;
        _snackbar = snackbar;
    }

    public async Task<HttpResponse<T>> Post<T, Y>(Y entity, string path)
    {
        return await SendRequest<T>(HttpMethod.Post, path, entity);
    }

    public async Task Post<T>(T entity, string path)
    {
        await SetJwtIntoClient();
        var content = CreateJsonContent(entity);
        var response = await _httpClient.PostAsync(path, content);
        if ((int)response.StatusCode == 401)
        {
            await HandleLogOut();
            return;
        }
    }

    public async Task<SimpleHttpResponse> SimplePost<T>(T entity, string path)
    {
        return await SendSimpleRequest(HttpMethod.Post, path, entity);
    }

    public async Task<HttpResponse<T>> Get<T>(string path)
    {
        var httpResult = new HttpResponse<T>();

        try
        {
            await SetJwtIntoClient();
            var response = await _httpClient.GetAsync(path);

            if ((int)response.StatusCode == 401)
            {
                await HandleLogOut();
                _snackbar.Add("Session Expired", Severity.Warning);
                httpResult.Success = false;
                return httpResult;
            }

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            httpResult.Content = JsonSerializer.Deserialize<T>(content, options)!;
            httpResult.StatusCode = response.StatusCode;
            httpResult.Success = response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = ex.Message;
        }

        return httpResult;
    }

    public async Task<SimpleHttpResponse> Delete(string path)
    {
        var httpResult = new SimpleHttpResponse();
        await SetJwtIntoClient();
        var response = await _httpClient.DeleteAsync(path);
        if ((int)response.StatusCode == 401)
        {
            await HandleLogOut();
            _snackbar.Add("Session Expired", Severity.Warning);
            httpResult.Success = false;
            return httpResult;
        }
        return new SimpleHttpResponse
        {
            Success = response.IsSuccessStatusCode,
            StatusCode = response.StatusCode
        };
    }

    public async Task<SimpleHttpResponse> SimplePut<T>(T entity, string path)
    {
        return await SendSimpleRequest(HttpMethod.Put, path, entity);
    }

    public async Task<SimpleHttpResponse> SimplePatch<T>(T entity, string path)
    {
        return await SendSimpleRequest(new HttpMethod("PATCH"), path, entity);
    }

    private async Task<HttpResponse<T>> SendRequest<T>(HttpMethod method, string path, object body)
    {
        var httpResult = new HttpResponse<T>();

        try
        {
            await SetJwtIntoClient();
            var request = new HttpRequestMessage(method, path)
            {
                Content = CreateJsonContent(body)
            };

            var response = await _httpClient.SendAsync(request);
            var stringContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            httpResult.StatusCode = response.StatusCode;
            httpResult.Success = response.IsSuccessStatusCode;
            httpResult.Content = JsonSerializer.Deserialize<T>(stringContent, options)!;
        }
        catch (Exception ex)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = ex.Message;
        }

        return httpResult;
    }

    private async Task<SimpleHttpResponse> SendSimpleRequest(HttpMethod method, string path, object body)
    {
        var httpResult = new SimpleHttpResponse();

        try
        {
            await SetJwtIntoClient();
            var request = new HttpRequestMessage(method, path)
            {
                Content = CreateJsonContent(body)
            };

            var response = await _httpClient.SendAsync(request);
            httpResult.StatusCode = response.StatusCode;
            httpResult.Success = response.IsSuccessStatusCode;

            if (!httpResult.Success)
            {
                httpResult.ErrorMessage = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            httpResult.Success = false;
            httpResult.ErrorMessage = ex.Message;
            _snackbar.Add(httpResult.ErrorMessage, Severity.Error);
        }

        return httpResult;
    }


    public async Task<HttpLocalReadsResponse<TResponse>> SendPost<TRequest, TResponse>(TRequest entity, string path)
    {
        return await SendLocalReadsRequest<TResponse>(HttpMethod.Post, path, entity!);
    }

    private async Task<HttpLocalReadsResponse<TResponse>> SendLocalReadsRequest<TResponse>(HttpMethod method, string path, object body)
    {
        var httpResult = new HttpLocalReadsResponse<TResponse>();

        try
        {
            await SetJwtIntoClient();
            var request = new HttpRequestMessage(method, path)
            {
                Content = CreateJsonContent(body)
            };

            var response = await _httpClient.SendAsync(request);
            var stringContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            httpResult = JsonSerializer.Deserialize<HttpLocalReadsResponse<TResponse>>(stringContent, options)!;
            
        }
        catch (Exception ex)
        {
            httpResult.IsSuccess = false;
            httpResult.Code = LocalReadsErrors.UnknownError;
            httpResult.ServerMessage = $"Unkown Exception: {ex.Message}";
            _snackbar.Add(httpResult.ServerMessage, Severity.Error);
        }

        return httpResult;
    }

    private static StringContent CreateJsonContent(object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<string> GetUserToken()
    {
        var tokenJson = await _js.InvokeAsync<string>("localStorage.getItem", "userState");

        if (string.IsNullOrEmpty(tokenJson))
            return string.Empty;

        var user = JsonSerializer.Deserialize<AuthResponse>(tokenJson);
        return user?.Jwt ?? string.Empty;
    }

    private async Task SetJwtIntoClient()
    {
        var token = await GetUserToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    private async Task HandleLogOut()
    {
        await _js.InvokeAsync<string>("localStorage.removeItem", "userState");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        _navigationManager.NavigateTo("/login");
    }
}
