# Proposed fix - ISSUE-001

Add `@implements IDisposable` at the top of both components.

```razor
@implements IDisposable
```

In NavMenu `Dispose()`, also unsubscribe notifications:

```csharp
NotificationService.OnNotificationReceived -= StateHasChanged;
```
