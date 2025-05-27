using LocalReads.Shared.DataTransfer.User;

namespace LocalReads.State;

public class AppState
{
    public SearchResults SearchResults { get; set; } = new SearchResults();
    public UserState UserState { get; set; } = new UserState();
}