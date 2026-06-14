# Analysis - ISSUE-031

## Root cause
SearchResults and UserState properties may be null at runtime.

## Code references
- SearchResults.cs
- UserState.cs

## Impact
Nullable warnings suppressed; null surprises in UI.

## How to verify
Build with warnings as errors — check state types.
