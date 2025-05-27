using LocalReads.Shared.DataTransfer.User;

namespace LocalReads.Services;

public class ServerService : IServerService
{
    private readonly IHttpRequest _httpRequest;
    public ServerService(IHttpRequest httpRequest)
    {
        _httpRequest = httpRequest;
    }
    public async Task<AuthResponse> Login(Login login)
    {
        return await _httpRequest.Post<AuthResponse, Login>(login, "/users/login")!; ;
    }
}
