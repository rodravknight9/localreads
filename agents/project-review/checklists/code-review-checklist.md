# Code Review Checklist — LocalReads Blazor Frontend

Use this checklist when reviewing changes to `LocalReads/`.

---

## Component Lifecycle

- [ ] Components subscribing to events in `OnInitialized` declare `@implements IDisposable` — [ISSUE-001](../issues/ISSUE-001/issue.md)
- [ ] All event subscriptions are unsubscribed in `Dispose()` — [ISSUE-001](../issues/ISSUE-001/issue.md)
- [ ] Custom async methods return `Task`, not `void` — [ISSUE-002](../issues/ISSUE-002/issue.md)
- [ ] `StateHasChanged()` is only called when updating state outside normal render cycle — [patterns/performance/unnecessary-rerenders.md](../patterns/performance/unnecessary-rerenders.md)

## HTTP & Data

- [ ] `response.Success` is checked before accessing `response.Content` — [ISSUE-004](../issues/ISSUE-004/issue.md)
- [ ] Loading states shown during `OnInitializedAsync` data fetches
- [ ] Error messages shown to user on API failure (snackbar or inline)
- [ ] No silent `catch (Exception)` without user feedback — [ISSUE-009](../issues/ISSUE-009/issue.md)

## Authorization

- [ ] Protected pages have `@attribute [Authorize]` — [ISSUE-003](../issues/ISSUE-003/issue.md)
- [ ] No manual `NavigateTo("/login")` when `[Authorize]` suffices
- [ ] Auth pages redirect logged-in users away (login/register)

## Naming & Clean Code

- [ ] Private fields use `_camelCase` prefix consistently — [ISSUE-026](../issues/ISSUE-026/issue.md)
- [ ] Method names are spelled correctly (no `HandlUpdate`) — [ISSUE-013](../issues/ISSUE-013/issue.md)
- [ ] No dead code (unused methods, unreferenced components) — [ISSUE-016](../issues/ISSUE-016/issue.md), [ISSUE-017](../issues/ISSUE-017/issue.md)
- [ ] No duplicated dialog + API logic — extract to service if 3+ pages — [ISSUE-014](../issues/ISSUE-014/issue.md)

## UI & MudBlazor

- [ ] Prefer MudBlazor components over raw HTML — [ISSUE-027](../issues/ISSUE-027/issue.md)
- [ ] `MudTable` with `Items` does not call `ReloadServerData()` — [ISSUE-015](../issues/ISSUE-015/issue.md)
- [ ] `MudTable` with `ServerData` does not also bind redundant `Items`
- [ ] Icon buttons have `AriaLabel` — [ISSUE-032](../issues/ISSUE-032/issue.md)

## Pages

- [ ] Each page has `<PageTitle>` — [ISSUE-024](../issues/ISSUE-024/issue.md)
- [ ] Navigation links point to correct routes (not stubs or `/`)
- [ ] Buttons and links have working `OnClick` handlers — [ISSUE-022](../issues/ISSUE-022/issue.md)

## Configuration

- [ ] No hardcoded `localhost` URLs — use `appsettings.json` — [ISSUE-005](../issues/ISSUE-005/issue.md)
- [ ] `AddMudServices()` called only once — [ISSUE-006](../issues/ISSUE-006/issue.md)
