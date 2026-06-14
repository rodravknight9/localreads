# Dead Code Management

**Related issues:** [ISSUE-016](../../issues/ISSUE-016/issue.md), [ISSUE-017](../../issues/ISSUE-017/issue.md), [ISSUE-022](../../issues/ISSUE-022/issue.md)

## Dead Code Found

| Item | Location | Action |
|------|----------|--------|
| `BookCard.razor` | `Components/` | Remove or integrate — not referenced anywhere |
| `GoToBook()` | `SearchBooks.razor` | Remove — links use `MudLink Href` directly |
| `_notShowPromptTwiceOnOverride` | `SearchBooks.razor` | Remove unused field |
| Stub buttons | `MyBooks.razor`, `ViewBook.razor` | Implement or remove UI |

## Rule

If a component or method isn't referenced and isn't planned for imminent use, delete it. Dead code confuses future readers (especially when learning a new framework).

## Stub UI

Placeholder buttons ("Add a comment", "Edit", "View") without handlers create false affordances. Either wire them up or hide them until the feature exists.
