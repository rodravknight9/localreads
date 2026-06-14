# Component Lifecycle and Disposal

**Related issues:** [ISSUE-001](../../issues/ISSUE-001/issue.md)

## Blazor Lifecycle (simplified)

```
OnInitialized / OnInitializedAsync
    → component renders
    → user navigates away
    → Dispose() called (only if @implements IDisposable)
```

## Rule

If you subscribe to an event in `OnInitialized`, you **must**:

1. Declare `@implements IDisposable`
2. Unsubscribe in `Dispose()`

## Affected Components

- `SearchBooks.razor` — subscribes to `AppState.SearchResults` events
- `NavMenu.razor` — subscribes to `UserState.OnChange` and `NotificationService.OnNotificationReceived`

## Example

```razor
@implements IDisposable

@code {
    protected override void OnInitialized()
    {
        AppState.UserState.OnChange += OnUserStateChanged;
    }

    private void OnUserStateChanged() => InvokeAsync(StateHasChanged);

    public void Dispose()
    {
        AppState.UserState.OnChange -= OnUserStateChanged;
    }
}
```
