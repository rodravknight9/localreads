# Analysis - ISSUE-008

## Root cause
NotifyAuthenticationStateChanged inside GetAuthenticationStateAsync (~46).

## Code references
- CustomAuthStateProvider.cs ~46

## Impact
Extra re-renders and possible UI flicker.

## How to verify
Log notifications — should spike only on login/logout.
