# VOLT — Requirements Gap Analysis

**Audit date:** 2026-06-05  
**Scope:** Full solution vs. Coach–Athlete–Gym Management Platform requirements  
**Method:** Static code audit of `GymCoach.Api`, `GymCoach.Database`, `GymCoach.Shared`, `GymCoach.Client`, tests

---

## Executive Summary

The solution has a **coach-centric fitness data model** (19 entities), a **working Coach Dashboard API**, athlete roster endpoints, and a **mobile-first Blazor WASM shell** with VOLT wrapper components. It is **not yet a production platform**: Identity/JWT, role-based panels, payment, gym management, request/assessment workflows, chat, notifications, and most API domains are missing or stubbed.

| Area | Coverage | Priority |
|------|----------|----------|
| Data model | ~40% | P0 |
| API layer | ~15% | P0 |
| Identity & roles | ~5% | P0 |
| Payment & settlement | 0% | P0 |
| Workflows (requests, assessments) | ~10% | P1 |
| Role-based UI panels | ~5% | P1 |
| Chat & notifications | 0% | P2 |
| Tests (80% target) | ~15% | P2 |

---

## 1. Missing Requirements Report

### Implemented ✅

| Requirement | Evidence |
|-------------|----------|
| Coach-centric model | `CoachAthlete`, coach-scoped roster & dashboard |
| Athlete phone linking | `AthletePhoneService`, placeholder athletes |
| Workout plan structure | `WorkoutPlan`, `ProgramDay`, `ProgramDayExercise`, `WorkoutPlanItem` |
| Progress data model (partial) | `Measurement`, `ProgressPhoto`, `PersonalRecord`, `WorkoutSession` |
| Exercise catalog (entity) | `Exercise` + media/steps/alternatives |
| Bilingual UI foundation | `ILocalizationService`, EN/FA keys |
| VOLT component wrappers | 11 `Volt*` components + bUnit tests |
| PWA shell | manifest, service worker |
| Layered architecture (no DDD/CQRS) | Api → Services → DbContext |
| Result / PagedResult types | `GymCoach.Shared/Common` |

### Missing ❌

| Requirement | Gap |
|-------------|-----|
| ASP.NET Identity + JWT | No Identity packages; coach ID from config |
| Phone as unique identity | Phone unique on Athlete only; no unified `ApplicationUser` |
| 5 roles (SuperAdmin … Athlete) | `UserRole` enum has 3 unused values |
| Role-based dashboards & nav | Single coach-oriented bottom nav; AI tab still present |
| Coach approval workflow | No `PendingApproval` / approve-reject |
| Athlete coach request workflow | Coach-initiated add-by-phone only |
| Assessment request/review workflow | Static `Assessment` record; no photo slots/submissions |
| Workout plan payment lifecycle | No price, `PendingPayment`, payment activation |
| Online payment + commission | No payment entities or gateway integration |
| Settlement (monthly + instant-ready) | No settlement entities |
| Gym management | No `Gym` entity or gym panels |
| Chat (offline Phase 1) | No messages entity/API |
| In-app + SMS notifications | Dashboard alerts only; not persisted notifications |
| SuperAdmin / SystemAdmin panels | Not started |
| GymManager panel | Not started |
| Athlete panel (browse coaches, purchase plans) | Not started |
| Nutrition future-ready entities | Not started |
| Remove AI Coach Phase 1 | `/ai-coach` page + nav + stub API still exist |
| Pagination on list APIs | `PagedResult` unused |
| 80%+ component test coverage | ~11 component test files only |

### Conflicts to resolve ⚠️

| Earlier spec | New spec | Resolution |
|--------------|----------|------------|
| Direct coach–athlete (no approval) | Coach approval + athlete requests | **Implement workflows**; keep direct link after acceptance |
| No AI Phase 1 | AI nav/page exists | Remove from nav; stub API deprecated |
| `ProgramStatus`: Active/Completed/Archived | Draft/Active/Completed/Cancelled/PendingPayment | **Extend enum** + migration |

---

## 2. Missing Entities Report

### Existing (19)

`Coach`, `Athlete`, `CoachAthlete`, `Assessment`, `Exercise` (+5 exercise-related), `WorkoutPlan`, `WorkoutPlanItem`, `ProgramDay`, `ProgramDayExercise`, `WorkoutSession`, `WorkoutSessionItem`, `Measurement`, `PersonalRecord`, `ProgressPhoto`, `CoachNote`

### Missing entities

| Entity | Purpose | Module |
|--------|---------|--------|
| `ApplicationUser` | ASP.NET Identity; phone unique | Identity |
| `Gym` | Gym profile, commission rate | Gym |
| `GymCoach` | Gym ↔ Coach M:N | Gym |
| `GymAthlete` | Gym ↔ Athlete M:N | Gym |
| `GymAnnouncement` | Gym announcements | Gym |
| `AthleteCoachRequest` | Athlete → coach request + intake data | Coaching |
| `CoachAssessmentRequest` | Coach-requested assessment cycle | Assessments |
| `AssessmentPhotoSlot` | Required photo definitions | Assessments |
| `AssessmentPhotoSubmission` | Athlete uploads + review status | Assessments |
| `ExerciseCategory` | Admin exercise catalog grouping | System Admin |
| `PaymentOrder` | Plan purchase order | Payments |
| `PaymentTransaction` | Gateway transaction | Payments |
| `CoachEarning` | Coach share after payment | Payments |
| `GymCommission` | Gym share after payment | Payments |
| `SettlementBatch` | Monthly/instant settlement runs | Payments |
| `ChatMessage` | Coach–athlete messaging | Chat |
| `Notification` | In-app (+ SMS channel field) | Notifications |
| `NutritionPlan` | Future nutrition (stub) | Nutrition |
| `Meal` | Future nutrition (stub) | Nutrition |
| `MealItem` | Future nutrition (stub) | Nutrition |
| `PlatformSetting` | SuperAdmin platform config | Admin |

### Entity field gaps (existing)

| Entity | Missing fields |
|--------|----------------|
| `Coach` | `UserId`, `ApprovalStatus`, `Bio`, `MaxPrice?` |
| `WorkoutPlan` | `Price`, `DaysPerWeek`; status enum expansion |
| `Measurement` | `Neck` (required list) |
| `Assessment` | Workflow fields OR superseded by new assessment entities |
| `ProgramDayExercise` | `SupersetGroupId` (superset support) |

---

## 3. Missing APIs Report

### Implemented endpoints

| Method | Route | Notes |
|--------|-------|-------|
| POST | `/api/auth/register` | Links external UserId to athlete |
| GET | `/api/athletes` | Coach roster + status filter |
| GET | `/api/athletes/{id}` | Single athlete |
| POST | `/api/athletes/check-phone` | Phone lookup |
| POST | `/api/athletes/add-by-phone` | Coach adds athlete |
| GET | `/api/coach/dashboard` | Dashboard aggregates |
| GET | `/health` | Health check |
| GET | `/api/exercises` etc. | **Stubs return `[]`** |

### Missing API domains

| Controller | Required endpoints (minimal) |
|------------|------------------------------|
| **Auth** | POST login, refresh, logout; GET me; role claims |
| **Coaches** | CRUD, approval (SystemAdmin), browse (Athlete), pricing |
| **Athletes** | PUT profile; requests CRUD; accept/reject (Coach) |
| **Gyms** | CRUD; assign coaches/athletes; announcements |
| **WorkoutPlans** | CRUD; publish; activate after payment; list by coach/athlete |
| **WorkoutTracking** | Start/complete session; log sets |
| **Progress** | Measurements CRUD; photos CRUD; PR list; charts data |
| **Assessments** | Request, submit photos, review, reject with reason |
| **Payments** | Create order, pay callback, history |
| **Settlements** | Monthly batch (admin); earning reports |
| **Messages** | Send/list/mark read |
| **Notifications** | List/mark read |
| **Exercises** | Admin CRUD + categories |
| **Users** | SuperAdmin/SystemAdmin user management |
| **Platform** | Settings CRUD |

All list endpoints need **pagination + filtering** per architecture rules.

---

## 4. Missing Pages Report

### Existing pages (8 routes)

| Route | Page | Role assumed |
|-------|------|--------------|
| `/` | Home (Coach Dashboard) | Coach |
| `/athletes` | Athletes list | Coach |
| `/library` | Library placeholder | Generic |
| `/ai-coach` | AI placeholder | **Remove Phase 1** |
| `/workouts` | Workouts placeholder | Generic |
| `/progress` | Progress placeholder | Generic |
| `/profile` | Profile placeholder | Generic |
| `/not-found` | 404 | — |

### Missing pages by role

#### SuperAdmin (0/7)
Dashboard, Users, Coaches, Athletes, Gyms, Platform Settings, Reports

#### SystemAdmin (0/5)
Dashboard, Coach Approvals, User Management, Exercise Catalog, Categories

#### GymManager (0/5)
Dashboard, Gym Profile, Coaches, Athletes, Announcements

#### Coach (1/8)
✅ Dashboard · ❌ Athlete Requests, Assessments, Workout Plans, Progress Tracking, Messages, Profile (full), Add Athlete flow page

#### Athlete (0/9)
Dashboard, Coaches, My Requests, Assessments, My Programs, Workout Session, Progress, Messages, Profile

**Total missing:** ~34 pages + role-specific layouts/navigation

---

## 5. Missing Components Report

### Existing component folders

```
Components/Common/     (11 Volt* wrappers — tested)
Components/Coach/      (CoachDashboard)
Components/Athlete/    (AthleteList)
Components/Dashboard/  (DashboardSummary — legacy)
Layout/                (VoltAppShell, VoltTopBar, VoltBottomNav)
```

### Missing component modules

| Folder | Components needed |
|--------|-------------------|
| `Auth/` | LoginForm, RegisterForm, OtpVerify |
| `Admin/` | UserTable, PlatformSettingsForm, ReportsSummary |
| `Gym/` | GymProfileCard, AssignCoachDialog, AnnouncementList |
| `Coach/` | AthleteRequestList, AssessmentReview, PlanBuilder, PlanDayEditor, MessageThread |
| `Athlete/` | CoachBrowse, RequestForm, AssessmentUpload, ActiveWorkout, ProgressChart |
| `Assessments/` | PhotoSlotGrid, PhotoReviewCard, RejectionDialog |
| `WorkoutPlans/` | PlanCard, PlanStatusBadge, ExercisePicker, SupersetGroup |
| `Payments/` | CheckoutSummary, PaymentStatus |
| `Messages/` | MessageList, MessageComposer, ReadReceipt |
| `Notifications/` | NotificationBell, NotificationList |
| `Progress/` | MeasurementForm, PhotoTimeline, PrTable, BeforeAfterCompare |
| `Shared/` | RoleGuard, PaginatedList, FilterBar, ConfirmRejectDialog |

Each needs `.razor`, `.razor.cs`, `.razor.css` per styling rules.

---

## 6. Missing Permissions Report

### Current state

- No `[Authorize]` on any endpoint
- No JWT middleware
- No policy definitions
- Client has no auth state / role routing

### Required permission matrix (policies)

| Policy | Roles |
|--------|-------|
| `SuperAdminOnly` | SuperAdmin |
| `SystemAdminOrAbove` | SuperAdmin, SystemAdmin |
| `GymManagerOrAbove` | SuperAdmin, SystemAdmin, GymManager |
| `CoachOnly` | Coach (+ approved status) |
| `AthleteOnly` | Athlete |
| `ManageExercises` | SystemAdmin, SuperAdmin |
| `ApproveCoaches` | SystemAdmin, SuperAdmin |
| `ManageGym` | GymManager (own gym), SuperAdmin |
| `ManageOwnAthletes` | Coach (linked athletes) |
| `ManageOwnPlans` | Coach (own plans) |
| `PurchasePlans` | Athlete |
| `ViewOwnProgress` | Athlete, linked Coach |
| `SendMessages` | Coach, Athlete (linked pair) |
| `ManageSettlements` | SuperAdmin, SystemAdmin |
| `ManagePlatformSettings` | SuperAdmin |

### Client-side

- Role-based route guards (`AuthorizeView` / custom `RoleGuard`)
- Separate `Layout` + `BottomNav` per role panel
- Redirect unauthenticated → login

---

## Implementation Plan (incremental batches)

### Batch A — Backend foundation (current)
- Expand enums, entities, Identity, JWT
- Repositories + core services
- Controllers: Auth, Coaches, WorkoutPlans, Payments, Progress
- EF migration

### Batch B — Workflows
- AthleteCoachRequest accept/reject
- CoachAssessmentRequest + photo workflow
- Payment → activate plan + commission split

### Batch C — Admin & Gym APIs
- Gym CRUD, announcements
- Coach approval, exercise catalog admin
- Platform settings, settlements

### Batch D — Client role panels (minimal)
- Auth pages + JWT client handler
- Role layouts + route guards
- Wire existing Coach Dashboard to auth
- Remove AI Coach nav

### Batch E — Remaining pages & components
- Per-role page scaffolds composing Volt components
- bUnit tests toward 80% coverage

---

## Files referenced

- Backend audit: `GymCoach.Api/`, `GymCoach.Database/`, `GymCoach.Shared/`
- UI audit: `GymCoach.Client/GymCoach.Client.Client/Pages/`, `Components/`
- Prior compliance: `docs/PROMPT-COMPLIANCE.md`

---

## Batch A Implementation (2026-06-14)

Implemented backend foundation:

- **Identity + JWT**: `ApplicationUser`, login/register/me, role claims, authorization policies
- **Entities**: Gym, payments, athlete requests, assessments workflow, chat, notifications, nutrition stubs
- **Services**: Auth, WorkoutPlan, Payment, Progress, CoachManagement, AthleteRequest
- **Controllers**: Auth, Coaches, WorkoutPlans, Payments, Progress; Athletes extended
- **Payment flow**: Create order → process payment → activate plan + coach earning + gym commission

Run migration after restore:

```powershell
dotnet restore VOLT.sln
dotnet ef migrations add PlatformFoundation --project GymCoach.Database --startup-project GymCoach.Api
dotnet ef database update --project GymCoach.Database --startup-project GymCoach.Api
```
