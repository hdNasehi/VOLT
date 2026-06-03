# Screens: Measurements & Progress Analytics

---

## AT11 — Measurements List

| Metric types | Weight, body fat %, chest, waist, hips, arms, thighs |
|--------------|--------------------------------------------------------|
| Row | Type, latest value, delta chip, sparkline thumb |
| CTA | Lime “+ Add measurement” |

---

## AT12 — Add / Edit Measurement

- Type picker grid icons  
- Value input large display  
- Date picker  
- Note optional  
- Save sticky bottom  

**Validation:** Range errors inline danger.

---

## Charts (within AT11 / AT13)

| Chart | Spec |
|-------|------|
| Weight line | Lime line; pinch zoom range |
| Body fat | Secondary line `#99A0AB` or success green |
| Circumference | Multi-series toggle per body part |

---

## AT13 — Progress Analytics

| # | Detail |
|---|--------|
| **Purpose** | Data-rich progress without clutter |
| **Sections** | Strength \| Volume \| Consistency \| PRs |

| Widget | Description |
|--------|-------------|
| Strength progress | Top lifts line chart |
| Estimated 1RM | Calculated; display typography |
| Weekly volume | Bar chart by week |
| Heatmap | GitHub-style consistency; lime intensity |
| Milestones | Timeline cards |
| PR board | Sortable list |

| # | Detail |
|---|--------|
| **Mobile** | Vertical stack; filters top |
| **Tablet** | 2×2 dashboard grid |
| **Empty** | “Log workouts to see analytics” |
| **Loading** | Chart skeletons |
| **Visual** | Data-dense but 24px section spacing; no grid lines overload |

**UX rationale:** WHOOP-inspired clarity; Strong-inspired PR focus.
