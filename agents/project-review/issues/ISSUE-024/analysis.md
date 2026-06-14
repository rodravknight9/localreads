# Analysis - ISSUE-024

## Root cause
Only NotFound sets title; pages omit <PageTitle>.

## Code references
- All Pages/*.razor except NotFound

## Impact
Poor tab identification and screen reader context.

## How to verify
Open tabs for Home vs Search — titles identical.
