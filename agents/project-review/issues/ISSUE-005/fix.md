# Proposed fix - ISSUE-005

Load API and SignalR URLs from configuration; use dynamic profile id in NavMenu:

```csharp
Navigation.NavigateTo($"/profile/{AppState.UserState.User.Id}");
```
