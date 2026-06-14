# Security Checklist — LocalReads Blazor Frontend

Client-side security review items. Real enforcement must remain on the API.

---

## Authorization

- [ ] All authenticated pages use `@attribute [Authorize]` — [ISSUE-003](../issues/ISSUE-003/issue.md)
- [ ] `AuthorizeRouteView` NotAuthorized links point to valid routes (`/login`, `/register`) — [ISSUE-003](../issues/ISSUE-003/issue.md)
- [ ] No pages access `AppState.UserState.User.Id` without auth guard — [ISSUE-003](../issues/ISSUE-003/issue.md)
- [ ] Public pages (`ViewProfile`, `ViewBook`) intentionally allow anonymous access

## Token Handling

- [ ] JWT read from one consistent path (Blazored or `IAuthService`) — [ISSUE-007](../issues/ISSUE-007/issue.md)
- [ ] Token sent via `Authorization` header, not query strings
- [ ] 401 responses trigger logout and redirect — `HttpRequest.cs`
- [ ] `NotifyAuthenticationStateChanged` only on login/logout — [ISSUE-008](../issues/ISSUE-008/issue.md)

## XSS Prevention

- [ ] No `MarkupString` with user-provided content
- [ ] Razor `@` encoding used for all displayed user data — [patterns/security/xss-safe-rendering.md](../patterns/security/xss-safe-rendering.md)
- [ ] No `@((MarkupString)userHtml)` patterns

## Storage

- [ ] JWT in `localStorage` — acceptable WASM tradeoff, document risk — [patterns/security/jwt-localstorage-tradeoffs.md](../patterns/security/jwt-localstorage-tradeoffs.md)
- [ ] No secrets (API keys) in frontend code or `wwwroot/`

## Session & Logout

- [ ] Logout clears localStorage and auth state before navigation — [ISSUE-018](../issues/ISSUE-018/issue.md)
- [ ] Logout does not race with `<a href>` navigation — [ISSUE-018](../issues/ISSUE-018/issue.md)

## External Content

- [ ] Third-party image URLs (Google Books, Unsplash) are from expected domains
- [ ] Consider Content Security Policy for production deployment

## Error Handling

- [ ] API errors do not expose stack traces to users
- [ ] Failed auth does not leave stale user state in `AppState` — [ISSUE-004](../issues/ISSUE-004/issue.md)
