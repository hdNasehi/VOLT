# VOLT Build Prompt — Compliance Audit

Last reviewed against the full-stack build prompt.

| Requirement | Status | Notes |
|-------------|--------|-------|
| **Solution:** Api, Client, Shared, Database | ✅ Partial | `VOLT.sln` + projects exist; extra `GymCoach.Client.Tests` + host `GymCoach.Client` (Blazor Web App IWA per earlier spec) |
| **.NET 9** | ⚠️ | SDK on machine is **.NET 10** → projects target `net10.0` |
| **Blazor WASM PWA** | ✅ Partial | Interactive WASM in `GymCoach.Client.Client`; PWA manifest + service worker present |
| **MudBlazor** | ✅ | Installed; theme being aligned to VOLT tokens |
| **Blazor-ApexCharts** | ✅ | Package + `AddApexCharts()` registered |
| **EF Core + SQL Server** | ✅ | Entities, Fluent config, `InitialCreate` migration, bilingual seed |
| **Identity + JWT** | ❌ | Next: Authentication prompt |
| **OpenAI / IAiCoachService** | ❌ | Next: AI Coach prompt |
| **No DDD/CQRS/MediatR/etc.** | ✅ | Plain layered structure |
| **Generic repository + Result + paging** | ✅ | `Result`/`PagedResult` + `IGenericRepository<T>` |
| **Design tokens (lime/violet)** | ✅ | `variables.css` + `VoltMudTheme` exact HEX |
| **Typography (Space Grotesk, Hanken, Vazirmatn)** | ✅ | Google fonts + CSS |
| **Bottom nav 5 tabs, elevated AI** | ✅ | `VoltBottomNav` — Home / Library / AI / Progress / Profile |
| **Top bar + EN/فا toggle** | ✅ | `VoltTopBar` |
| **Bilingual RTL + Persian numerals** | ✅ Partial | `ILocalizationService`; expand via localization prompt |
| **All 11 screen groups** | ❌ | Skeleton pages only; per-feature prompts pending |
| **Data model entities** | ❌→✅ | Full entity set + seed in this pass |
| **Controllers per feature** | ❌ | Stub minimal API only; feature prompts pending |
| **Component library (Volt*)** | ✅ Partial | 11 VOLT wrappers exist; charts/heatmap = feature work |
| **Coach roster dashboard** | ❌ | Placeholder athletes page |
| **Auth screens** | ❌ | Pending Authentication prompt |

## Intentional deviations (earlier architecture)

- **Blazor Web App (host + WASM)** instead of standalone WASM-only template — still delivers Interactive WASM + PWA.
- **GymCoach.Client.Tests** — retained from component-architecture rules.

## Next prompts (as specified)

Use these in order after skeleton is green:

1. Athlete Dashboard  
2. Exercise Library + Detail  
3. AI Coach  
4. Workout Plans  
5. Workout Tracking  
6. Measurements + Analytics  
7. Authentication  
8. Coach roster  
9. PWA hardening  
10. Full EN/FA localization pass  
