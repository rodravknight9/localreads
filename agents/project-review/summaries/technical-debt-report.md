# Technical Debt Report

**Project:** LocalReads Blazor WASM Frontend  
**Scope:** `LocalReads/` only  
**Status:** Analysis complete — no fixes applied

---

## Debt Score by Area

| Area | Score (1–5) | Notes | Key issues |
|------|-------------|-------|------------|
| **Reliability** | 4/5 (high debt) | Lifecycle leaks, unchecked HTTP, auth gaps | ISSUE-001, 002, 003, 004 |
| **Security (client)** | 3/5 | Auth guards incomplete; JWT in localStorage (acceptable for WASM) | ISSUE-003, 007, 032 |
| **Maintainability** | 3/5 | Duplicated favorites logic, dead code | ISSUE-014, 016, 017 |
| **Configuration** | 4/5 | All URLs hardcoded | ISSUE-005 |
| **UX completeness** | 2/5 | Stub buttons, incomplete register flow | ISSUE-012, 020, 022 |
| **Testing** | 5/5 (no coverage) | Zero frontend tests | ISSUE-030 |
| **Accessibility** | 3/5 | No PageTitle, minimal ARIA | ISSUE-024, 032 |
| **Architecture** | 2/5 (good structure) | Folder layout is sound; service extraction needed | ISSUE-014 |

*Score: 1 = healthy, 5 = significant debt*

---

## Severity Breakdown

### High (5 issues) — fix first

Bugs and reliability problems that can cause crashes or data leaks in normal use.

- ISSUE-001: Memory leaks (`IDisposable`)
- ISSUE-002: `async void` anti-pattern
- ISSUE-003: Authorization inconsistent
- ISSUE-004: Unchecked HTTP responses
- ISSUE-005: Hardcoded configuration

### Medium (18 issues) — maintainability and correctness

Won't always crash the app, but increase bug risk and slow future development.

- ISSUE-006 through ISSUE-023

### Low (9 issues) — polish and conventions

Quality-of-life improvements; address after high/medium items.

- ISSUE-024 through ISSUE-032

---

## 4-Phase Remediation Roadmap

### Phase 1 — Quick wins (1–2 hours)

| Task | Issues addressed |
|------|------------------|
| Add `@implements IDisposable` to `SearchBooks` and `NavMenu` | ISSUE-001 |
| Unsubscribe `NotificationService` event in NavMenu | ISSUE-001 |
| Change `TriggerReload` to `async Task` | ISSUE-002 |
| Fix `App.razor` login/register links | ISSUE-003 |
| Remove duplicate `AddMudServices()` | ISSUE-006 |
| Fix NavMenu profile link to current user ID | ISSUE-005 |

### Phase 2 — Reliability (half day)

| Task | Issues addressed |
|------|------------------|
| Add `[Authorize]` to protected pages | ISSUE-003 |
| Guard all HTTP calls; add loading/error UI | ISSUE-004 |
| Externalize API/SignalR URLs | ISSUE-005 |
| Initialize `ViewBook._listType` from server | ISSUE-011 |
| Fix Register page UX | ISSUE-012 |

### Phase 3 — Maintainability (1–2 days)

| Task | Issues addressed |
|------|------------------|
| Extract `IFavoritesService` | ISSUE-014 |
| Consolidate token access via `IAuthService` | ISSUE-007 |
| Move auth state notification to login/logout only | ISSUE-008 |
| Remove/fix dead code | ISSUE-016, 017, 022 |
| Fix Home book links, MyServer back button | ISSUE-020, 021 |
| Add missing wwwroot assets | ISSUE-019 |

### Phase 4 — Quality (ongoing)

| Task | Issues addressed |
|------|------------------|
| Add bUnit tests | ISSUE-030 |
| Accessibility pass | ISSUE-024, 025, 032 |
| CSS isolation | ISSUE-028 |
| Align package versions | ISSUE-029 |
| Naming cleanup | ISSUE-026 |

---

## Estimated Total Effort

| Phase | Effort | Cumulative |
|-------|--------|------------|
| Phase 1 | 1–2 hours | 1–2 hours |
| Phase 2 | 4 hours | ~6 hours |
| Phase 3 | 1–2 days | ~2–3 days |
| Phase 4 | Ongoing | — |

---

## Debt Trends

If left unaddressed:

1. **ISSUE-001 + ISSUE-014** will compound — more pages subscribing to events and duplicating logic makes leaks and bugs harder to trace.
2. **ISSUE-003 + ISSUE-004** create a poor unauthenticated user experience (crashes instead of login prompts).
3. **ISSUE-030** means every future change ships without regression safety net.

---

## Source

[`.cursor/knowledge/blazor-frontend-analysis.md`](../../.cursor/knowledge/blazor-frontend-analysis.md)
