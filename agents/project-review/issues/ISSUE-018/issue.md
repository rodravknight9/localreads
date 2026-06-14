# ISSUE-018: Logout link navigation race

| Field | Value |
| --- | --- |
| ID | ISSUE-018 |
| Severity | MEDIUM |
| Status | Open |
| Affected files | `LocalReads/Layout/NavMenu.razor` |
| Summary | Anchor href=/ with @onclick LogOut may navigate before async logout completes. |
| Related issues | ISSUE-003, ISSUE-007 |
