# bUnit Recommendations

**Related issues:** [ISSUE-030](../../issues/ISSUE-030/issue.md)

## Setup

Create `LocalReads.Tests` project:

```xml
<PackageReference Include="bunit" Version="1.x" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.x" />
<PackageReference Include="xunit" Version="2.x" />
```

## Example Test — GenericDialog

```csharp
[Fact]
public void GenericDialog_Submit_InvokesCallback()
{
    using var ctx = new Bunit.TestContext();
    ctx.Services.AddMudServices();
    
    var cut = ctx.RenderComponent<GenericDialog>(parameters => parameters
        .Add(p => p.Title, "Confirm")
        .Add(p => p.Message, "Are you sure?"));
    
    cut.Find("button").Click();
    // assert dialog result
}
```

## What to Test First

| Component | Test focus |
|-----------|------------|
| `GenericDialog` | Submit/cancel events |
| `AddToListDialog` | Progress slider, date picker return values |
| `RateBookDialog` | Rating value passed back |
| `SearchBox` | Sets search term and navigates |
