# Proposed fix - ISSUE-003

Fix NotAuthorized URLs and adopt `[Authorize]` on protected pages:

```razor
@attribute [Authorize]
@page "/mybooks"
```
