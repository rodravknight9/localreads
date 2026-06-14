# Analysis - ISSUE-003

## Root cause
AuthorizeRouteView exists but most pages omit @attribute [Authorize]. NotAuthorized links /users/login and /users/register instead of /login and /register.

## Code references
- App.razor NotAuthorized ~6-9
- EditProfile sole [Authorize]
- Home manual redirect

## Impact
Logged-out users can open /mybooks and hit null UserState; auth links 404.

## How to verify
Visit /mybooks logged out; click NotAuthorized Login link.
