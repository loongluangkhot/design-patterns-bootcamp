# Observer — Order Status Notifications

> **Week 4 · Day 1a · Behavioral**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Observer"`
> **This is a build-from-scratch kata:** you create every type yourself. Nothing is stubbed.

## The scenario

When an order's status changes (New → PartiallyFilled → Filled → Cancelled), lots of things need to
react: an audit trail, a client push notification, a risk monitor, a P&L feed. That list grows and
changes, and the order shouldn't have to know about any of it.

## The challenge

Open [`Legacy/LegacyOrderBook.cs`](./Legacy/LegacyOrderBook.cs). The book hard-codes a call to each
interested party:

```csharp
AuditEntries.Add($"{orderId}:{status}"); // audit
ClientNotifications++;                    // notify client
// want a risk monitor too? edit this method.
```

| Smell | What it costs you |
|-------|-------------------|
| **Tight coupling** | The subject knows every subscriber and its API. |
| **Closed to extension** | A new interested party means editing the subject. |
| **No runtime flexibility** | Subscribers can't be added or removed while running. |

We want the subject to just **announce** "status changed" and let anyone listen.

## The pattern: Observer

> **Intent:** Define a one-to-many dependency between objects so that when one object changes state,
> all its dependents are notified and updated automatically. — *Gang of Four*

- The **Subject** (`OrderStatusPublisher`) keeps a list of observers and broadcasts changes.
- **Observers** (`IOrderObserver` → `AuditLog`, `ClientNotifier`) subscribe and react.
- The subject depends only on the `IOrderObserver` interface — never on concrete observers.

### UML

```mermaid
classDiagram
    class OrderStatusPublisher {
        -List~IOrderObserver~ observers
        +Subscribe(IOrderObserver)
        +Unsubscribe(IOrderObserver)
        +ChangeStatus(string, OrderStatus)
    }
    class IOrderObserver {
        <<interface>>
        +OnStatusChanged(string, OrderStatus)
    }
    class AuditLog
    class ClientNotifier
    IOrderObserver <|.. AuditLog
    IOrderObserver <|.. ClientNotifier
    OrderStatusPublisher o--> "0..*" IOrderObserver : notifies
```

> **C# note:** The idiomatic Observer in C# is the **`event`** keyword (and `IObservable<T>` for
> streams). This hand-rolled version shows the mechanics behind them — a subscriber list and a
> broadcast loop.

## Target API — what the (commented-out) tests expect

You must create these types so the tests compile and pass. **Names and constructor signatures are
fixed by the tests; everything inside is your design.**

| Type | Shape | Behaviour the tests pin down |
|------|-------|------------------------------|
| `IOrderObserver` | interface: `void OnStatusChanged(string orderId, OrderStatus status)` | the observer contract the publisher broadcasts to |
| `OrderStatusPublisher` | `void Subscribe(IOrderObserver)`, `void Unsubscribe(IOrderObserver)`, `void ChangeStatus(string orderId, OrderStatus status)` | the subject — keeps a subscriber list and notifies every one |
| `AuditLog` | `AuditLog()` : `IOrderObserver`; exposes `Entries` | records `"{orderId}:{status}"` (e.g. `"O1:Filled"`) per change |
| `ClientNotifier` | `ClientNotifier()` : `IOrderObserver`; exposes `NotificationsSent` (int), `LastStatus` (`OrderStatus`) | counts pushes and remembers the last status |

**Provided (don't recreate):** the `OrderStatus` enum in [`OrderStatus.cs`](./OrderStatus.cs) and the
`Legacy/` baseline. The observer interface, the publisher, and both concrete observers are yours.

## Your task (from scratch)

1. **Uncomment the tests.** In [`ObserverTests.cs`](../../../DesignPatternsBootcamp.Tests/Behavioral/ObserverTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~Observer"` **won't compile** —
   `IOrderObserver`, `OrderStatusPublisher`, `AuditLog` and `ClientNotifier` don't exist yet. Each build
   error names the next type to create.
2. **Define the observer contract.** Add a new `.cs` file; declare `IOrderObserver` with a single
   `OnStatusChanged(orderId, status)` method.
3. **Build the subject.** Give `OrderStatusPublisher` a private list of observers, `Subscribe` /
   `Unsubscribe` to add and remove one, and `ChangeStatus` that calls `OnStatusChanged` on **every**
   subscribed observer. The subject knows only the interface, never the concrete observers.
4. **Build the concrete observers.** `AuditLog` records `"{orderId}:{status}"` into its `Entries` for
   each change; `ClientNotifier` increments `NotificationsSent` and stores `LastStatus`. Get
   `Subscribed_observers_are_notified…` and `Unsubscribed_observers_stop_receiving_updates` green.
5. **Green.** `dotnet test --filter "FullyQualifiedName~Observer"`.
   `A_brand_new_observer_type_just_subscribes…` is the payoff: a new listener plugs in without the
   publisher changing at all.

### Stretch goals

- **Rewrite with `event`.** Replace the list + loop with a C# `event Action<string, OrderStatus>`.
  Note what you gain (language support, `+=`/`-=`) and lose (explicit control of the list).
- **Push vs. pull.** We *push* the new status to observers. Alternatively push just "something
  changed" and let observers *pull* details. When is each better?
- **Beware feedback loops & ordering.** What happens if an observer, while handling a change,
  triggers another change? Add a guard.

## When to use it

- **Use it** for event/notification systems, publish–subscribe, and keeping multiple views/consumers
  in sync with a changing subject.
- **Real-world finance:** order/execution status feeds, market-data subscriptions, position and P&L
  updates, alerting, UI data binding.
- **Avoid it** when there's exactly one dependent (just call it), or when hidden update cascades
  would make the flow hard to follow — sometimes an explicit call is clearer than a broadcast.

## Done when

- [ ] You created `IOrderObserver`, `OrderStatusPublisher`, `AuditLog` and `ClientNotifier` from scratch.
- [ ] `dotnet test --filter "FullyQualifiedName~Observer"` is fully green.
- [ ] You can add a new observer type without touching `OrderStatusPublisher`.
