# Screens: Workout Plan Builder & Tracking

---

## C04 — Workout Plan Builder (Coach, mobile-first)

| # | Detail |
|---|--------|
| **Purpose** | Compose multi-day plans quickly |
| **Goals** | Add days, exercises, sets/reps; clone |
| **Hierarchy** | Plan meta → Day tabs → Exercise list → Editor sheet |

### Structure

```
Plan name [________]
[ Day 1 | Day 2 | Day 3 | + ]
┌ Exercise row ─────────────┐
│ Bench Press    4×8  ≡   │
└───────────────────────────┘
[+ Add exercise]
```

| Feature | Interaction |
|---------|-------------|
| Drag reorder | Long-press handle; haptic |
| Edit sets/reps | Bottom sheet numeric steppers |
| Clone plan | Duplicate icon plan header |
| Add day | Pill “+ Day” |

| # | Detail |
|---|--------|
| **Mobile** | Day tabs horizontal scroll |
| **Tablet** | Days left column; exercises right |
| **Empty day** | “Add exercise” dashed card |
| **Loading** | Saving indicator nav bar |
| **Visual** | Premium: no spreadsheet look; card-based rows |

---

## AT03 — Active Workout (Athlete)

| # | Detail |
|---|--------|
| **Purpose** | Log session fast in gym |
| **Goals** | Complete sets; rest; beat PRs |
| **Hierarchy** | Timer bar → Current exercise → Sets → Next up |
| **Components** | Progress bar exercises; set rows; rest timer overlay |

| Element | Spec |
|---------|------|
| Set row | Prev ghost text; weight input; reps input; check |
| Rest timer | Full-width sheet; -15s +15s; skip |
| Previous records | Ghost “Last: 80×8” |
| Exercise nav | Swipe or next/prev bar |

| # | Detail |
|---|--------|
| **Mobile** | Hide bottom nav; keep minimal top bar (elapsed time) |
| **Interaction** | Check set → lime flash; auto-start rest |
| **Error** | Offline queue badge |
| **Rationale** | Hevy-like speed; big touch targets |

---

## AT04 — Workout Complete

- Duration, volume, PR badges carousel  
- Lime celebration header  
- Share card (optional image export)  
- CTA “Done” → Dashboard  

**Mood:** High energy copy — “Session crushed” not “Workout saved”.
