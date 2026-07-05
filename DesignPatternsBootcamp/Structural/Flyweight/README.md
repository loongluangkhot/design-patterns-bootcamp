# Flyweight — Instrument Reference Data

> **Week 2 · Day 3b · Structural**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Flyweight"`
> **This is a build-from-scratch kata:** only the legacy code is provided; you build the flyweight, its factory, and the order yourself.

## The scenario

An order-management system holds **millions** of orders. Each order references its instrument's
**static reference data** — currency, tick size, lot size, exchange. For a given symbol that data is
identical across every order and never changes.

## The challenge

Open [`Legacy/LegacyOrder.cs`](./Legacy/LegacyOrder.cs). Every order stores its **own copy** of the
reference data:

```csharp
(Symbol, Currency, TickSize, LotSize, Exchange) = symbol switch { "AAPL" => ("AAPL","USD",0.01m,100,"NASDAQ"), ... };
```

Book a million AAPL orders and you have a million identical copies of `USD / 0.01 / 100 / NASDAQ`.
The `ReferenceDataCopies` counter ticks once per order to make the waste visible.

| Smell | What it costs you |
|-------|-------------------|
| **Memory blow-up** | Immutable data that's the same for millions of objects is duplicated per object. |
| **Cache-unfriendly** | More bytes, more allocations, more GC pressure. |
| **No identity** | You can't ask "is this the AAPL instrument?" — there are a million of them. |

We want the shared, unchanging part to exist **once** and be **shared** by every order.

## The pattern: Flyweight

> **Intent:** Use sharing to support large numbers of fine-grained objects efficiently. — *Gang of Four*

The trick is to split an object's state:

- **Intrinsic** state — shared, immutable, context-free: the `Instrument` reference data. Stored
  **once** in a flyweight.
- **Extrinsic** state — unique per use, supplied by the context: an order's `Quantity` and `Price`.

A **Flyweight Factory** (`InstrumentFactory`) hands out shared flyweights, creating each distinct one
only once (interning). Millions of orders then hold a *reference* to a handful of shared instruments.

### UML

```mermaid
classDiagram
    class Instrument {
        <<flyweight · intrinsic · immutable>>
        +string Symbol
        +string Currency
        +decimal TickSize
    }
    class InstrumentFactory {
        -Dictionary~string,Instrument~ pool
        +Get(string) Instrument
        +DistinctInstrumentCount int
    }
    class Order {
        +int Quantity  «extrinsic»
        +decimal Price «extrinsic»
    }
    InstrumentFactory o--> "1 per symbol" Instrument : pools & shares
    Order --> Instrument : references shared flyweight
```

## Target API — what the (commented-out) tests expect

You must create these types so the tests compile and pass. **Property names read by the tests and the
`Order`/`Get` signatures are fixed; each type's internal shape is your design.**

| Type | Shape | Behaviour the tests pin down |
|------|-------|------------------------------|
| `Instrument` | exposes `Symbol`, `Currency` (string), `TickSize` (decimal), `LotSize` (int), `Exchange` (string) | the shared, **immutable** intrinsic reference data — one object per symbol |
| `InstrumentFactory` | `InstrumentFactory()`; `Instrument Get(string symbol)`; `int DistinctInstrumentCount { get; }` | **interns**: same symbol → the *very same* instance; count = number of distinct symbols pooled |
| `Order` | `Order(Instrument instrument, int quantity, decimal price)`; exposes `Instrument` | holds a *reference* to a shared flyweight plus its own extrinsic quantity/price |

**Provided (do not recreate):** `Legacy/LegacyOrder.cs` — the "before" code, and the source of the
reference-data values you must reproduce (`AAPL`/`MSFT` → `USD, 0.01, 100, NASDAQ`; `SAP` →
`EUR, 0.01, 1, XETRA`). Everything else here is yours to build.

## Your task (from scratch)

1. **Uncomment the tests.** In [`FlyweightTests.cs`](../../../DesignPatternsBootcamp.Tests/Structural/FlyweightTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~Flyweight"` **won't
   compile** — `Instrument`, `InstrumentFactory`, and `Order` don't exist yet. The build errors are
   your checklist.
2. **Create the flyweight.** Add a new `.cs` file; define an immutable `Instrument` carrying the
   intrinsic reference data. `The_flyweight_carries_the_reference_data` goes green once the factory can
   build one with the right `Currency` / `TickSize` / `Exchange`.
3. **Create the factory.** `InstrumentFactory.Get` **interns**: keep a pool keyed by symbol — if the
   symbol is already pooled, return that same instance; otherwise build one from a catalog holding the
   same values the legacy `switch` used, store it, and return it. `DistinctInstrumentCount` is the pool
   size.
4. **Create the order.** `Order` holds a shared `Instrument` plus its own `Quantity`/`Price` — the
   extrinsic state the flyweight doesn't carry.
5. **Green.** `dotnet test --filter "FullyQualifiedName~Flyweight"`.

These tests use `Assert.Same` (reference identity), not `Assert.Equal` — because the whole point is
that everyone gets the **same object**, not merely an equal one. `Many_orders_share_a_single_instrument_object`
builds three orders and proves they point at one flyweight.

### Stretch goals

- **Thread-safety.** Swap `_pool` for a `ConcurrentDictionary` and use `GetOrAdd` so concurrent
  callers still create each flyweight once. (Compare with the Singleton kata's thread-safety.)
- **Measure it.** Estimate the memory saved: a million orders × (bytes of duplicated reference data)
  versus a handful of shared `Instrument` objects.
- **Why immutable matters.** Explain what breaks if `Instrument` were mutable and two orders shared
  it — this is the same "shared mutable state" trap you saw in the Prototype kata, from the other side.

## When to use it

- **Use it** when you have a huge number of objects whose state is mostly shared and immutable, and
  memory (or allocation churn) is the bottleneck.
- **Real-world finance:** instrument/security master data, currency and calendar objects, symbol and
  venue metadata, glyphs in a rendering engine (the classic example).
- **Avoid it** when objects are few, when the "shared" state actually varies, or when the intrinsic
  state is mutable — the sharing would then leak changes between contexts.

## Done when

- [ ] You created `Instrument`, `InstrumentFactory`, and `Order` from scratch, and `Get` returns one
  shared, interned `Instrument` per symbol.
- [ ] `dotnet test --filter "FullyQualifiedName~Flyweight"` is fully green.
- [ ] You can name which fields here are intrinsic (shared) and which are extrinsic (per-order).
