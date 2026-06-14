# Service Layer Design

**Related issues:** [ISSUE-014](../../issues/ISSUE-014/issue.md), [ISSUE-007](../../issues/ISSUE-007/issue.md)

## Current Services

| Service | Lifetime | Role |
|---------|----------|------|
| `HttpRequest` | Scoped | HTTP gateway, JWT, 401, snackbars |
| `AuthService` | Scoped | Login/logout, localStorage persistence |
| `CustomAuthStateProvider` | Scoped | JWT claims for `[Authorize]` |
| `NotificationService` | Singleton | SignalR hub, notification queue |

## Gaps

- No `IFavoritesService` — favorites CRUD lives in pages
- Token read duplicated in `HttpRequest` (should use `IAuthService`)

## Target Design

```
Pages → IFavoritesService → IHttpRequest → API
Pages → IAuthService → IHttpRequest + localStorage + AuthStateProvider
NavMenu → NotificationService (singleton, SignalR)
```

Pages should not open dialogs and call HTTP directly for business operations.
