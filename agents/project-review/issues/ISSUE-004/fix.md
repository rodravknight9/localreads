# Proposed fix - ISSUE-004

Check `Success` before using `Content`:

```csharp
if (!response.Success)
{
    Snackbar.Add(response.ErrorMessage ?? "Request failed.", Severity.Error);
    return;
}
```
