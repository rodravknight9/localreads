# Analysis - ISSUE-017

## Root cause
Private GoToBook ~312-315 unused.

## Code references
- SearchBooks.razor GoToBook method

## Impact
Dead code noise in 317-line page.

## How to verify
grep GoToBook — only definition.
