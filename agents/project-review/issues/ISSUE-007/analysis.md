# Analysis - ISSUE-007

## Root cause
Same userState key accessed two different ways.

## Code references
- AuthService SetItemAsync
- HttpRequest.js ~213-221

## Impact
Auth header may desync after storage changes.

## How to verify
Login and inspect Authorization header on API calls.
