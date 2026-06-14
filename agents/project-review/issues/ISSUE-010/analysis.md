# Analysis - ISSUE-010

## Root cause
Guard ~276-281 likely needs OR; dead field ~179.

## Code references
- SearchBooks guard condition
- unused _notShowPromptTwiceOnOverride

## Impact
Pagination prompts behave incorrectly.

## How to verify
Change term and page rapidly — observe prompt logic.
