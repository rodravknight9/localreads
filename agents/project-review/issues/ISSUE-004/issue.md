# ISSUE-004: Unchecked HTTP responses

| Field | Value |
| --- | --- |
| ID | ISSUE-004 |
| Severity | HIGH |
| Status | Open |
| Affected files | `LocalReads/Pages/Home.razor`, `LocalReads/Pages/ViewProfile.razor`, `LocalReads/Pages/MyBooks.razor`, `LocalReads/Pages/ViewBook.razor` |
| Summary | Pages use HttpRequest.Get(...).Content without checking Success. |
| Related issues | ISSUE-003, ISSUE-012, ISSUE-013 |
