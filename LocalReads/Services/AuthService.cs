using Blazored.LocalStorage;
using LocalReads.Shared.DataTransfer.User;
using LocalReads.State;

namespace LocalReads.Services;

public class AuthService : IAuthService
{
    private readonly IHttpRequest _httpRequest;
    private readonly ILocalStorageService _localStorage;
    private readonly AppState _appState;
    public AuthService(
        IHttpRequest httpRequest, 
        ILocalStorageService localStorage, 
        AppState appState)
    {
        _httpRequest = httpRequest;
        _localStorage = localStorage;
        _appState = appState;
    }
    public async Task<bool> Login(Login login)
    {
        var res = await _httpRequest.Post<AuthResponse, Login>(login, "/users/login")!; ;
        _appState.UserState.OnUserLog(res.Content);
        await _localStorage.SetItemAsync("userState", res.Content);
        return res.Success;
    }

    public async Task Logout()
    {
        _appState.UserState.OnUserLogOut();
        await _localStorage.RemoveItemAsync("userState");
    }
}
