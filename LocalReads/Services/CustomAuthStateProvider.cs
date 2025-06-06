using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using LocalReads.Shared.DataTransfer.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace LocalReads.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    public CustomAuthStateProvider(ILocalStorageService  localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _httpClient = http;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string authToken = await _localStorage.GetItemAsStringAsync("userState"); 

        var identity = new ClaimsIdentity();
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(authToken))
        {
            try
            {
                AuthResponse deserializedState = JsonSerializer.Deserialize<AuthResponse>(authToken);
                string token = deserializedState.Jwt;
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",  token.Replace("\"", ""));
            }
            catch
            {
                await _localStorage.RemoveItemAsync("userState");
                identity = new ClaimsIdentity();
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private byte[] ParseBase64WithoutPadding(string token)
    {
        switch (token.Length %4)
        {
            case 2: token += "=="; break;
            case 3: token += "="; break;
        }
        return Convert.FromBase64String(token);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var payload = token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        var  claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        return claims;
    }
}