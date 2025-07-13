using System.Text.Json;
using Blazored.LocalStorage;
using LocalReads.Shared.DataTransfer.User;
using LocalReads.Shared.Domain;
using LocalReads.State;
using Microsoft.AspNetCore.Components.Authorization;

namespace LocalReads.Services;

public class AuthService : IAuthService
{
    private readonly IHttpRequest _httpRequest;
    private readonly ILocalStorageService _localStorage;
    private readonly AppState _appState;
    private readonly AuthenticationStateProvider _authenticationStateProvider; 
    public AuthService(
        IHttpRequest httpRequest, 
        ILocalStorageService localStorage, 
        AppState appState,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _httpRequest = httpRequest;
        _localStorage = localStorage;
        _appState = appState;
        _authenticationStateProvider = authenticationStateProvider;
    }
    public async Task<bool> Login(Login login)
    {
        //TODO: handle potential errors and propmt them to the user
        var res = await _httpRequest.Post<AuthResponse, Login>(login, "/users/login")!; ;
        _appState.UserState.OnUserLog(res.Content);
        await _localStorage.SetItemAsync("userState", res.Content);
        await _authenticationStateProvider.GetAuthenticationStateAsync();
        return res.Success;
    }

    public async Task Logout()
    {
        _appState.UserState.OnUserLogOut();
        await _localStorage.RemoveItemAsync("userState");
        await _authenticationStateProvider.GetAuthenticationStateAsync();
    }

    public async Task<bool> IsLoggedIn()
    {
        var userState = await _localStorage.GetItemAsStringAsync("userState");
        return !string.IsNullOrEmpty(userState);
    }

    public async Task PersistLoggedInUser()
    {
        var userState = await _localStorage.GetItemAsStringAsync("userState");
        if (string.IsNullOrEmpty(userState))
            return;
        var user = JsonSerializer.Deserialize<AuthResponse>(userState);
        _appState.UserState.OnUserLog(user!);
    }

    public async Task<string> GetUserToken()
    {
        var userState = await _localStorage.GetItemAsStringAsync("userState");
        if (string.IsNullOrEmpty(userState))
            return string.Empty;
        var user = JsonSerializer.Deserialize<AuthResponse>(userState);
        return user?.Jwt.Replace("Bearer ", "") ?? string.Empty;
    }
}
