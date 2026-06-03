# VOLT Design System

## 1. Brand & visual language

### Personality
| Attribute | Expression |
|-----------|------------|
| Premium | Deep blacks, restrained borders, subtle glass on hero cards |
| Energetic | Lime accents on CTAs, streaks, PR badges |
| AI-forward | Purple gradients, glow, particle micro-motion on AI tab |
| Sport-focused | Bold numerals, exercise thumbnails, muscle heatmaps |
| Minimal | One primary action per screen; generous negative space |

### Anti-patterns (reject)
- Bootstrap-style cards, default blue links, gray admin tables  
- Dense data grids on mobile  
- Purple used for non-AI actions  
- Lime used for AI chat or plan generation  

---

## 2. Color system

| Token | Hex | Usage |
|-------|-----|--------|
| `bg/base` | `#0A0B0D` | App background, full bleed |
| `bg/card` | `#16181D` | Cards, list rows, modals |
| `bg/elevated` | `#1C1F25` | Nested surfaces, chips, input fields |
| `accent/training` | `#CBFB54` | Workouts, PRs, streaks, primary CTAs (non-AI) |
| `accent/ai` | `#7C4DFF` | AI tab, AI cards, generation, chat assistant |
| `text/primary` | `#F4F6F8` | Headlines, values |
| `text/secondary` | `#99A0AB` | Labels, captions, metadata |
| `semantic/success` | `#35D9A0` | Goal hit, plan complete, positive delta |
| `semantic/warning` | `#FFC24B` | Adherence risk, deload suggested |
| `semantic/danger` | `#FF5C49` | Errors, missed workouts, injury flags |

### Gradients
- **AI Hero:** `#7C4DFF` → `#5B2FD9` (135°), optional outer glow `rgba(124,77,255,0.35)`  
- **Training Hero:** `#CBFB54` at 12% opacity overlay on `#16181D` + lime border 1px `rgba(203,251,84,0.25)`  
- **Chart fill:** Lime or purple at 20% → 0% vertical fade  

### Semantic rules
| Domain | Color | Examples |
|--------|-------|----------|
| Body & training | Lime | Next workout, log set, weight KPI, volume chart |
| AI | Purple | Chat bubbles (assistant), generate plan, AI insight |
| Neutral analytics | White/gray lines | Axis, grid; highlight series in lime |

---

## 3. Typography

| Role | Font (EN) | Font (FA) | Scale (mobile) |
|------|-----------|-----------|----------------|
| Display KPI | Space Grotesk Bold | Vazirmatn Bold | 40–56px / line 1.0 |
| H1 | Space Grotesk Semibold | Vazirmatn Bold | 28px |
| H2 | Space Grotesk Medium | Vazirmatn Semibold | 22px |
| H3 | Space Grotesk Medium | Vazirmatn Medium | 18px |
| Body | Hanken Grotesk Regular | Vazirmatn Regular | 16px / 24px line |
| Caption | Hanken Grotesk Medium | Vazirmatn Regular | 13px / 18px |
| Overline | Hanken Grotesk Semibold | Vazirmatn Medium | 11px, +0.08em, uppercase |

**Numeric displays:** Tabular lining figures; KPI blocks use Display size with unit in Caption below (e.g. `82.4` + `kg`).

---

## 4. Spacing, radius, elevation

| Token | Value | Use |
|-------|-------|-----|
| `space/xs` | 4px | Icon gaps |
| `space/sm` | 8px | Inline chips |
| `space/md` | 16px | Card padding |
| `space/lg` | 24px | Section gaps |
| `space/xl` | 32px | Hero padding |
| `radius/sm` | 8px | Chips, inputs |
| `radius/md` | 12px | Cards |
| `radius/lg` | 20px | Bottom sheets, hero |
| `radius/pill` | 999px | FAB, tab pill |
| `elevation/1` | `0 2px 8px rgba(0,0,0,0.35)` | Cards |
| `elevation/ai` | `0 8px 32px rgba(124,77,255,0.25)` | AI tab button |

**Safe areas:** All fixed nav respects `env(safe-area-inset-*)`; min touch target 44×44px.

---

## 5. Iconography & motion

- **Style:** 2px stroke, rounded caps; filled only for active tab / AI  
- **Motion:** 200ms ease-out UI; 350ms spring for bottom sheet; rest timer pulse 1s loop  
- **Haptics (PWA):** Light on set complete; medium on PR; success on workout finish  

---

## 6. Navigation — Bottom Tab Bar

```
[ Home ] [ Library ] [ ( AI ) ] [ Progress ] [ Profile ]
                         ↑
              Elevated 56px circle, purple gradient,
              slight float (-8px), soft glow
```

| Tab | Icon | Active color |
|-----|------|----------------|
| Home | House / dashboard | Lime underline dot |
| Library | Dumbbell grid | Lime |
| AI Coach | Sparkle / brain | Purple fill + glow |
| Progress | Chart / camera | Lime |
| Profile | User | Lime |

**Tablet:** Same tabs; optional persistent side rail for coach role (secondary).

---

## 7. Bilingual (EN / FA)

| Aspect | LTR (EN) | RTL (FA) |
|--------|----------|----------|
| Typography | Space Grotesk + Hanken | Vazirmatn all roles |
| Nav order | Home left → Profile right | Mirrored |
| Back chevron | Points left | Points right |
| Charts | Y-axis left | Y-axis right optional |
| AI bubble align | User right, AI left | Mirrored |
| Numeric KPI | Always LTR digits inside RTL shell | `dir="ltr"` on number blocks |

**Settings:** Language toggle with instant preview; no app restart required in UX spec.

---

## 8. MudBlazor mapping (for future implementation)

| VOLT component | MudBlazor base | Notes |
|----------------|----------------|-------|
| VoltCard | MudCard | Custom `bg/card`, no default shadow |
| VoltButton Primary | MudButton | Lime filled |
| VoltButton AI | MudButton | Purple gradient |
| VoltBottomNav | Custom + MudFab (center) | AI tab |
| VoltKpiCard | MudPaper + typography | Display numerals |
| VoltProgressRing | MudProgressCircular | Lime track |
| VoltAiCard | MudPaper | Purple border glow |
| VoltChart | MudChart / custom SVG | Dark theme axes |

*This section is reference only for dev handoff; no code in design deliverable.*
