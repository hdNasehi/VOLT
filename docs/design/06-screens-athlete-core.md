# Screens: Athlete Dashboard, Profile & Settings

---

## AT01 — Athlete Dashboard (Home tab)

| # | Detail |
|---|--------|
| **Purpose** | Daily command center |
| **Goals** | See next workout; track KPIs; act on AI insight |
| **Hierarchy** | See layout below |
| **Components** | See breakdown |

### Layout hierarchy (mobile, top → bottom)

1. **Header** — “Good morning, Alex” + avatar + notification bell  
2. **Next Workout Hero** — Full-width card; workout name; muscle tags; “Start” lime CTA; est. duration  
3. **KPI Row** — 3 scrollable mini cards: Weight | Body fat % | Streak (days)  
4. **AI Insight Card** — Purple accent; 2-line insight; “Ask AI” ghost  
5. **Weight Trend Chart** — 7D/30D/90D segmented control; line chart lime  
6. **Consistency Card** — Heatmap week grid; % adherence  
7. **Personal Records Carousel** — Horizontal cards: lift name, PR weight, date  

| # | Detail |
|---|--------|
| **Mobile** | Single column 16px gutters; hero 180px min height |
| **Tablet** | 2-col: hero left 60%; KPI+chart right |
| **Interaction** | Pull-to-refresh; tap hero → AT02 |
| **Empty** | No plan: hero = “Connect with coach” CTA |
| **Loading** | Skeleton hero + 3 KPI blocks |
| **Error** | Banner retry for chart data |
| **Visual** | Hero image darkened 40%; lime CTA only primary |
| **Rationale** | Fitbod-style “what’s next” first; data second |

**Wireframe (ASCII):**
```
[ Header                    🔔 ]
┌─────────────────────────────┐
│ NEXT WORKOUT          45min │
│ Upper Power                 │
│ [ Chest Back ]    [ START ] │
└─────────────────────────────┘
[ 82.4kg ] [ 14.2% ] [ 12🔥 ]
┌─ AI Insight (purple) ───────┐
└─────────────────────────────┘
[ Weight chart 7D|30D|90D     ]
[ Consistency heatmap         ]
[ PR carousel →               ]
```

---

## AT02 — Next Workout Detail

Exercise list grouped by superset; equipment icons; “Start workout” sticky bottom.

---

## AT16 — Profile

| Section | Content |
|---------|---------|
| Header | Avatar 80px, name, coach name link |
| Stats row | Workouts total, hours, PRs |
| Menu | Goals, Units, Connected coach |
| Logout | Danger text |

---

## AT17 — Settings

| Item | Screen |
|------|--------|
| Profile edit | Name, photo, bio |
| Language | EN / FA with preview |
| Theme | Dark (only MVP) / future light |
| Notifications | Toggles per category |
| Security | Change password, 2FA future |
| Subscription | Plan card, upgrade CTA |

**Bilingual:** Settings is canonical RTL test screen; all rows mirror.

---

## Bottom nav (persistent)

Documented in `01-design-system.md`; hidden during AT03 Active Workout.
