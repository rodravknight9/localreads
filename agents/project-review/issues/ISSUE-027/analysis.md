# Analysis - ISSUE-027

## Root cause
SearchBox uses plain input and emoji button without Enter or aria-label.

## Code references
- SearchBox.razor markup

## Impact
Inconsistent UX and accessibility vs MudTextField pages.

## How to verify
Tab to search — no Enter submit; compare to Mud forms.
