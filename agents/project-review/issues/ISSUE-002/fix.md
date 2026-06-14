# Proposed fix - ISSUE-002

Use `async Task` instead of `async void`:

```csharp
private async Task TriggerReload()
{
    await InvokeAsync(async () =>
    {
        if (_table != null)
        {
            await _table.ReloadServerData();
            StateHasChanged();
        }
    });
}
```
