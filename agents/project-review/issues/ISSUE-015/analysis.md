# Analysis - ISSUE-015

## Root cause
MudTable bound to _favorites but uses server reload API.

## Code references
- MyBooks.razor table vs ReloadServerData

## Impact
Reload no-op or inconsistent refresh.

## How to verify
Change category — favorites should reload via HTTP not ServerData API.
