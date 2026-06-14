# ISSUE-007: Dual token storage access

| Field | Value |
| --- | --- |
| ID | ISSUE-007 |
| Severity | MEDIUM |
| Status | Open |
| Affected files | `LocalReads/Services/AuthService.cs`, `LocalReads/Services/HttpRequest.cs` |
| Summary | Token written via Blazored LocalStorage, read via JS interop in HttpRequest. |
| Related issues | ISSUE-023, ISSUE-008 |
