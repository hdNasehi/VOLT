# Screens: Progress Photos (Major Feature)

Photos are a **hero visual** — large thumbnails, minimal chrome, gallery-first.

---

## AT07 — Progress Hub (tab entry)

| # | Detail |
|---|--------|
| **Purpose** | Orient across photos, measurements, analytics |
| **Hierarchy** | Sub-tabs: Photos \| Measurements \| Analytics |
| **CTA** | Floating lime “+ Log progress” |

---

## AT08 — Photo Timeline

| # | Detail |
|---|--------|
| **Purpose** | Historical visual journey |
| **Layout** | Date headers; 2-col grid cards 3:4 |
| **Card** | Front thumb dominant; badges Front/Side/Back if set |
| **Interaction** | Tap → detail; long-press multi-select compare |
| **Empty** | Silhouette placeholders + “Take first photos” |
| **Loading** | Shimmer grid |

---

## AT09 — Upload / Capture Flow

| Step | Screen |
|------|--------|
| 1 | Instruction: consistent lighting, same pose |
| 2 | Pose picker: Front → Side → Back (progress dots) |
| 3 | Camera: overlay silhouette; level hint |
| 4 | Review: crop; blur face toggle (privacy) |
| 5 | Upload: progress bar lime |
| 6 | Success: confetti subtle; view timeline |

**Error:** Camera permission sheet with illustration.

---

## AT10 — Before/After Compare

| # | Detail |
|---|--------|
| **Purpose** | Motivation through visual delta |
| **Components** | Date selectors; comparison slider; optional side-by-side |
| **Slider** | Draggable vertical line; haptic ticks |
| **Labels** | Before (left) After (right); weight at capture optional |
| **Tablet** | Side-by-side 50/50 + slider below |

---

## AI Analysis Card (post-upload)

| # | Detail |
|---|--------|
| **Visual** | Purple card below photos |
| **Content** | “Posture”, “Visible changes”, “Suggested focus” bullets |
| **CTA** | “Discuss with AI coach” → AT14 with context |
| **Rationale** | Purple = AI interpretation of images |

---

## Coach view (C03 Photos tab)

- Same timeline + coach private notes field  
- Flag “review requested” badge  

---

## Media gallery component spec

| Variant | Use |
|---------|-----|
| Grid cell | Timeline |
| Hero | Latest session on dashboard teaser |
| Compare frame | Slider |

**Performance UX:** Blur-up placeholder; full-res on tap pinch-zoom.
