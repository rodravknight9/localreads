# Performance Checklist — LocalReads Blazor Frontend

---

## Component Lifecycle & Memory

- [ ] Event subscriptions cleaned up via `@implements IDisposable` — [ISSUE-001](../issues/ISSUE-001/issue.md)
- [ ] `NotificationService.OnNotificationReceived` unsubscribed on dispose — [ISSUE-001](../issues/ISSUE-001/issue.md)
- [ ] No `async void` custom handlers — [ISSUE-002](../issues/ISSUE-002/issue.md)

## Re-renders

- [ ] `NotifyAuthenticationStateChanged` not called on every `GetAuthenticationStateAsync` — [ISSUE-008](../issues/ISSUE-008/issue.md)
- [ ] `StateHasChanged` not over-called in lifecycle methods — [patterns/performance/unnecessary-rerenders.md](../patterns/performance/unnecessary-rerenders.md)
- [ ] Event handlers use named methods instead of passing `StateHasChanged` directly when logic is non-trivial

## Data Loading

- [ ] Paged tables use `ServerData` delegate, not client-side full load — `SearchBooks.razor`
- [ ] API failures don't trigger unnecessary re-fetch loops — [ISSUE-009](../issues/ISSUE-009/issue.md)
- [ ] Pagination state guards use correct boolean logic — [ISSUE-010](../issues/ISSUE-010/issue.md)

## Table Binding

- [ ] Client-bound tables (`Items=`) refresh via data reload, not `ReloadServerData()` — [ISSUE-015](../issues/ISSUE-015/issue.md)
- [ ] Server-bound tables don't also bind redundant `Items`

## SignalR

- [ ] `NotificationService` singleton — acceptable for app lifetime
- [ ] Hub connection uses automatic reconnect — `NotificationService.cs`
- [ ] Hub URL from configuration, not hardcoded — [ISSUE-005](../issues/ISSUE-005/issue.md)

## Assets

- [ ] Static assets exist and are cached appropriately — [ISSUE-019](../issues/ISSUE-019/issue.md)
- [ ] Book cover images have fallback placeholder — [ISSUE-019](../issues/ISSUE-019/issue.md)

## Packages

- [ ] SignalR client version aligned with .NET 8 stack — [ISSUE-029](../issues/ISSUE-029/issue.md)
