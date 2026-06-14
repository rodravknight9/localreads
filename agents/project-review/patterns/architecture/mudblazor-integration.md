# MudBlazor Integration

**Related issues:** [ISSUE-006](../../issues/ISSUE-006/issue.md), [ISSUE-027](../../issues/ISSUE-027/issue.md)

## Current Usage (Good)

- `MudLayout`, `MudAppBar`, `MudContainer`, `MudPaper` for layout
- `MudTable` with `ServerData` for paginated search
- `IDialogService` for confirmation dialogs
- `ISnackbar` for user feedback
- `MudForm` + `MudTextField` on login/register

## Issues

1. `AddMudServices()` registered twice in `Program.cs` — keep only the configured call
2. `SearchBox.razor` uses raw HTML `<input>` instead of `MudTextField`
3. Per-page `<style>` blocks instead of `.razor.css` isolation

## Registration (correct)

```csharp
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    // ... other snackbar config
});
```

Register once. Do not call `AddMudServices()` before and after.
