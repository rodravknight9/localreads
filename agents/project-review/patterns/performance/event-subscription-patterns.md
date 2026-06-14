# Event Subscription Patterns

**Related issues:** [ISSUE-001](../../issues/ISSUE-001/issue.md)

## AppState Event Pattern

`UserState` and `SearchResults` expose C# events (`OnChange`, `OnTermSearchChange`). Components subscribe to react to cross-component state changes.

This is valid for a small app. Rules:

| Do | Don't |
|----|-------|
| Subscribe in `OnInitialized` | Subscribe in `OnAfterRender` without guard |
| Unsubscribe in `Dispose()` | Forget `@implements IDisposable` |
| Use named handler methods | Pass `StateHasChanged` when logic may grow |

## NotificationService Pattern

Singleton service with `OnNotificationReceived` event. `NavMenu` subscribes but never unsubscribes — fix in ISSUE-001 resolution.

## Alternative (future)

For larger apps, consider `IObservable`, MediatR, or Fluxor instead of raw C# events.
