# Factory Method — Payment Rails

> **Week 1 · Day 1a · Creational**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~FactoryMethod"`

## The scenario

Our fintech app moves money over several **rails**: card, ACH, and wire. Each rail has its own
fee schedule and its own confirmation format, but the surrounding workflow (validate the request,
run it, hand back a result) is the same for all of them.

## The challenge

Open [`Legacy/LegacyPaymentGateway.cs`](./Legacy/LegacyPaymentGateway.cs). Everything lives in one
method behind a `switch`:

```csharp
switch (method)
{
    case PaymentMethod.Card: /* fee maths + confirmation */ ...
    case PaymentMethod.Ach:  ...
    case PaymentMethod.Wire: ...
}
```

It works — the legacy baseline test passes — but the design fights you:

| Smell | What it costs you |
|-------|-------------------|
| **Open/Closed violation** | Adding *real-time payments* means editing this method — and risking the rails already in production. |
| **Single Responsibility violation** | Validation, fee maths, and rail behaviour are tangled in one place. |
| **Poor testability** | You cannot test "card fee logic" in isolation; you must go through the whole gateway. |
| **Shotgun surgery** | Changing how wires work forces a change to code shared by every other rail. |

We want to **add a rail by adding a class, never by editing existing code.**

## The pattern: Factory Method

> **Intent:** Define an interface for creating an object, but let subclasses decide which class to
> instantiate. Factory Method lets a class defer instantiation to subclasses. — *Gang of Four*

The trick is to split "the work that is always the same" from "the object that varies":

- The **Creator** (`PaymentService`) owns the shared workflow (`Pay`) and declares an abstract
  **factory method** (`CreateProcessor`).
- Each **Concrete Creator** (`CardPaymentService`, …) overrides the factory method to return its
  own **Concrete Product**.
- The **Product** (`IPaymentProcessor`) is the interface the shared workflow depends on, so it
  never needs to know which rail it is running.

### Participants

| Role | In this kata |
|------|--------------|
| Product | `IPaymentProcessor` |
| Concrete Product | `CardPaymentProcessor`, `AchPaymentProcessor`, `WirePaymentProcessor` |
| Creator | `PaymentService` (abstract; owns `Pay`) |
| Concrete Creator | `CardPaymentService`, `AchPaymentService`, `WirePaymentService` |

### UML

```mermaid
classDiagram
    class IPaymentProcessor {
        <<interface>>
        +Process(PaymentRequest) PaymentResult
    }
    class CardPaymentProcessor
    class AchPaymentProcessor
    class WirePaymentProcessor
    IPaymentProcessor <|.. CardPaymentProcessor
    IPaymentProcessor <|.. AchPaymentProcessor
    IPaymentProcessor <|.. WirePaymentProcessor

    class PaymentService {
        <<abstract>>
        +Pay(PaymentRequest) PaymentResult
        +CreateProcessor()* IPaymentProcessor
    }
    class CardPaymentService {
        +CreateProcessor() IPaymentProcessor
    }
    class AchPaymentService {
        +CreateProcessor() IPaymentProcessor
    }
    class WirePaymentService {
        +CreateProcessor() IPaymentProcessor
    }
    PaymentService <|-- CardPaymentService
    PaymentService <|-- AchPaymentService
    PaymentService <|-- WirePaymentService

    PaymentService ..> IPaymentProcessor : uses (via Pay)
    CardPaymentService ..> CardPaymentProcessor : creates
    AchPaymentService ..> AchPaymentProcessor : creates
    WirePaymentService ..> WirePaymentProcessor : creates
```

Read the arrows as: `Pay` (in the base) *uses* an `IPaymentProcessor`; each subclass *creates* the
concrete one. The base never learns which rail it is running.

## Your task

You are refactoring the legacy `switch` into the skeletons already stubbed out for you. The shared
`Pay` workflow and the class layout are done — you supply the rail-specific bodies.

1. **Implement the three Concrete Products** in [`Processors.cs`](./Processors.cs). Move each branch
   of the legacy `switch` into the matching `Process` method. Keep the fees and the
   `"CARD-…"/"ACH-…"/"WIRE-…"` confirmation prefixes identical.
2. **Implement the three factory methods** in [`PaymentService.cs`](./PaymentService.cs). Each
   `CreateProcessor()` override should return `new XPaymentProcessor()`.
3. **Run the tests** and watch them go green:
   ```bash
   dotnet test --filter "FullyQualifiedName~FactoryMethod"
   ```
4. **Prove the point.** Notice you never touched `PaymentService.Pay`, and validation was written
   exactly once. Adding a rail later is purely additive.

### Stretch goals

- Add a fourth rail, `RealTimePayments` (flat `$0.50`), by adding **only new classes** plus tests.
  Confirm you never open `PaymentService.cs`'s `Pay` method.
- The app still needs to pick a `PaymentService` from a `PaymentMethod` value. Add a tiny
  registry (`Dictionary<PaymentMethod, PaymentService>`) so callers select a rail without a
  `switch`. How is that registry different from — and complementary to — Factory Method?

## Factory Method vs. Simple Factory

A *Simple Factory* is one method with a `switch` that returns products — it centralises the smell
rather than removing it. Factory Method removes the `switch` entirely by using **polymorphism**:
the subclass you already chose *is* the decision. Use a Simple Factory when a single decision point
is genuinely fine; reach for Factory Method when a base class needs to run a shared workflow over a
product whose type varies.

## When to use it

- **Use it** when a class can't anticipate the concrete type it must create, or when you want the
  choice of type to be an extension point (new subclass), not an edit.
- **Real-world finance:** payment/settlement rail selection, market-data feed handlers per venue,
  report exporters per regulator (MiFID, FINRA), order-router creation per asset class.
- **Avoid it** when there is only ever one product, or when a simple `Func<>`/DI registration is
  clearer than a class hierarchy. Don't manufacture subclasses just to look clever.

## Done when

- [ ] `Processors.cs` — all three `Process` methods implemented.
- [ ] `PaymentService.cs` — all three `CreateProcessor` overrides implemented.
- [ ] `dotnet test --filter "FullyQualifiedName~FactoryMethod"` is fully green.
- [ ] You did **not** modify `PaymentService.Pay`.
