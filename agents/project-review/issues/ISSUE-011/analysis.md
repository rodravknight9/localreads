# Analysis - ISSUE-011

## Root cause
Defaults to WantToRead; not mapped after GET (~98, 105-110).

## Code references
- ViewBook.razor _listType and load path

## Impact
Dropdown shows wrong shelf for in-progress books.

## How to verify
Open in-progress favorite in ViewBook — select should match API.
