# Review & Integration — The Trade Desk Capstone

> **Week 1 · Day 5 · Creational**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Integration"`
> **This is a build-from-scratch kata:** the capstone's whole source was deleted — you build the
> composing `TradeDesk` yourself. Nothing here is stubbed.
> **Prerequisites:** finish **Day 1b (Abstract Factory)** and **Day 2 (Builder)** first — this
> capstone *calls into both*, so it won't compile until their types exist.

## Goal

The individual katas each fixed one pain in isolation. Real systems combine patterns. Here you wire
two of them into a single flow: a **trade desk** that books a limit order and prices it for its
market. One method, two patterns, cleanly composed.

## Target API — what the (commented-out) tests expect

This is a capstone: you create the composing types below, and they *lean on* types you already built
in the prerequisite katas. **Names and signatures are fixed by the tests; the composition inside is
your design.**

| Type (you create) | Shape | Behaviour the tests pin down |
|-------------------|-------|------------------------------|
| `TradeDesk` | `TradeDesk(IMarketFactory market)`; `TradeTicket BookLimitBuy(string symbol, int quantity, decimal limitPrice, string account)` | builds a validated limit-buy order (Builder), prices commission on the notional and reads settlement from the desk's one `IMarketFactory` (Abstract Factory), and returns a ticket |
| `TradeTicket` | bundles `.Order` (the built `TradeOrder`), `.Commission` (`Money`), `.SettlementDays` (`int`), `.SettlementCurrency` (`string`) | the result of a booking; `.Order` exposes `Side`, `OrderType`, `Quantity`, `LimitPrice`, `Account` |

**Provided:** nothing but this README — the whole capstone is yours to write. The **dependency types
come from the prerequisite katas**: `IMarketFactory` / `UsMarketFactory` / `EuMarketFactory` / `Money`
(Abstract Factory, Day 1b) and `TradeOrderBuilder` / `TradeOrder` / `Side` / `OrderType` (Builder,
Day 2). If those aren't done, this won't compile.

## Your task (from scratch)

1. **Uncomment the tests.** In [`IntegrationTests.cs`](../../../DesignPatternsBootcamp.Tests/Creational/IntegrationTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~Integration"` **won't
   compile** — that's step one done. Missing-type errors point at both this capstone's types *and* any
   prerequisite type you haven't built yet.
2. **Create the `TradeTicket`.** Add a new `.cs` file holding the ticket fields the tests read
   (`Order`, `Commission`, `SettlementDays`, `SettlementCurrency`).
3. **Create the `TradeDesk` and `BookLimitBuy`.** Compose the two patterns:
   - **Builder** — assemble a validated, immutable `TradeOrder`:
     `TradeOrderBuilder.Create().Buy(quantity, symbol).Limit(limitPrice).ForAccount(account).Build()`.
   - **Abstract Factory** — pull the fee schedule and settlement policy from the desk's `IMarketFactory`.
   - Charge commission on the **notional** (`quantity × limitPrice`) and attach the settlement terms,
     then return a `TradeTicket`.
4. **Green.** `dotnet test --filter "FullyQualifiedName~Integration"`.

Notice what the composition buys you: the desk holds **one** `IMarketFactory`, so every ticket it
produces is priced *and* settled in the same market — the consistency guarantee from Day 1b now
protects an entire workflow, not just a single call.

### How the booking flows

```mermaid
sequenceDiagram
    participant Client
    participant Desk as TradeDesk
    participant B as TradeOrderBuilder
    participant M as IMarketFactory
    Client->>Desk: BookLimitBuy("AAPL", 100, 150, "ACC-1")
    Desk->>B: Buy(100,"AAPL").Limit(150).ForAccount("ACC-1").Build()
    B-->>Desk: TradeOrder (validated, immutable)
    Desk->>M: CreateFeeSchedule() / CreateSettlementPolicy()
    M-->>Desk: US fee + US settlement (one family)
    Desk->>Desk: commission = fees.Commission(100 × 150)
    Desk-->>Client: TradeTicket(order, $15.00, T+1, USD)
```

## The Week 1 map — where each creational pattern fits an order pipeline

| Pattern | Day | Role in a real desk | Answers the question |
|---------|-----|---------------------|----------------------|
| **Factory Method** | 1a | Create the right *payment/settlement processor* per rail | "Which **one** object do I need?" |
| **Abstract Factory** | 1b | Supply a consistent *market family* (fees + settlement) | "Which **matched set** do I need?" |
| **Builder** | 2 | Assemble a complex, validated *order* step by step | "How do I **construct** it safely?" |
| **Prototype** | 3 | Clone a *model basket* template per client | "How do I **copy** it independently?" |
| **Singleton** | 4 | Share one *market-data connection / config* | "How do I guarantee **one** of it?" |

All five answer *"how should this object come into existence?"* — that is what makes them
**creational**. This capstone uses two of them; the stretch goals fold in the rest.

## Stretch goals — grow the capstone into the full pipeline

- **Factory Method to pick the market.** Add `IMarketFactory MarketFor(string region)` so callers
  pass `"US"`/`"EU"` and the desk selects the factory — without the desk hard-coding a market.
- **Prototype the order source.** Overload the desk to book from a `ModelBasket` (Day 3): clone the
  template, then build orders for each cloned line. The master template stays pristine.
- **Singleton / DI config.** Give the desk a shared `TradingConfiguration` (Day 4) for the default
  account and a max-notional guardrail — then rewrite it as an injected dependency and note why that
  is nicer to test.
- **Anti-pattern watch.** If you find yourself reaching for `MarketDataConnection.Instance` deep
  inside `TradeDesk`, stop: that hides a dependency. Inject it instead. Composing patterns well is as
  much about what you *don't* couple as what you do.

## Done when

- [ ] You built `TradeTicket` and `TradeDesk.BookLimitBuy` from scratch, composing Builder + Abstract
      Factory (both prerequisite katas done first).
- [ ] `dotnet test --filter "FullyQualifiedName~Integration"` is green.
- [ ] You can explain, using this one method, why "creational patterns" are a family — and which
      question each of the five answers.

---

🎉 **That completes Week 1 — the five creational patterns.** Update your
[STATUS.md](../../../STATUS.md) as you turn each kata green, and tell the professor when you're ready
for Week 2 (Structural Patterns).
