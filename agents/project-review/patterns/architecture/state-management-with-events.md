# State Management with Events

**Related issues:** [ISSUE-001](../../issues/ISSUE-001/issue.md), [ISSUE-031](../../issues/ISSUE-031/issue.md)

## Pattern

```csharp
// AppState.cs — scoped container
public class AppState
{
    public SearchResults SearchResults { get; set; } = new();
    public UserState UserState { get; set; } = new();
}

// UserState.cs — event for UI refresh
public class UserState
{
    public AuthResponse? User { get; set; }
    public event Action? OnChange;
    public void SetUser(AuthResponse user) { User = user; OnChange?.Invoke(); }
}
```

## Why It Works

- Scoped registration = per browser session
- Events notify components without prop drilling
- Lightweight — no Redux/Fluxor needed at this scale

## Requirements

1. Components subscribing to events must implement `IDisposable`
2. Initialize nullable properties when nullable is enabled (ISSUE-031)

## Data Flow

```
SearchBox → AppState.SearchResults.SetTerm() → event → SearchBooks reloads table
AuthService → AppState.UserState.SetUser() → event → NavMenu refreshes avatar
```
