# Template Method — Regulatory Reports

> **Week 4 · Day 2b · Behavioral**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~TemplateMethod"`
> **This is a build-from-scratch kata:** you create every type yourself. Nothing is stubbed.

## The scenario

We file several regulatory reports — MiFID, FINRA, more to come. They all share the same shape:
a **title**, then a **formatted line per trade**, then a **summary**. Only the details of each step
differ (CSV vs. pipe-delimited, EUR vs. USD, the footer wording).

## The challenge

Open [`Legacy/LegacyReportGenerator.cs`](./Legacy/LegacyReportGenerator.cs). Each report type
re-implements the whole skeleton:

```csharp
public string GenerateMifid(...) { sb.AppendLine("MiFID..."); foreach(...) sb.AppendLine(csvRow); sb.Append(summary); }
public string GenerateFinra(...) { sb.AppendLine("FINRA..."); foreach(...) sb.AppendLine(pipeRow); sb.Append(summary); }
```

| Smell | What it costs you |
|-------|-------------------|
| **Duplicated skeleton** | The title→rows→summary shape is copy-pasted per report. |
| **Drift** | Change the skeleton (add a timestamp, blank-line separators) and every copy must change — and won't. |
| **Buried variation** | What actually differs between reports is hard to see amid the boilerplate. |

We want the **skeleton written once**, with only the varying steps supplied per report.

## The pattern: Template Method

> **Intent:** Define the skeleton of an algorithm in an operation, deferring some steps to
> subclasses. Template Method lets subclasses redefine certain steps of an algorithm without changing
> the algorithm's structure. — *Gang of Four*

- The **Abstract Class** (`RegulatoryReport`) defines the **template method** `Generate()` — the
  fixed skeleton — and declares the steps it calls.
- **Primitive operations** (`Title`, `FormatTrade`) are abstract — subclasses must supply them.
- **Hooks** (`Summary`) have a default — subclasses may override.
- **Concrete Classes** (`MifidReport`, `FinraReport`) fill in the steps.

This is the **Hollywood Principle**: "Don't call us, we'll call you" — the base class calls down into
your steps, not the other way round.

### UML

```mermaid
classDiagram
    class RegulatoryReport {
        <<abstract>>
        +Generate() string
        #Title()* string
        #FormatTrade(Trade)* string
        #Summary() string
    }
    class MifidReport {
        #Title() string
        #FormatTrade(Trade) string
    }
    class FinraReport {
        #Title() string
        #FormatTrade(Trade) string
        #Summary() string
    }
    RegulatoryReport <|-- MifidReport
    RegulatoryReport <|-- FinraReport
    note for RegulatoryReport "Generate() is the template method:\ncalls Title(), FormatTrade(), Summary()"
```

> **Template Method vs. Strategy (Day 2a):** both let an algorithm vary. Template Method varies
> *steps of a fixed skeleton* via **inheritance** (subclass overrides). Strategy swaps the *whole*
> algorithm via **composition** (inject a different object). Inheritance vs. composition is the crux.

## Target API — what the (commented-out) tests expect

You must create these types so the tests compile and pass. **Names and constructor signatures are
fixed by the tests; the step names (`Title`/`FormatTrade`/`Summary`) and everything inside are your
design.**

| Type | Shape | Behaviour the tests pin down |
|------|-------|------------------------------|
| `RegulatoryReport` | abstract base: `RegulatoryReport(IEnumerable<Trade> trades)`; `string Generate()` (the template method); abstract step `Title()`; abstract step `FormatTrade(Trade)`; hook `Summary()` with a default | `Generate()` emits **title → one `FormatTrade` line per trade → summary**, in that fixed order; default `Summary()` = `"Total trades: {count}"` |
| `MifidReport` | `MifidReport(IEnumerable<Trade> trades)` | `Title` = `"MiFID II Transaction Report"`; row = `$"{Symbol},{Quantity},{Price:0.00},EUR"`; keeps the **default** `Summary` |
| `FinraReport` | `FinraReport(IEnumerable<Trade> trades)` | `Title` = `"FINRA OATS Report"`; row = `$"{Symbol}|{Quantity}|{Price:0.00}|USD"`; **overrides** `Summary` = `$"{count} trades reported to FINRA"` |

**Provided:** `Trade.cs` (the `record Trade(string Symbol, int Quantity, decimal Price)`) and
`Legacy/LegacyReportGenerator.cs` (the copy-pasted "before"). You build the abstract report and both
subclasses.

## Your task (from scratch)

1. **Uncomment the tests.** In [`TemplateMethodTests.cs`](../../../DesignPatternsBootcamp.Tests/Behavioral/TemplateMethodTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~TemplateMethod"` **won't
   compile** — that's step one done. Each missing-type error is a row of the table above.
2. **Write the abstract base + template method.** Add a new `.cs` file; define `RegulatoryReport` with
   `Generate()` fixed as title → rows → summary, calling the (abstract) `Title`/`FormatTrade` steps and
   the (default) `Summary` hook. Nothing greens yet — the base is abstract.
3. **Write the subclasses.** Add `MifidReport` and `FinraReport`, each supplying its steps:
   - **`MifidReport`** — `Title` = `"MiFID II Transaction Report"`; row `$"{Symbol},{Quantity},{Price:0.00},EUR"`; uses the default `Summary`.
   - **`FinraReport`** — `Title` = `"FINRA OATS Report"`; row `$"{Symbol}|{Quantity}|{Price:0.00}|USD"`; override `Summary` = `$"{count} trades reported to FINRA"`.
4. **Green.** `dotnet test --filter "FullyQualifiedName~TemplateMethod"`.

`Both_reports_follow_the_same_skeleton_order` proves the shape is enforced by the shared template
method — neither subclass can reorder it.

### Stretch goals

- **Add a report** (`EmirReport`) by writing only its steps. The skeleton — and any future change to
  it — is inherited for free.
- **Add a hook.** Give the base a `virtual string Separator() => ""` between rows; override it in one
  report. Hooks are the "optional" extension points.
- **Skeleton change, once.** Add a generation-timestamp line to `Generate()` (pass the time in — the
  bootcamp forbids `DateTime.Now` in library code) and watch every report gain it with no per-report
  edits.

## When to use it

- **Use it** when several variants share an invariant overall algorithm but differ in specific steps,
  and you want to enforce the shape while allowing controlled variation.
- **Real-world finance:** report/statement generation, ETL and batch pipelines (extract→transform→
  load), settlement/reconciliation runs, onboarding flows.
- **Avoid it** when composition fits better (prefer Strategy for swapping whole algorithms), or when
  deep inheritance hierarchies would make the flow hard to follow.

## Done when

- [ ] You created the abstract `RegulatoryReport` (with the `Generate()` template method) and both
      `MifidReport` and `FinraReport` subclasses from scratch.
- [ ] `dotnet test --filter "FullyQualifiedName~TemplateMethod"` is fully green.
- [ ] You can explain the difference between a primitive operation and a hook.
