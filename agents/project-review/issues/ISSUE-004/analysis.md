# Analysis - ISSUE-004

## Root cause
HttpResponse<T>.Success is ignored at Home, ViewProfile, MyBooks, ViewBook call sites.

## Code references
- Home.razor ~89-90
- ViewProfile ~61
- MyBooks ~121
- ViewBook ~105

## Impact
API failures yield null Content and NullReferenceException in UI.

## How to verify
Stop API and load Home — expect error message not crash.
