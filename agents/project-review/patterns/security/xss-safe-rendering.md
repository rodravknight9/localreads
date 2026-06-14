# XSS-Safe Rendering

**Related issues:** None (strength)

## Current State — Good

LocalReads renders user content (book titles, descriptions, search terms) using Razor `@` syntax, which HTML-encodes output automatically. No `MarkupString` usage was found.

## Keep Doing

```razor
<!-- Safe -->
<MudText>@book.Title</MudText>

<!-- Dangerous — avoid -->
<MudText>@((MarkupString)book.Description)</MudText>
```

## Watch For

- Future "comments" feature — never render raw HTML from users
- Book descriptions from Google Books API — treat as untrusted text even from API
