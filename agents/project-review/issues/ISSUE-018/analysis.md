# Analysis - ISSUE-018

## Root cause
`<a href="/" @onclick="LogOut">` races default navigation vs await LogOut.

## Code references
- NavMenu.razor ~46-47

## Impact
Session may clear after navigation; stale auth UI.

## How to verify
Click logout rapidly — token may remain briefly.
