# VOLT Component Inventory

Each component: **Purpose · States · Variants · Mobile · Tablet**

---

## Buttons

**Purpose:** Primary actions; never more than one lime CTA per viewport.

| Variant | Visual | Use |
|---------|--------|-----|
| Primary Training | Lime fill, dark text | Start workout, Save set |
| Primary AI | Purple gradient, white text | Generate plan, Ask AI |
| Secondary | `bg/elevated` border | Cancel, Back |
| Ghost | Text only lime/purple | Tertiary links |
| Destructive | Danger fill | Delete photo, Remove athlete |
| Icon FAB | 56px circle | Add exercise (coach builder) |

**States:** Default, Pressed (scale 0.97), Disabled (40% opacity), Loading (spinner inline).

**Mobile:** Full-width primary at bottom sticky above tab bar when contextual.  
**Tablet:** Max-width 360px centered or inline end-aligned in split panes.

---

## Cards

| Variant | Purpose |
|---------|---------|
| Base Card | `bg/card`, 12px radius, 16px padding |
| Hero Card | Full-width, gradient overlay, large title |
| KPI Card | Display number + delta chip + sparkline |
| Workout Card | Thumbnail, title, duration, muscle tags |
| Exercise Card | Grid tile 1:1 image + name |
| AI Insight Card | Purple left border 3px, icon sparkle |
| Media Card | 3:4 aspect progress photo |
| Analytics Card | Chart header + range selector |

**States:** Default, Pressed, Selected (lime outline), Skeleton shimmer.

---

## Dialogs & sheets

| Type | Behavior |
|------|----------|
| Bottom Sheet | Filters, pickers, rest timer quick edit |
| Modal | Confirm delete, auth errors |
| Full-screen takeover | Active workout, photo capture |

**States:** Enter slide-up 300ms; backdrop 60% black; swipe-down dismiss on sheets.

---

## Bottom Navigation

See Design System §6. **States:** Active tab, badge dot (coach messages), AI pulse idle animation.

---

## Search Bars

- Sticky below header on list screens  
- Leading search icon; trailing filter chip  
- **States:** Empty, Focused (elevated border lime), Results loading  

---

## Chips & filters

| Type | Example |
|------|---------|
| Filter chip | Muscle: Chest (toggle) |
| Status chip | At risk (warning) |
| Metric chip | +1.2 kg (success) |

**Mobile:** Horizontal scroll row; multi-select for muscles.  
**Tablet:** Wrap grid in sidebar filter panel.

---

## Avatars & badges

- **Avatar:** 40 / 56 / 80px; ring lime if PR this week  
- **Badge:** Count, “PR”, “AI”, “Pro”; never purple except “AI suggested”  

---

## Workout & exercise cards

**Workout Card:** Day label, exercise count, est. duration, adherence % bar.  
**Exercise Card (list):** Thumbnail, name, last performed weight.  
**Set Row:** Set #, prev, input, RPE optional, checkmark.

---

## Progress rings

- Lime arc on `#1C1F25` track  
- Center: % or reps remaining  
- Sizes: 64px (inline), 120px (dashboard)

---

## Charts

| Chart | Use |
|-------|-----|
| Line | Weight, 1RM trend |
| Bar | Weekly volume |
| Heatmap | Consistency calendar |
| Donut | Macro split (future) |

Dark axes `#99A0AB`; grid `#1C1F25`; tooltip `bg/elevated`.

---

## Empty states

- Illustration: minimal line art (athlete silhouette)  
- Title + one-line description + single CTA  
- Never more than one button  

---

## Skeleton loading

- Pulse animation on `bg/elevated`  
- Match final layout geometry (KPI blocks, list rows)  
- Shimmer 1.2s loop  

---

## AI-specific

| Component | Description |
|-----------|-------------|
| AI Message Bubble | Purple tint assistant; user neutral elevated |
| Suggested Question Pill | Outline purple; horizontal scroll |
| AI Thinking | Three dot pulse + “Analyzing your progress…” |
| Generation Progress | Stepped: Goals → Exercises → Schedule |
| Rich AI Card | Embedded chart or exercise list in thread |

---

## KPI cards

```
┌─────────────────────┐
│ WEIGHT        ▲ 0.8 │
│  82.4 kg            │
│  ─── sparkline ───  │
└─────────────────────┘
```

Delta chip: green up / red down; neutral gray if flat.

---

## Media gallery & comparison

- **Gallery:** Masonry 2-col mobile; 3-col tablet  
- **Comparison slider:** Vertical divider drag; before left, after right; date labels  
- **Upload slot:** Front / Side / Back placeholders with pose outline  

---

## Component QA checklist (handoff)

- [ ] Touch targets ≥ 44px  
- [ ] Lime/purple semantic respected  
- [ ] RTL mirror verified  
- [ ] Skeleton → content transition  
- [ ] Error + empty for every list  
