using LocalReads.Shared.DataTransfer.User;

namespace LocalReads.State;

public class UserState 
{
    public AuthResponse User { get; set; }
    public event Action? OnChange;
    public void OnUserLog(AuthResponse authResponse)
    { 
        User = authResponse;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
