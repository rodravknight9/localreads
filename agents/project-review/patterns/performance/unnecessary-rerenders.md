# Unnecessary Re-renders

**Related issues:** [ISSUE-008](../../issues/ISSUE-008/issue.md), [ISSUE-001](../../issues/ISSUE-001/issue.md)

## When Blazor Re-renders Automatically

- After event handlers (`@onclick`, form submit)
- After `OnInitialized` / `OnParametersSet` complete
- After `await` in lifecycle methods completes

## When You Need `StateHasChanged`

- C# event callbacks from `AppState`, `NotificationService`, timers
- SignalR message handlers
- Background `Task` completions outside Blazor context

## Anti-patterns in LocalReads

1. `NotifyAuthenticationStateChanged` inside `GetAuthenticationStateAsync` — triggers full auth cascade on every read
2. Leaked event handlers keep firing `StateHasChanged` on disposed components
3. Passing `StateHasChanged` directly as event handler — works but obscures intent

## Fix Priority

1. Fix IDisposable leaks (ISSUE-001)
2. Move auth notification to login/logout only (ISSUE-008)
