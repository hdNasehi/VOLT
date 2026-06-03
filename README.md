# VOLT GymCoach

Mobile-first Blazor Web App for gym coaching with Interactive WebAssembly (IWA), PWA support, and MudBlazor UI.

## Solution (`VOLT.sln`)

| Project | Role |
|---------|------|
| `GymCoach.Api` | REST API (no direct DB access from client) |
| `GymCoach.Client` | Blazor host (SSR shell, static assets, PWA manifest) |
| `GymCoach.Client.Client` | Interactive WASM UI, VOLT components, services |
| `GymCoach.Shared` | DTOs, API routes, offline sync contracts |
| `GymCoach.Database` | EF Core data access |
| `GymCoach.Client.Tests` | bUnit + xUnit + FluentAssertions |

## Stack notes

- **Target framework:** `net10.0` (environment has .NET 10 SDK; architecture matches .NET 9 Blazor Web App + IWA requirements).
- **Render mode:** Interactive WebAssembly only (no Server / Auto).
- **UI:** MudBlazor with VOLT wrapper components under `Components/Common/`.
- **Offline:** `IOfflineSyncQueue` + `PendingSyncOperation` in Shared/Client (ready for IndexedDB sync).

## Run locally

```bash
# Terminal 1 – API
dotnet run --project GymCoach.Api

# Terminal 2 – Blazor app
dotnet run --project GymCoach.Client/GymCoach.Client
```

Configure `ApiBaseUrl` in `GymCoach.Client.Client/wwwroot/appsettings.Development.json` (default: `https://localhost:7219`).

## Tests

```bash
dotnet test VOLT.sln
```

## UI/UX design (no code)

Complete mobile-first product design specs live in [`docs/design/`](docs/design/README.md):

- Design system (colors, type, navigation, RTL)
- ~55 screen inventory + user journeys
- Component library (buttons, cards, AI, photos, charts)
- Flagship flows: **AI Coach**, **Progress Photos**, **Active Workout**

## Feature generation

When requesting **"Generate the complete implementation for &lt;feature&gt;"**, the solution expects: folders, DTOs, interfaces, services, API endpoints, repositories, Razor components (`.razor` / `.razor.cs` / `.razor.css`), localization, bUnit tests, DI, and navigation.
