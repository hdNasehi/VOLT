# Screens: Coach Dashboard, Athlete Profile & Admin

---

## C01 — Coach Dashboard

| # | Detail |
|---|--------|
| **Purpose** | Monitor roster health at a glance |
| **Goals** | Spot risk; quick actions |
| **Hierarchy** | Greeting → Summary KPIs → Risk alerts → Athlete preview list |
| **Components** | 4 KPI cards: Active athletes, Avg adherence, At-risk count, Plans due |
| **Mobile** | KPI 2×2 grid; alerts as stacked cards |
| **Tablet** | KPI row; list + detail split |
| **Empty** | “Invite your first athlete” + share link CTA |
| **Loading** | Skeleton KPI + list rows |
| **Visual** | At-risk uses warning `#FFC24B`; not purple |

**Quick actions (FAB menu):** New plan, Message athlete, Add athlete.

---

## C02 — Athlete List

| Feature | Spec |
|---------|------|
| Search | Sticky; filters adherence, goal, last active |
| Row | Avatar, name, adherence %, last workout, risk dot |
| Swipe | Assign plan (lime), Message |
| Filters sheet | Multi-select chips |

**Populated:** 20+ rows virtualized.  
**Empty:** Illustration + import CSV (tablet).

---

## C03 — Athlete Profile (5 tabs)

### Tab: Overview
- Goals chips, join date, notes (coach only edit)  
- Stats: workouts/month, volume, PR count  

### Tab: Measurements
- Latest metrics table; “Add measurement” (coach log for athlete)  
- Chart previews  

### Tab: Progress Photos
- Grid timeline; coach comment thread per session  

### Tab: Workout Plans
- Active plan card; history list; Assign CTA  

### Tab: Analytics
- Embedded charts: strength, volume, consistency  

| # | Detail |
|---|--------|
| **Mobile** | Tab bar scrollable under header |
| **Tablet** | Tabs vertical left 240px |
| **Rationale** | Single athlete 360° view reduces navigation |

---

## C04 — Workout Plan Builder (coach)

See `10-screens-workout-plan-tracking.md`.

---

## C05 — Assign Plan

Athlete header → Plan picker → Start date → Confirm sheet.

---

## C06 — Adherence Report

Weekly calendar per athlete; export PDF (tablet).

---

## Admin screens (AD01–AD12)

| ID | Screen | Key elements |
|----|--------|--------------|
| AD01 | Admin dashboard | MRR, active coaches, AI token usage |
| AD02 | Coaches list | Search, tier, status |
| AD03 | Coach detail | Athletes count, billing, impersonate |
| AD04 | Athletes global | GDPR export |
| AD05 | Exercises | CRUD table, video preview |
| AD06 | Exercise edit | Muscles multi-select, steps reorder |
| AD07 | Subscriptions | Plans, coupons |
| AD08 | Invoices | Status filters |
| AD09 | AI settings | Model select, rate limits, prompt templates |
| AD10 | Feature flags | Toggle per tenant |
| AD11 | Audit log | Who changed what |
| AD12 | System health | API latency, job queue |

**Admin visual:** Same dark tokens; denser tables allowed; lime for primary actions only; purple reserved for AI settings section header.

**Tablet/desktop:** Left nav 240px; content max-width 1200px.
