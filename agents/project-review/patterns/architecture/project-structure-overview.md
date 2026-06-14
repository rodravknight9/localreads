# Project Structure Overview

**Related issues:** None (strength)

## Current Layout (Good)

```
LocalReads/
├── Pages/          → Routable views (@page)
├── Components/     → Reusable UI (no @page)
├── Layout/         → Shell layouts and nav
├── Services/       → HTTP, auth, notifications
├── State/          → Session state containers
├── DTO/            → Frontend-only mapping types
├── Models/         → HTTP response wrappers
└── wwwroot/        → Static assets and CSS
```

## Conventions

- **Pages** own routing and page-level orchestration
- **Components** are dialog/UI building blocks
- **Services** own API communication and side effects
- **State** holds session data shared across components
- **Shared project** (`LocalReads.Shared`) holds API contracts

This structure scales well for a small-to-medium Blazor app.
