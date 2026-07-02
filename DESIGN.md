---
name: Retail Logic
colors:
  surface: '#f7f9ff'
  surface-dim: '#d7dadf'
  surface-bright: '#f7f9ff'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f1f4f9'
  surface-container: '#ebeef3'
  surface-container-high: '#e5e8ee'
  surface-container-highest: '#e0e3e8'
  on-surface: '#181c20'
  on-surface-variant: '#444655'
  inverse-surface: '#2d3135'
  inverse-on-surface: '#eef1f6'
  outline: '#747686'
  outline-variant: '#c4c5d7'
  surface-tint: '#294fdb'
  primary: '#264dd9'
  on-primary: '#ffffff'
  primary-container: '#4568f3'
  on-primary-container: '#fffbff'
  inverse-primary: '#b8c3ff'
  secondary: '#575f67'
  on-secondary: '#ffffff'
  secondary-container: '#d8e1ea'
  on-secondary-container: '#5b646b'
  tertiary: '#765700'
  on-tertiary: '#ffffff'
  tertiary-container: '#956e00'
  on-tertiary-container: '#fffbff'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#dde1ff'
  primary-fixed-dim: '#b8c3ff'
  on-primary-fixed: '#001355'
  on-primary-fixed-variant: '#0035bd'
  secondary-fixed: '#dbe4ed'
  secondary-fixed-dim: '#bfc8d0'
  on-secondary-fixed: '#141d23'
  on-secondary-fixed-variant: '#3f484f'
  tertiary-fixed: '#ffdf9f'
  tertiary-fixed-dim: '#f9bd22'
  on-tertiary-fixed: '#261a00'
  on-tertiary-fixed-variant: '#5c4300'
  background: '#f7f9ff'
  on-background: '#181c20'
  surface-variant: '#e0e3e8'
typography:
  headline-lg:
    fontFamily: Hanken Grotesk
    fontSize: 32px
    fontWeight: '700'
    lineHeight: '1.2'
  headline-md:
    fontFamily: Hanken Grotesk
    fontSize: 24px
    fontWeight: '600'
    lineHeight: '1.3'
  headline-sm:
    fontFamily: Hanken Grotesk
    fontSize: 20px
    fontWeight: '600'
    lineHeight: '1.4'
  body-lg:
    fontFamily: Hanken Grotesk
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.5'
  body-md:
    fontFamily: Hanken Grotesk
    fontSize: 14px
    fontWeight: '400'
    lineHeight: '1.5'
  label-md:
    fontFamily: Hanken Grotesk
    fontSize: 12px
    fontWeight: '600'
    lineHeight: '1'
    letterSpacing: 0.05em
  headline-lg-mobile:
    fontFamily: Hanken Grotesk
    fontSize: 24px
    fontWeight: '700'
    lineHeight: '1.2'
rounded:
  sm: 0.125rem
  DEFAULT: 0.25rem
  md: 0.375rem
  lg: 0.5rem
  xl: 0.75rem
  full: 9999px
spacing:
  container-max: 1440px
  gutter: 1.5rem
  margin-md: 2rem
  padding-card: 1.25rem
  stack-sm: 0.5rem
  stack-md: 1rem
---

## Brand & Style
This design system is engineered for efficiency, clarity, and reliability within the retail management sector. It targets administrative and floor staff who require high-density information layouts that remain legible over long working hours.

The visual style is **Corporate / Modern**, leaning heavily into a refined Bootstrap 5.2.0 aesthetic. It utilizes a clean, light-mode foundation with generous whitespace to reduce cognitive load during complex tasks like inventory tracking and point-of-sale operations. The interface feels "utilitarian-premium"—functional enough for a warehouse, yet polished enough for a boutique storefront.

## Colors
The palette evolves the core colors from the source material into a professional, high-contrast system. 

- **Primary (Blue):** A refined, more vibrant blue replaces the default for primary actions and navigation.
- **Success (Green):** A deep, forest-toned green is used for "Save" and "Complete" actions, ensuring high legibility against white backgrounds.
- **Warning/Alert (Yellow):** A warm amber is utilized for stock alerts, providing visibility without being as jarring as pure red.
- **Neutrals:** A range of cool grays (from `#F8F9FA` for backgrounds to `#6C757D` for secondary buttons) creates a sophisticated hierarchy.

## Typography
We employ **Hanken Grotesk** across the entire system. It provides a sharp, contemporary look that maintains excellent readability in data-heavy tables. 

- **Headlines:** Use Bold (700) and SemiBold (600) weights to clearly demarcate sections.
- **Body:** The standard size is set to 14px (`body-md`) for desktop to maximize the information visible on screen, scaling to 16px for better touch-targets on mobile.
- **Labels:** Small caps or slightly tracked-out uppercase styles should be used for table headers and form labels to differentiate them from user-entered data.

## Layout & Spacing
The system follows a **12-column fluid grid** with a fixed maximum width of 1440px for desktop to prevent line lengths from becoming unreadable.

- **Desktop:** 1.5rem (24px) gutters between columns. Content blocks use a 2rem margin for vertical separation.
- **Mobile:** Margins shrink to 1rem (16px). Grids typically collapse to 1 or 2 columns.
- **Spacing Logic:** Use an 8px base grid. All padding and margins must be multiples of 8 (8, 16, 24, 32, 48, 64) to maintain mathematical harmony across the layout.

## Elevation & Depth
Depth is conveyed through **Tonal Layering** and **Subtle Ambient Shadows**. 

The main background is a very light gray (`#F8F9FA`). Cards and containers sit on top of this background with a pure white fill (`#FFFFFF`). 

- **Surface Level 0:** Background.
- **Surface Level 1:** Cards and Modals. These use a very soft shadow: `0 4px 6px -1px rgba(0, 0, 0, 0.05), 0 2px 4px -1px rgba(0, 0, 0, 0.03)`.
- **Surface Level 2:** Active dropdowns and tooltips. These use a slightly deeper shadow to indicate they are floating above the card layer.

Borders are kept minimal, using a light 1px stroke (`#DEE2E6`) only when necessary to define boundaries within a white container.

## Shapes
In line with the "Soft" setting, the design system utilizes a 0.25rem (4px) base radius. This provides a professional edge that is approachable but remains structural.

- **Small elements (Buttons, Inputs):** 4px (`0.25rem`).
- **Large elements (Cards, Alerts):** 8px (`0.5rem`).
- **Extra Large (Modals):** 12px (`0.75rem`).

## Components

### Buttons
- **Primary:** `#4A6CF7` fill, white text. Semi-bold weight.
- **Success (Save):** `#2D7A54` fill. Used exclusively for final submission actions.
- **Secondary (Cancel/Neutral):** `#6C757D` fill or a light gray ghost style.
- **Padding:** 0.5rem top/bottom, 1.25rem left/right.

### Form Inputs
- Standard inputs have a 1px border (`#CED4DA`) that transitions to the Primary Blue on focus with a soft 3px focus-ring.
- Labels sit above the field in `label-md` style, using a medium-gray color.

### Data Tables
- **Header:** Light gray background (`#F1F3F5`), uppercase labels.
- **Rows:** Thin bottom border. Use zebra-striping (`#F8F9FA`) for tables exceeding 10 rows.
- **Cells:** Vertical padding of 0.75rem to ensure high readability.

### Alert Banners
- **Stock Alerts:** Use the tertiary yellow (`#FBBF24`) as a top-border accent or light fill. The header of the alert should be a saturated version of the color, while the body is a 10% opacity tint.

### Cards
- White background, 8px border radius, and Level 1 shadow.
- Card headers should have a subtle 1px bottom border to separate titles from content.