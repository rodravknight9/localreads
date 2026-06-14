# Naming Conventions

**Related issues:** [ISSUE-026](../../issues/ISSUE-026/issue.md), [ISSUE-013](../../issues/ISSUE-013/issue.md)

## Inconsistencies Found

| Pattern | Examples | Preferred |
|---------|----------|-----------|
| Private fields | `isLoading` vs `_isLoading` | `_camelCase` for private fields |
| Method typos | `HandlUpdate` | `HandleUpdate` |
| Parameter typos | `AfirmativeText` | `AffirmativeText` |
| Inject names | `State` vs `AppState` | Match type name: `AppState` |

## Recommended Conventions for LocalReads

- **Pages:** PascalCase for component class (implicit from filename)
- **Private fields:** `_camelCase`
- **Parameters/properties:** PascalCase
- **Inject directives:** use type name (`@inject AppState AppState`)
- **Async methods:** suffix with `Async` when wrapping async work (`LoadFavoritesAsync`)
