# ISSUE-001: Memory leaks from missing @implements IDisposable

| Field | Value |
| --- | --- |
| ID | ISSUE-001 |
| Severity | HIGH |
| Status | Open |
| Affected files | `LocalReads/Pages/SearchBooks.razor`, `LocalReads/Layout/NavMenu.razor` |
| Summary | Components subscribe to AppState/NotificationService events and define Dispose(), but Blazor never calls Dispose() without @implements IDisposable. |
| Related issues | ISSUE-002, ISSUE-008 |
