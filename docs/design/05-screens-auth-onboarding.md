# Screens: Authentication & Onboarding

*Template per screen: Purpose · Goals · Hierarchy · Components · Mobile · Tablet · Interaction · Empty · Loading · Error · Visual · UX rationale*

---

## A01 — Splash

| # | Detail |
|---|--------|
| **Purpose** | Brand moment; app init |
| **User goals** | Recognize VOLT; wait <2s |
| **Hierarchy** | ① Logo ② Tagline ③ Loader |
| **Components** | Full-bleed `#0A0B0D`, animated logo mark (lime pulse), wordmark Space Grotesk |
| **Mobile** | Centered; safe area top/bottom |
| **Tablet** | Same; optional subtle particle field |
| **Interaction** | Auto-advance to onboarding (first launch) or login |
| **Loading** | Indeterminate lime arc below logo |
| **Error** | Retry if config fetch fails |
| **Visual** | No buttons; minimal |
| **Rationale** | Premium apps don’t flash white; instant dark |

**Wireframe:** `[ Logo ]` / `Gym Management & AI` / `( ••• )`  
**Hi-fi:** Logo with soft lime glow; tagline `#99A0AB`; 2px lime progress line.

---

## A02 — Onboarding (3 slides)

| Slide | Headline | Visual |
|-------|----------|--------|
| 1 | Train smarter | Athlete lifting, lime accent UI mock |
| 2 | Track real progress | Photo compare slider preview |
| 3 | Your AI coach | Purple chat bubble mock |

| # | Detail |
|---|--------|
| **Purpose** | Communicate value before signup |
| **Goals** | Understand pillars; reach CTA |
| **Hierarchy** | Illustration → H1 → body → dots → CTA |
| **Components** | Carousel, pagination dots, Primary “Get started”, Ghost “Sign in” |
| **Mobile** | Full-bleed art 45% height |
| **Tablet** | Split: art left, copy right |
| **Interaction** | Swipe; skip top-right |
| **States** | Last slide CTA lime full-width |

---

## A03 — Login

| # | Detail |
|---|--------|
| **Purpose** | Authenticate returning users |
| **Goals** | Fast email/password login |
| **Hierarchy** | Back → Title → Form → CTA → Social (optional) → Register link |
| **Components** | Email field, password + reveal, Remember me, Forgot link, Primary button |
| **Mobile** | Single column; keyboard pushes CTA sticky |
| **Tablet** | Card max 400px centered |
| **Interaction** | Inline validation on blur |
| **Empty** | N/A |
| **Loading** | Button spinner |
| **Error** | Shake form; banner “Invalid credentials” |
| **Visual** | Inputs `bg/elevated`; focus ring lime |
| **Rationale** | No clutter; coach/athlete role from token |

**States:** Default, invalid email, wrong password, locked account, network error.

---

## A04 — Register

**Extra fields:** Name, email, password, role selector (Athlete | Coach), terms checkbox.

| Role | Post-register |
|------|----------------|
| Athlete | Coach code screen |
| Coach | Business name, athlete count |

**Visual:** Role cards side-by-side; selected = lime border.

---

## A05 — Forgot password

Email only → “Send reset link” → success state with email illustration (check your inbox).

---

## A06 — Reset password

New password + confirm; strength meter (lime segments); success → Login.

---

## A07 — Email verification (optional)

Pending state with resend; verified confetti micro-animation (lime, not purple).

---

## RTL notes (FA)

- Form labels right-aligned  
- Back chevron flips  
- Primary CTA full-width unchanged  
- Use Vazirmatn; numerals in password fields LTR  
