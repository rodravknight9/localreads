# Analysis - ISSUE-009

## Root cause
catch (Exception) at ~289-296 returns previous AppState books.

## Code references
- SearchBooks.razor ~289-296

## Impact
Users see stale search results on API errors.

## How to verify
Fail API during pagination — expect error not stale data.
