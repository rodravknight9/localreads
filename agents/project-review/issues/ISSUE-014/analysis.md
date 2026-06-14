# Analysis - ISSUE-014

## Root cause
~50+ lines duplicated per page instead of IFavoritesService.

## Code references
- SearchBooks, Home, MyBooks @code blocks

## Impact
Fixes need triple application; behavior drift.

## How to verify
Compare add-to-list on Home vs SearchBooks.
