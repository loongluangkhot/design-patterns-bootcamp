# Proxy — Caching Market-Data Service

> **Week 2 · Day 4 · Structural**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Proxy"`

## The scenario

Fetching a price from our vendor is expensive — a network round-trip per lookup. But prices are
requested constantly, often for the same symbol many times in a row. We want to stop paying for the
same lookup twice, **without** changing the callers or the vendor client.

## The challenge

Open [`Legacy/LegacyPriceClient.cs`](./Legacy/LegacyPriceClient.cs). It calls the real service
directly:

```csharp
symbols.Select(service.GetPrice).ToArray(); // one backend hit per element, duplicates and all
```

Ask for `AAPL` three times → three round-trips (`RealPriceService.CallCount == 3`).

| Smell | What it costs you |
|-------|-------------------|
| **Repeated expensive work** | Identical lookups each pay full price. |
| **Nowhere to intervene** | Adding caching means editing every caller, or bloating the real service with concerns that aren't its job. |
| **Mixed responsibilities (the alternative)** | Baking a cache into `RealPriceService` mixes "fetch" with "remember". |

We want to insert caching **transparently**, behind the same interface.

## The pattern: Proxy

> **Intent:** Provide a surrogate or placeholder for another object to control access to it. — *Gang of Four*

- The **Subject** (`IPriceService`) is the shared interface.
- The **Real Subject** (`RealPriceService`) does the actual, expensive work.
- The **Proxy** (`CachingPriceProxy`) implements the same interface, **holds** the real subject, and
  controls access to it — here by caching. The client holds an `IPriceService` and never knows.

Common flavours of Proxy:

| Flavour | Controls access by… | Example |
|---------|---------------------|---------|
| **Caching / smart** | remembering results | *this kata* |
| **Virtual** | deferring creation until first use | a lazy-loaded statement/report |
| **Protection** | checking permissions first | trader-role guard on an admin service |
| **Remote** | standing in for an object elsewhere | an RPC/client stub |

### UML

```mermaid
classDiagram
    class IPriceService {
        <<interface>>
        +GetPrice(string) decimal
    }
    class RealPriceService {
        +GetPrice(string) decimal
        +CallCount int
    }
    class CachingPriceProxy {
        -IPriceService real
        -Dictionary~string,decimal~ cache
        +GetPrice(string) decimal
    }
    IPriceService <|.. RealPriceService
    IPriceService <|.. CachingPriceProxy
    CachingPriceProxy o--> IPriceService : delegates to real subject
```

> **Proxy vs. Decorator vs. Adapter:** all wrap an object behind an interface. Adapter *changes* the
> interface; Decorator *adds behaviour* meant to stack; Proxy *keeps the interface identical* and
> *controls access* (often invisibly, and usually a single wrapper, not a stack).

## Your task

Implement `GetPrice` in [`CachingPriceProxy.cs`](./CachingPriceProxy.cs):

1. On a cache **hit**, return the cached price and do **not** call the real service.
2. On a **miss**, call `_real.GetPrice(symbol)` once, store it, and return it.

```bash
dotnet test --filter "FullyQualifiedName~Proxy"
```

The tests assert on `RealPriceService.CallCount`, so they verify you actually *avoided* the backend,
not just returned the right number. `Proxy_is_a_drop_in_replacement…` even hands the proxy to the
unchanged legacy client — transparency in action.

### Stretch goals

- **Lazy-loading (virtual) proxy** *(the second Day 4 exercise)*: build a `LazyStatementProxy` that
  doesn't construct/load an expensive account statement until the first method call on it.
- **Protection proxy:** wrap the service so only a caller with a "market-data" entitlement may call
  `GetPrice`; throw otherwise. Same interface, access controlled.
- **Cache invalidation.** Prices go stale. Add a TTL so entries expire — and appreciate why "there
  are only two hard problems in computer science."

## When to use it

- **Use it** to control access to an object transparently: cache, lazy-load, guard, meter, or stand
  in for something remote — all behind the real object's own interface.
- **Real-world finance:** caching market-data/reference-data services, lazy-loaded reports and
  statements, permissioned access to sensitive services, client stubs for remote pricing/risk APIs.
- **Avoid it** when you don't need to control access (just use the object), or when a Decorator
  (stackable added behaviour) or Facade (simplified subsystem) is the better fit for your intent.

## Done when

- [ ] `GetPrice` caches: hits skip the backend, misses fetch once.
- [ ] `dotnet test --filter "FullyQualifiedName~Proxy"` is fully green.
- [ ] You can explain why the proxy sharing `IPriceService` is what makes it invisible to callers.
