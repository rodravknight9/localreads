# Duplication and Service Extraction

**Related issues:** [ISSUE-014](../../issues/ISSUE-014/issue.md)

## Duplicated Flows

The "add book to favorites" flow is copy-pasted across:

- `SearchBooks.razor` (~60 lines)
- `Home.razor` (~30 lines for progress update)
- `MyBooks.razor` (~100 lines for rate/delete/progress)

Each copy includes: dialog open → user input → API POST → override dialog on conflict → snackbar.

## Extraction Threshold

Extract a service when the same **HTTP + dialog + feedback** pattern appears in **3+ places**.

## Proposed Abstraction

```csharp
public interface IFavoritesService
{
    Task<bool> AddToListAsync(BookDto book, BookState listType);
    Task<bool> UpdateProgressAsync(Favorite favorite);
    Task<bool> RateBookAsync(Favorite favorite);
    Task<bool> DeleteAsync(Favorite favorite);
}
```

Pages become thin: call service, handle bool result.
