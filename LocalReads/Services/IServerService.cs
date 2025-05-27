using LocalReads.Shared.DataTransfer.User;

namespace LocalReads.Services
{
    public interface IServerService
    {
        public Task<AuthResponse> Login(Login login); 
    }
}
