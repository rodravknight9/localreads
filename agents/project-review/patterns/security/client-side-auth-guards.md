# Client-Side Auth Guards

**Related issues:** [ISSUE-003](../../issues/ISSUE-003/issue.md), [ISSUE-004](../../issues/ISSUE-004/issue.md)

## Defense in Depth

Client-side auth is **UX only**. The API must enforce authorization on every endpoint. The frontend guards prevent:

- Null-reference crashes on `User.Id`
- Confusing error states for unauthenticated users
- Exposure of UI meant for logged-in users

## Current Gaps

Pages like `SearchBooks.razor` line 220 use `AppState.UserState.User.Id` without verifying the user is logged in. If auth state is null, the app crashes instead of redirecting.

## Pattern

```razor
@attribute [Authorize]

@code {
    protected override async Task OnInitializedAsync()
    {
        var response = await HttpRequest.Get<List<Favorite>>("/favorite");
        if (!response.Success) { /* show error */ return; }
        _favorites = response.Content ?? [];
    }
}
```
