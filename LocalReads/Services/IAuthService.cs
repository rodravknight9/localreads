using LocalReads.Shared.DataTransfer.User;

namespace LocalReads.Services
{
    public interface IAuthService
    {
        public Task<bool> Login(Login login);
        public Task Logout();
    }
}
