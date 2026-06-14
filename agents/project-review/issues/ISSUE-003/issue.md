# ISSUE-003: Authorization is inconsistent

| Field | Value |
| --- | --- |
| ID | ISSUE-003 |
| Severity | HIGH |
| Status | Open |
| Affected files | `LocalReads/App.razor`, `LocalReads/Pages/Home.razor`, `LocalReads/Pages/EditProfile.razor` |
| Summary | Only EditProfile uses [Authorize]; other pages unguarded; NotAuthorized links use wrong routes. |
| Related issues | ISSUE-004, ISSUE-018, ISSUE-023 |
