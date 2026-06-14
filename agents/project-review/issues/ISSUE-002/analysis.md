# Analysis - ISSUE-002

## Root cause
Custom event handlers must return Task; async void swallows exceptions (~line 300).

## Code references
- SearchBooks.razor ~300 private async void TriggerReload()

## Impact
Unhandled exceptions can fault WASM without UI feedback.

## How to verify
Break reload in dev and compare async void vs async Task behavior.
