# Analysis - ISSUE-021

## Root cause
MudButton back control ~line 8 without handler.

## Code references
- MyServer.razor ~8

## Impact
Back affordance does nothing.

## How to verify
Click back on MyServer — no navigation.
