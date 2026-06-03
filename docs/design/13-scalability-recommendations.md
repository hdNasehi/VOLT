# Future Scalability Recommendations

## Product

| Area | Recommendation |
|------|----------------|
| Social | Design tokens reserved for “Community” tab — don’t reuse purple |
| Nutrition | New semantic color (e.g. teal) — never lime/purple |
| Wearables | Dashboard widget slot pattern established in AT01 |
| Marketplace | Coach selling plans — separate commerce card style |
| Teams / gyms | Multi-coach org switcher in profile |

## Design system

| Topic | Action |
|-------|--------|
| Tokens | Export Figma variables matching hex table in `01-design-system.md` |
| Components | Document MudBlazor mapping when implementing |
| Dark-only MVP | Plan light theme as inverted surfaces, not gray bootstrap |
| Motion library | Lottie for PR celebration, workout complete |
| Illustration set | 6 empty states + onboarding — consistent stroke weight |

## IA

| Scale | Approach |
|-------|----------|
| 50+ screens | Maintain screen IDs (AT01, C01) in tickets |
| Role expansion | Tab bar config per role JSON |
| Admin | Break to separate shell at 15+ admin screens |

## Localization

| Item | Note |
|------|------|
| FA | Vazirmatn QA on all KPI screens |
| EN | Shorter copy for buttons max 22 chars |
| Dates | Locale-aware; charts stay LTR for numbers |
| Units | kg/lb, cm/in toggle in settings |

## Accessibility

- WCAG AA contrast on lime/purple against `#16181D`  
- Focus rings for keyboard/PWA desktop  
- Reduced motion tier  

## Handoff to Blazor + MudBlazor

1. Implement tokens in `variables.css` (align with existing VOLT scaffold)  
2. Build VOLT wrappers per `04-components-inventory.md`  
3. Screen-by-screen: Athlete dashboard → Active workout → AI Coach → Progress photos  
4. RTL pass on each milestone  
5. Usability test 5 athletes, 3 coaches on mobile PWA  

## Design deliverables checklist

- [x] Complete design system  
- [x] Information architecture  
- [x] User journey maps  
- [x] Screen inventory (~55 screens)  
- [x] Component inventory  
- [x] Per-domain screen specs (auth, athlete, coach, photos, library, workout, analytics, AI)  
- [ ] Figma high-fidelity mockups (recommended next step)  
- [ ] Interactive prototype (AI flow + active workout)  

---

*Document version 1.0 — aligns with VOLT Blazor IWA + MudBlazor implementation roadmap.*
