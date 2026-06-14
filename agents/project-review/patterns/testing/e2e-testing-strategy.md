# E2E Testing Strategy

**Related issues:** [ISSUE-030](../../issues/ISSUE-030/issue.md)

## Recommended Tool

Playwright for .NET — tests real browser behavior including navigation, auth, and API integration.

## Critical User Flows to Cover

1. **Login flow:** `/login` → enter credentials → redirect to `/`
2. **Search flow:** enter search term → `/searchbooks` → results appear
3. **Add to list:** search → add book to "Want to Read" → snackbar confirmation
4. **Auth guard:** visit `/mybooks` unauthenticated → redirect or NotAuthorized UI

## Prerequisites Before E2E

- Fix ISSUE-003 (consistent auth) so tests have predictable behavior
- Fix ISSUE-005 (configurable URLs) so tests can target a test API
- Consider a `docker-compose` test environment

## Effort

E2E setup is Phase 4 work — after unit tests and core reliability fixes.
