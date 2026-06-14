# Async Patterns

**Related issues:** [ISSUE-002](../../issues/ISSUE-002/issue.md)

## Rules

| Context | Return type |
|---------|-------------|
| Blazor event binding (`@onclick`) | `async Task` (preferred) or `async void` (framework allows) |
| Custom event handlers (`OnBooksSearchChanged`) | `async Task` |
| Lifecycle methods | `async Task OnInitializedAsync()` |
| `IDisposable.Dispose()` | `void` (synchronous only) |

## Anti-pattern in LocalReads

```csharp
// BAD — SearchBooks.razor line 300
private async void TriggerReload() { ... }

// GOOD
private async Task TriggerReload()
{
    if (_table != null)
        await _table.ReloadServerData();
}
```

## Event Subscription with async handlers

If subscribing to `EventHandler` that expects `void`, wrap:

```csharp
AppState.SearchResults.OnBooksSearchChanged += async () => await TriggerReload();
```

Or use a synchronous wrapper that calls `InvokeAsync`.
