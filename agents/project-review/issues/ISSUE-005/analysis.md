# Analysis - ISSUE-005

## Root cause
Program.cs BaseAddress localhost:5033; NotificationService hub localhost:7223; NavMenu NavigateTo /profile/1.

## Code references
- Program.cs ~15
- NotificationService.cs ~17-18
- NavMenu.razor ~81

## Impact
Breaks non-local deployments; wrong profile opened from avatar.

## How to verify
Change API port or click avatar — URL should use logged-in user id.
