# Learning Journal — First Blazor Project

A reflective guide for continuing to learn Blazor, based on the LocalReads frontend review.

---

## What I Got Right

These patterns show good instincts and should be kept as the project grows.

### Project organization

Splitting code into `Pages/`, `Components/`, `Services/`, `State/`, and `Layout/` made navigation easy from day one. This matches how most Blazor teams structure apps.

**Pattern guide:** [architecture/project-structure-overview.md](../patterns/architecture/project-structure-overview.md)

### Centralized HTTP

`HttpRequest` as a single gateway for API calls — with JWT injection, 401 logout, and snackbar errors — is the right abstraction. Pages don't need to know how tokens work.

### Reusable dialogs

`GenericDialog`, `AddToListDialog`, and `RateBookDialog` demonstrate component composition. MudBlazor's `IDialogService` is used correctly.

### Shared DTOs

Referencing `LocalReads.Shared` avoids duplicating API contracts between frontend and backend.

### Server-side pagination

`SearchBooks.razor` using `MudTable` with `ServerData` is the correct approach for paged API data — not loading all books at once.

---

## What I Learned (and Why It Matters)

### 1. `@implements IDisposable` is not optional

**Lesson:** Writing `Dispose()` is not enough. Blazor only calls it when you declare `@implements IDisposable`.

**Related:** [ISSUE-001](../issues/ISSUE-001/issue.md), [performance/component-lifecycle-and-disposal.md](../patterns/performance/component-lifecycle-and-disposal.md)

### 2. `async Task` vs `async void`

**Lesson:** Custom methods should return `Task`. Only UI framework bindings (like `@onclick`) can be `async void`. Exceptions in `async void` disappear.

**Related:** [ISSUE-002](../issues/ISSUE-002/issue.md), [clean-code/async-patterns.md](../patterns/clean-code/async-patterns.md)

### 3. Pick one auth strategy

**Lesson:** Mixing `[Authorize]`, manual redirects, and no guards creates gaps. `AuthorizeRouteView` already handles the "not logged in" UI — use it consistently.

**Related:** [ISSUE-003](../issues/ISSUE-003/issue.md), [security/authorization-strategy.md](../patterns/security/authorization-strategy.md)

### 4. Always check HTTP success before using content

**Lesson:** `HttpResponse<T>.Success` exists for a reason. Using `.Content` blindly causes null-reference crashes.

**Related:** [ISSUE-004](../issues/ISSUE-004/issue.md)

### 5. Extract services when logic repeats 3+ times

**Lesson:** Favorites CRUD is copy-pasted across three pages. An `IFavoritesService` would cut ~150 lines and centralize behavior.

**Related:** [ISSUE-014](../issues/ISSUE-014/issue.md), [architecture/service-layer-design.md](../patterns/architecture/service-layer-design.md)

### 6. `StateHasChanged` is not magic glue

**Lesson:** Blazor re-renders after event handlers automatically. Only call `StateHasChanged` when updating state outside the normal cycle (C# events, timers, SignalR).

**Related:** [performance/unnecessary-rerenders.md](../patterns/performance/unnecessary-rerenders.md)

### 7. Scoped `AppState` is correct for WASM

**Lesson:** Registering state as scoped gives each browser session its own instance. Using C# events on it is fine — just unsubscribe in `Dispose()`.

**Related:** [architecture/state-management-with-events.md](../patterns/architecture/state-management-with-events.md)

---

## What to Study Next

| Topic | Resource direction | Ties to |
|-------|-------------------|---------|
| Component lifecycle | Microsoft docs: Blazor component lifecycle | ISSUE-001 |
| Authorization in WASM | `AuthorizeRouteView`, `[Authorize]`, policies | ISSUE-003 |
| bUnit testing | bUnit docs + sample tests for dialogs | ISSUE-030 |
| CSS isolation | `.razor.css` scoped styles | ISSUE-028 |
| Accessibility in Blazor | `PageTitle`, `AriaLabel`, semantic headings | ISSUE-024, ISSUE-032 |
| Configuration in WASM | `wwwroot/appsettings.json` pattern | ISSUE-005 |

---

## Mistakes That Are Normal for Beginners

Don't feel bad about these — they appear in many first Blazor projects:

- Forgetting `@implements IDisposable` despite writing `Dispose()`
- Using `async void` for custom event handlers
- Applying `[Authorize]` to only one page
- Not checking HTTP response success flags
- Hardcoding `localhost` URLs during development
- Copy-pasting dialog + API logic instead of extracting a service
- Leaving stub UI buttons without handlers

---

## Personal Action Items

1. Read [checklists/code-review-checklist.md](../checklists/code-review-checklist.md) before every PR
2. Start Phase 1 fixes from [technical-debt-report.md](technical-debt-report.md)
3. Add one bUnit test as a learning exercise (e.g. `GenericDialog`)
4. Re-read this journal after completing each remediation phase

---

## Source

[`.cursor/knowledge/blazor-frontend-analysis.md`](../../.cursor/knowledge/blazor-frontend-analysis.md)
