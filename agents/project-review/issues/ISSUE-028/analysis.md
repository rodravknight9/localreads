# Analysis - ISSUE-028

## Root cause
Per-page style blocks in Home, SearchBooks, MyServer only.

## Code references
- Home/SearchBooks/MyServer style blocks

## Impact
Styles can leak globally.

## How to verify
Inspect scoped attributes — none on components.
