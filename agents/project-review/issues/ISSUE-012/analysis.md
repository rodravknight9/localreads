# Analysis - ISSUE-012

## Root cause
_isLoading never true; _error never set; wrong button copy.

## Code references
- RegisterPage.razor ~27-31 and ~59-67

## Impact
No feedback on slow or failed registration.

## How to verify
Invalid register and API down — expect spinner and errors.
