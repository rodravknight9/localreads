# ISSUE-002: async void anti-pattern in TriggerReload

| Field | Value |
| --- | --- |
| ID | ISSUE-002 |
| Severity | HIGH |
| Status | Open |
| Affected files | `LocalReads/Pages/SearchBooks.razor` |
| Summary | TriggerReload is async void but subscribed to SearchResults.OnBooksSearchChanged. |
| Related issues | ISSUE-001, ISSUE-009 |
