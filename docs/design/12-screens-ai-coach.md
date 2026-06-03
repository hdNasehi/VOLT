# Screens: AI Coach (Flagship)

**Semantic color: Purple only.** Center tab elevated.

---

## AT14 — AI Coach Hub

| # | Detail |
|---|--------|
| **Purpose** | Entry to Chat + Generate modes |
| **Goals** | Get coaching advice or new plan fast |
| **Hierarchy** | Header → Mode toggle → Content area → Input zone |
| **Components** | Segmented control `Chat | Generate`, thread or form, bottom input |

### Mode A — Chat

| Element | Spec |
|---------|------|
| Message list | User bubbles right `bg/elevated`; AI left purple tint 8% |
| Suggested questions | Pills above input; horizontal scroll |
| Quick actions | “Analyze week”, “Deload?”, “Swap exercise” chips |
| Input bar | Text + mic + send; send purple when active |
| Voice | Hold-to-talk; waveform purple |
| Typing | Three dots + “Coach is thinking…” |
| Rich cards | Embedded: exercise list, mini chart, plan summary |

| # | Detail |
|---|--------|
| **Mobile** | Full height; keyboard lifts input |
| **Tablet** | Optional sidebar: conversation list |
| **Empty** | Welcome card: “I’m your AI coach” + 4 suggestions |
| **Loading** | Skeleton bubbles |
| **Error** | “Couldn’t reach AI” retry purple button |
| **Rationale** | ChatGPT-level polish; fitness-specific cards |

### Mode B — Generate Plan

| Step | UI |
|------|-----|
| Form | Goal dropdown, Experience, Equipment multi-select, Days/week slider |
| CTA | “Generate plan” purple gradient full-width |
| Thinking | Full-screen purple pulse; steps checklist animating |
| Progress | ① Profile ② Exercises ③ Schedule ④ Review |
| Result | Day cards expandable; swap exercise per row |
| Actions | Save plan \| Regenerate \| Edit in builder (coach) |

**Generation progress visual:** Linear bar purple; estimated time ~30s copy.

---

## AI rich card types

| Card | Content |
|------|---------|
| Progress analysis | Sparkline + 3 bullet insights |
| Exercise recommendation | Thumb + “Add to today” |
| Deload warning | Warning icon + explanation |
| PR prediction | Display numeral lime (exception: metric not AI brand) |

*Rule: Card chrome purple; metric highlights inside may use lime for data readability.*

---

## Coach AI variant (CAI)

- Bulk athlete question: “Who missed 2+ sessions?”  
- Output: table card with athlete avatars → tap profile  

---

## Accessibility

- Voice input labels  
- Screen reader announces AI vs user messages  
- Reduced motion: disable tab pulse and generation particles  

---

## RTL (FA)

- User bubbles flip to left in RTL; AI to right  
- Input send icon mirrored  
- Suggested pills scroll from right edge  
