# Analysis - ISSUE-001

## Root cause
SearchBooks and NavMenu wire handlers in OnInitialized(Async) and implement Dispose() without @implements IDisposable. NavMenu never unsubscribes NotificationService.OnNotificationReceived.

## Code references
- SearchBooks.razor subscriptions ~181-196
- NavMenu.razor UserState and NotificationService ~56-70

## Impact
Handlers keep firing after navigation, causing extra renders and possible NullReferenceException on disposed instances.

## How to verify
Navigate Home and Search Books repeatedly; log in Dispose() — without @implements IDisposable it never runs.
