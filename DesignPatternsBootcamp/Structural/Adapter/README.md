# Adapter — Third-Party Payment Gateways

> **Week 2 · Day 1a · Structural**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Adapter"`
> **This is a build-from-scratch kata:** you create every type yourself. Nothing is stubbed.

## The scenario

We take payments through several providers. Each ships an SDK we **cannot change**, and every one
has a different shape: **AcmePay** uses request/response objects and whole-dollar amounts; **GlobalPay**
wants integer **cents**, an ISO currency code, and returns its confirmation through an `out`
parameter. Our application just wants to "charge this account this much."

## The challenge

Open [`Legacy/LegacyCheckout.cs`](./Legacy/LegacyCheckout.cs). The checkout talks to each SDK
directly, behind a `switch`:

```csharp
case "acme":   var r = _acme.SubmitPayment(new AcmeRequest { ... }); return r.Ok ? r.AcmeTxnId : "DECLINED";
case "global": long cents = (long)Math.Round(amount * 100m); ... _global.Pay(cents, ..., out var code); ...
```

| Smell | What it costs you |
|-------|-------------------|
| **Leaky vendor knowledge** | Business code knows each SDK's private shapes and the `$ → ¢` conversion. |
| **No common type** | You can't hold "a payment provider" in a variable or a list — there's no shared interface. |
| **Duplication** | Every place that charges money repeats the translation. |
| **Open/Closed violation** | A new provider means editing this switch. |

We want one uniform interface, with each vendor's quirks **quarantined** behind it.

## The pattern: Adapter

> **Intent:** Convert the interface of a class into another interface clients expect. Adapter lets
> classes work together that couldn't otherwise because of incompatible interfaces. — *Gang of Four*

- The **Target** (`IPaymentGateway`) is the interface our app wants.
- The **Adaptee** (`AcmePayClient`, `GlobalPayApi`) is the incompatible third-party class.
- The **Adapter** (`AcmePayAdapter`, `GlobalPayAdapter`) implements the Target by **wrapping** an
  Adaptee and translating each call in and each result out.

### Participants

| Role | In this kata |
|------|--------------|
| Target | `IPaymentGateway` |
| Adaptee | `AcmePayClient`, `GlobalPayApi` (in `ThirdParty/`, "unchangeable") |
| Adapter | `AcmePayAdapter`, `GlobalPayAdapter` |
| Client | anything holding an `IPaymentGateway` |

### UML

```mermaid
classDiagram
    class IPaymentGateway {
        <<interface>>
        +Charge(decimal, string, string) GatewayResult
    }
    class AcmePayClient {
        +SubmitPayment(AcmeRequest) AcmeResponse
    }
    class GlobalPayApi {
        +Pay(long, string, string, out string) bool
    }
    class AcmePayAdapter
    class GlobalPayAdapter
    IPaymentGateway <|.. AcmePayAdapter
    IPaymentGateway <|.. GlobalPayAdapter
    AcmePayAdapter o--> AcmePayClient : wraps
    GlobalPayAdapter o--> GlobalPayApi : wraps
```

The adapter **has-a** adaptee (object adapter, via composition) and **is-a** target (implements the
interface). That composition is what lets it translate.

## Target API — what the (commented-out) tests expect

You must create these types so the tests compile and pass. **Names and constructor signatures are
fixed by the tests; everything inside is your design.**

| Type | Shape | Behaviour the tests pin down |
|------|-------|------------------------------|
| `IPaymentGateway` | interface: `GatewayResult Charge(decimal amount, string currency, string account)` | the target interface the whole app codes against |
| `GatewayResult` | carries `bool Approved`, `string Reference`, `decimal AmountCharged`, `string Currency` | the one uniform result shape every gateway returns |
| `AcmePayAdapter` | `AcmePayAdapter(AcmePayClient client)` — implements `IPaymentGateway` | wraps AcmePay: builds an `AcmeRequest`, submits it, maps `Ok → Approved`, `AcmeTxnId → Reference`, echoes the dollar amount/currency |
| `GlobalPayAdapter` | `GlobalPayAdapter(GlobalPayApi api)` — implements `IPaymentGateway` | wraps GlobalPay: converts **dollars → integer cents**, calls `Pay(..., out var code)`, maps `code → Reference` |

**Provided (do not edit):** the "unchangeable" third-party SDKs in [`ThirdPartySdks.cs`](./ThirdPartySdks.cs)
— `AcmeRequest`, `AcmeResponse`, `AcmePayClient`, `GlobalPayApi` (namespace `…Adapter.ThirdParty`) —
and the [`Legacy/`](./Legacy/) baseline. Everything else in the table above you build.

## Your task (from scratch)

1. **Uncomment the tests.** In [`AdapterTests.cs`](../../../DesignPatternsBootcamp.Tests/Structural/AdapterTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~Adapter"` **won't
   compile** — that's step one done. Each "type or namespace could not be found" is a type on your
   checklist. (The `Legacy_checkout…` test already passes; it exercises the provided baseline.)
2. **Create the target.** Add a new `.cs` file in this folder; define `IPaymentGateway` and the
   `GatewayResult` it returns. This is the shape your app wants — invented by you, owned by you.
3. **Create `AcmePayAdapter`.** Wrap an `AcmePayClient`; in `Charge`, build an `AcmeRequest`, call
   `SubmitPayment`, and map the `AcmeResponse` back onto a `GatewayResult` (`Ok → Approved`,
   `AcmeTxnId → Reference`). Get `Acme_adapter_exposes_the_sdk_as_an_IPaymentGateway` green.
4. **Create `GlobalPayAdapter`.** Wrap a `GlobalPayApi`; convert dollars to **integer cents**, call
   `Pay(..., out var code)`, and map the result. The
   `Global_adapter_converts_dollars_to_integer_cents` test proves `123.45 → 12345`.
5. **Green.** `dotnet test --filter "FullyQualifiedName~Adapter"`.

Notice the pay-off in `Both_providers_can_be_treated_uniformly…`: once wrapped, both live in a
`List<IPaymentGateway>` and the client stops caring which vendor is which.

### Stretch goals

- **Add a third provider** with yet another odd API (e.g. amounts as `string`, status as an int
  code) by writing **only** a new adapter — the client and the other adapters don't move.
- **Class vs. object adapter.** We used an *object* adapter (composition). C# can't do the classic
  *class* adapter (multiple inheritance), but explain when you'd prefer composition anyway.
- **Two-way adapter / decline path.** Make the SDKs sometimes decline and map that to
  `Approved = false`; add tests.

## When to use it

- **Use it** to integrate code you don't control, to unify several incompatible APIs behind one
  interface, or to keep a third-party type from leaking through your codebase.
- **Real-world finance:** payment/PSP gateways, market-data vendor feeds, broker/FIX connectors,
  KYC/AML provider APIs, legacy core-banking systems.
- **Avoid it** when you *can* change the source interface (just fix it), or when the "translation" is
  really new behaviour (that's a Decorator or a Facade, not an Adapter).

## Done when

- [ ] You created `IPaymentGateway`, `GatewayResult`, and both adapters from scratch.
- [ ] Each adapter translates arguments/results correctly (watch the `$ → ¢` conversion).
- [ ] `dotnet test --filter "FullyQualifiedName~Adapter"` is fully green.
- [ ] You can explain why the client can now hold providers in a `List<IPaymentGateway>`.
