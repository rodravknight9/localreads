# Analysis - ISSUE-025

## Root cause
App.razor ~11 FocusOnNavigate h1 mismatch.

## Code references
- App.razor FocusOnNavigate

## Impact
Skip link / focus management ineffective.

## How to verify
Navigate route — focus may not move to visible heading.
