# Authorization Strategy

**Related issues:** [ISSUE-003](../../issues/ISSUE-003/issue.md)

## Current State

LocalReads has three conflicting auth approaches:

1. `[Authorize]` on `EditProfile.razor` only
2. Manual `Navigation.NavigateTo("/login")` on `Home.razor`
3. No guard on most pages (`MyBooks`, `SearchBooks`, `MyServer`, `ViewBook`)

`App.razor` uses `AuthorizeRouteView` but NotAuthorized links point to wrong routes (`/users/login`).

## Recommended Strategy

Use **attribute-based authorization** consistently:

```razor
@using Microsoft.AspNetCore.Authorization
@attribute [Authorize]
```

For public pages (`LoginPage`, `RegisterPage`, `ViewProfile`, `ViewBook`), omit the attribute.

## Rules

- Protected pages: always `[Authorize]`
- Auth pages: redirect if already logged in (current pattern is fine)
- Let `AuthorizeRouteView` handle NotAuthorized UI — fix links to `/login` and `/register`
- Remove redundant manual redirects once attributes are in place
