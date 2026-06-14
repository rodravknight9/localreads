# JWT in localStorage — Tradeoffs

**Related issues:** [ISSUE-007](../../issues/ISSUE-007/issue.md)

## Current Implementation

- `AuthService` stores `AuthResponse` via Blazored.LocalStorage (`userState` key)
- `HttpRequest` reads the same key via JS interop (`localStorage.getItem`)
- `CustomAuthStateProvider` reads via Blazored and parses JWT claims

## WASM Constraint

Blazor WebAssembly has no server-side session. `localStorage` is the standard choice for persisting JWT across page reloads.

## Risks

- If XSS is ever introduced, tokens in `localStorage` are accessible to malicious scripts
- No refresh token pattern — user must re-login when JWT expires

## Recommendations

1. Consolidate token access through `IAuthService` only (remove JS interop in `HttpRequest`)
2. Keep XSS prevention strict (no `MarkupString` with user content)
3. Consider refresh tokens in a future iteration
