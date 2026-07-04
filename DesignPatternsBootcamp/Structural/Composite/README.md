# Composite — Portfolio Hierarchy

> **Week 2 · Day 2a · Structural**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Composite"`

## The scenario

A book of business is a **tree**: a top-level portfolio holds sub-portfolios (by desk, strategy, or
asset class), which hold more sub-portfolios or individual **positions**. We constantly need to roll
a number *up* the tree — market value, exposure, P&L — treating "one position" and "a whole
sub-book" the same way.

## The challenge

Open [`Legacy/LegacyValuation.cs`](./Legacy/LegacyValuation.cs). Leaves (`LegacyHolding`) and
branches (`LegacyPortfolio`) are **different types in different lists**, so every roll-up hand-rolls
the recursion:

```csharp
foreach (var holding in portfolio.Holdings)      total += holding.Quantity * holding.Price;
foreach (var sub    in portfolio.SubPortfolios)  total += TotalValue(sub);
```

| Smell | What it costs you |
|-------|-------------------|
| **Client knows the shape** | Callers must understand the two-list structure to do anything. |
| **Duplicated traversal** | That two-loop recursion gets copy-pasted into valuation, counting, reporting… |
| **Rigid to new node types** | Add a cash balance or a derivative leg and *every* traversal must change. |

We want to treat a leaf and a branch as **the same kind of thing** and let the tree value itself.

## The pattern: Composite

> **Intent:** Compose objects into tree structures to represent part-whole hierarchies. Composite
> lets clients treat individual objects and compositions of objects uniformly. — *Gang of Four*

- The **Component** (`IPortfolioComponent`) is the common interface for both leaves and branches.
- The **Leaf** (`Position`) implements it directly.
- The **Composite** (`Portfolio`) implements it by **delegating to its children** and combining the
  results — and since a child may itself be a composite, the recursion is automatic.

### UML

```mermaid
classDiagram
    class IPortfolioComponent {
        <<interface>>
        +Name string
        +MarketValue() decimal
    }
    class Position {
        +int Quantity
        +decimal Price
        +MarketValue() decimal
    }
    class Portfolio {
        +Add(IPortfolioComponent) Portfolio
        +MarketValue() decimal
    }
    IPortfolioComponent <|.. Position
    IPortfolioComponent <|.. Portfolio
    Portfolio o--> "0..*" IPortfolioComponent : children
```

That self-referential arrow (`Portfolio` holds many `IPortfolioComponent`, which a `Portfolio`
*is*) is the whole pattern: branches contain components, and a branch is itself a component.

## Your task

Implement `MarketValue()` in [`PortfolioComponents.cs`](./PortfolioComponents.cs):

1. **`Position.MarketValue()`** — `Quantity × Price`.
2. **`Portfolio.MarketValue()`** — the **sum of `MarketValue()` over `Children`**. One line; it
   recurses through the whole tree because each child knows how to value itself.

```bash
dotnet test --filter "FullyQualifiedName~Composite"
```

`A_leaf_and_a_branch_are_interchangeable` puts a `Position` and a `Portfolio` in the same parent —
proof the client no longer cares which is which.

### Stretch goals

- **Add a second roll-up**, e.g. `int PositionCount()` on the interface. Notice it's the same
  shape — leaf returns 1, composite sums children — and no traversal code lives in the client.
- **Add a new leaf type** `CashBalance : IPortfolioComponent`. It drops into any portfolio and every
  existing roll-up keeps working — contrast with the legacy, where you'd edit every traversal.
- **Safety vs. transparency.** We kept `Add` only on `Portfolio` (the "safe" variant). Read about the
  "transparent" variant (Add on the Component) and note the trade-off (uniformity vs. leaf misuse).

## When to use it

- **Use it** for part-whole hierarchies you traverse uniformly: trees where "one" and "many" should
  look identical to callers.
- **Real-world finance:** portfolio/account hierarchies, org and cost-centre trees, nested fee
  schedules, bill-of-materials style instrument baskets, hierarchical limits.
- **Avoid it** when the structure isn't really a tree, or when leaves and composites have so little
  in common that a shared interface would be mostly empty (forcing meaningless methods onto leaves).

## Done when

- [ ] `Position.MarketValue()` and `Portfolio.MarketValue()` are implemented.
- [ ] `dotnet test --filter "FullyQualifiedName~Composite"` is fully green.
- [ ] You can explain why `Portfolio.MarketValue()` needs no special case for nested portfolios.
