# Analysis - ISSUE-006

## Root cause
Two AddMudServices calls (~20 and ~26-37) may override MudBlazor options.

## Code references
- Program.cs duplicate AddMudServices

## Impact
Unpredictable snackbar/dialog configuration.

## How to verify
grep AddMudServices — should appear once with config.
