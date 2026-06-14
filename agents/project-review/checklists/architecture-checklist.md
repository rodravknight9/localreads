# Architecture Checklist — LocalReads Blazor Frontend

---

## Project Structure

- [ ] New pages go in `Pages/` with `@page` directive
- [ ] Reusable UI goes in `Components/` (no `@page`)
- [ ] API/HTTP logic goes in `Services/`, not pages
- [ ] Session state goes in `State/`, not static classes — [patterns/architecture/state-management-with-events.md](../patterns/architecture/state-management-with-events.md)
- [ ] Shared API contracts stay in `LocalReads.Shared`

## Service Layer

- [ ] HTTP calls go through `IHttpRequest`, not raw `HttpClient` in pages
- [ ] Auth operations go through `IAuthService`
- [ ] Duplicated business logic (3+ pages) extracted to a service — [ISSUE-014](../issues/ISSUE-014/issue.md)
- [ ] Services registered with correct lifetime (scoped vs singleton) in `Program.cs`

## Dependency Injection

- [ ] `AddMudServices()` registered once with config — [ISSUE-006](../issues/ISSUE-006/issue.md)
- [ ] `HttpClient` base address from configuration — [ISSUE-005](../issues/ISSUE-005/issue.md)
- [ ] `AppState` registered as scoped
- [ ] `NotificationService` registered as singleton

## Configuration

- [ ] API URL in `wwwroot/appsettings.json` — [ISSUE-005](../issues/ISSUE-005/issue.md)
- [ ] SignalR hub URL in configuration — [ISSUE-005](../issues/ISSUE-005/issue.md)
- [ ] No magic numbers or hardcoded user IDs — [ISSUE-005](../issues/ISSUE-005/issue.md)

## State Management

- [ ] `AppState` used as container, not god object
- [ ] State change events unsubscribed in `Dispose()` — [ISSUE-001](../issues/ISSUE-001/issue.md)
- [ ] Nullable properties initialized where nullable is enabled — [ISSUE-031](../issues/ISSUE-031/issue.md)

## Component Design

- [ ] Pages over ~200 lines of logic consider code-behind (`.razor.cs`)
- [ ] Dialogs are reusable components, not inline markup
- [ ] Dead components removed or integrated — [ISSUE-016](../issues/ISSUE-016/issue.md)

## Styling

- [ ] Page-specific styles use `.razor.css` isolation — [ISSUE-028](../issues/ISSUE-028/issue.md)
- [ ] Global styles limited to `wwwroot/css/app.css`
- [ ] MudBlazor utilities preferred over custom CSS where possible

## Testing

- [ ] New services have unit tests — [ISSUE-030](../issues/ISSUE-030/issue.md)
- [ ] Critical dialogs have bUnit tests — [patterns/testing/bunit-recommendations.md](../patterns/testing/bunit-recommendations.md)
