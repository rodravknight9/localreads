# Current Test Gap

**Related issues:** [ISSUE-030](../../issues/ISSUE-030/issue.md)

## State

- No test project in `LocalReads.sln`
- No bUnit, xUnit, NUnit, or Playwright references
- No `*.Tests` project anywhere in the repo for the Blazor app

## Risk

Every change ships without regression detection. For a learning project this is acceptable initially, but becomes costly as features grow.

## Priority Test Targets

1. `GenericDialog` — confirm/cancel behavior
2. `AddToListDialog` — form validation and return data
3. `AuthService.Login` — success/failure paths
4. `BookDto.FromBook` / `ToBookDomain` — mapping correctness
