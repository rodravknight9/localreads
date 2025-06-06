using Blazored.LocalStorage;
using LocalReads.Shared.DataTransfer.User;
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
}
