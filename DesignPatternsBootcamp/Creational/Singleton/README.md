# Singleton — Shared Market-Data Connection

> **Week 1 · Day 4 · Creational**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Singleton"`

## The scenario

Opening a market-data feed is expensive: a socket, an authentication handshake, a set of symbol
subscriptions. The whole application should share **one** connection — one source of truth for live
prices — not open a fresh one in every module.

## The challenge

Open [`Legacy/LegacyMarketDataClient.cs`](./Legacy/LegacyMarketDataClient.cs). Its constructor is
**public**, so anyone can `new` one:

```csharp
var a = new LegacyMarketDataClient(); // module 1 opens a connection
var b = new LegacyMarketDataClient(); // module 2 opens ANOTHER one
// a.SessionId != b.SessionId — two authentications, two subscription sets, two "truths".
```

| Smell | What it costs you |
|-------|-------------------|
| **Duplicated expensive work** | Every caller pays the full connection cost again. |
| **Divergent state** | Two clients can drift — prices, sequence numbers, and config no longer agree. |
| **No single owner** | Nothing in the type system says "there should only be one." |

We want the type itself to **guarantee a single instance** and hand everyone the same one.

## The pattern: Singleton

> **Intent:** Ensure a class has only one instance, and provide a global point of access to it. — *GoF*

Two mechanics do the work:

1. A **private constructor** — no one outside the class can call `new`. (Provided for you.)
2. A **static accessor** (`Instance`) that creates the one instance on first use and returns it
   forever after.

The interesting part is doing step 2 **lazily** (don't pay the cost until first use) and
**thread-safely** (a burst of concurrent first-callers must still construct exactly one).

### UML

```mermaid
classDiagram
    class MarketDataConnection {
        -MarketDataConnection()
        -Lazy~MarketDataConnection~ lazyInstance$
        +Instance MarketDataConnection$
        +SessionId string
        +ConstructionCount int$
    }
    note for MarketDataConnection "Constructor is private.\nInstance (static) returns the one shared object.\n$ = static member"
    MarketDataConnection ..> MarketDataConnection : Instance returns the single self
```

### Idiomatic C#: `Lazy<T>`

`System.Lazy<T>` gives you lazy creation *and* thread-safety for free (its default mode is
`ExecutionAndPublication` — the factory runs at most once):

```csharp
private static readonly Lazy<MarketDataConnection> LazyInstance = new(() => new MarketDataConnection());
public static MarketDataConnection Instance => LazyInstance.Value;
```

Alternatives you should recognise:
- **Static field initializer** — `private static readonly MarketDataConnection _i = new();` The CLR
  guarantees a type's static initializer runs once, thread-safely. Simple, but eager-ish (runs when
  the type is first touched) and harder to make truly lazy.
- **Double-checked locking** — the manual version: check, `lock`, check again, create. Easy to get
  subtly wrong; prefer `Lazy<T>` unless you have a reason.

## Your task

Implement the `Instance` accessor in [`MarketDataConnection.cs`](./MarketDataConnection.cs) so it
returns a single, lazily-created, thread-safe instance (use `Lazy<T>`).

```bash
dotnet test --filter "FullyQualifiedName~Singleton"
```

The `Concurrent_first_access_still_constructs_exactly_one` test fires 256 threads at `Instance` at
once and asserts `ConstructionCount == 1` — that is the thread-safety requirement made concrete.

## ⚠️ Singleton is the most abused pattern — read this

Singleton is a **global variable in a nicer coat**. Before you reach for it, know the costs:

- **Hidden dependencies.** `MarketDataConnection.Instance` buried inside a method hides that the
  method needs a connection — the signature lies about its dependencies.
- **Hard to test.** Global mutable state leaks between tests; you can't easily substitute a fake.
  (Notice our own tests can only ever observe the *one* process-wide instance.)
- **Concurrency traps.** Shared mutable singletons invite race conditions.

**The modern alternative: dependency injection with a singleton *lifetime*.** Keep "there is one
instance" but stop hard-coding global access. Register it once:

```csharp
services.AddSingleton<MarketDataConnection>();     // container guarantees one instance
// ...and receive it explicitly, so the dependency is visible and fakeable:
public sealed class PriceService(MarketDataConnection feed) { /* ... */ }
```

Same guarantee ("one instance"), but the dependency is explicit and testable. Use the classic
Singleton for genuinely process-global, stateless-ish concerns; prefer DI singletons for almost
everything else in application code.

### Stretch goals

- **Application Configuration** *(the second Day 4 exercise)*: build a `TradingConfiguration`
  singleton (e.g. `BaseCurrency`, `MaxOrderNotional`) as a read-only single source of truth.
- **Refactor to DI.** Register `MarketDataConnection` as an `AddSingleton` in a
  `ServiceCollection`, inject it into a small `PriceService`, and write a test that swaps in a fake
  — something the static Singleton makes painful. Feel the difference.

## When to use it

- **Use it** (sparingly) for a genuinely single, process-wide resource where global access is
  acceptable and the object is effectively stateless or internally synchronised.
- **Real-world finance:** a single shared reference-data cache, a connection/session pool manager, a
  process-wide metrics registry.
- **Avoid it** for anything you'll want to test, swap, or scope per-request/per-tenant. Reach for a
  DI container's singleton lifetime instead.

## Done when

- [ ] `Instance` returns a lazily-created, thread-safe single instance.
- [ ] `dotnet test --filter "FullyQualifiedName~Singleton"` is fully green.
- [ ] You can articulate, in one sentence, why a DI singleton is usually preferable to this one.
