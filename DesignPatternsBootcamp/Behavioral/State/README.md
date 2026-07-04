# State — Order State Machine

> **Week 4 · Day 1b · Behavioral**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~State"`

## The scenario

An order moves through a lifecycle: **New → PartiallyFilled → Filled**, and **New/PartiallyFilled →
Cancelled**. What you're allowed to do — and where you go next — depends entirely on the current
state. A filled order can't be cancelled; a cancelled order can't be filled.

## The challenge

Open [`Legacy/LegacyOrder.cs`](./Legacy/LegacyOrder.cs). It tracks a `Status` string and every
operation re-checks it:

```csharp
public void Fill(int quantity) {
    if (Status is "Filled" or "Cancelled") throw ...;
    FilledQuantity += quantity;
    Status = FilledQuantity >= Quantity ? "Filled" : "PartiallyFilled";
}
public void Cancel() { if (Status is "Filled" or "Cancelled") throw ...; Status = "Cancelled"; }
```

| Smell | What it costs you |
|-------|-------------------|
| **Duplicated transition rules** | The same status checks are copied into every operation. |
| **Implicit machine** | The state machine exists only smeared across `if`s — nowhere to see it. |
| **Rigid to new states** | Add "PendingCancel" and you edit every method's guards. |

We want each state to be a **thing that owns its own behaviour and transitions**.

## The pattern: State

> **Intent:** Allow an object to alter its behavior when its internal state changes. The object will
> appear to change its class. — *Gang of Four*

- The **Context** (`OrderContext`) holds a reference to a current **State** and delegates operations
  to it.
- Each **Concrete State** (`NewState`, `PartiallyFilledState`, `FilledState`, `CancelledState`)
  implements `Fill`/`Cancel` for that state — including moving the context to the next state, or
  refusing the operation.

### State diagram

```mermaid
stateDiagram-v2
    [*] --> New
    New --> PartiallyFilled : Fill (partial)
    New --> Filled : Fill (full)
    New --> Cancelled : Cancel
    PartiallyFilled --> PartiallyFilled : Fill (partial)
    PartiallyFilled --> Filled : Fill (full)
    PartiallyFilled --> Cancelled : Cancel
    Filled --> [*]
    Cancelled --> [*]
```

```mermaid
classDiagram
    class OrderContext {
        +IOrderState State
        +Fill(int)
        +Cancel()
    }
    class IOrderState {
        <<interface>>
        +Name string
        +Fill(OrderContext, int)
        +Cancel(OrderContext)
    }
    OrderContext o--> IOrderState : current
    IOrderState <|.. NewState
    IOrderState <|.. PartiallyFilledState
    IOrderState <|.. FilledState
    IOrderState <|.. CancelledState
```

> **State vs. Strategy (Day 2a):** structurally twins — a context delegating to a swappable object.
> The difference is *who swaps it and why*: a **Strategy** is chosen by the client and usually stays
> put; a **State** changes itself as part of the object's lifecycle (the transitions live in the
> states).

## Your task

Implement `Fill`/`Cancel` for the four states in [`OrderStates.cs`](./OrderStates.cs) (the `Name`s
are provided):

1. **`NewState` / `PartiallyFilledState` `Fill`** — add to `context.FilledQuantity`, then set
   `context.State` to `FilledState` if fully filled, else `PartiallyFilledState`.
2. **`NewState` / `PartiallyFilledState` `Cancel`** — set `context.State` to `CancelledState`.
3. **`FilledState` / `CancelledState`** — both operations are illegal; throw
   `InvalidOperationException`.

```bash
dotnet test --filter "FullyQualifiedName~State"
```

### Stretch goals

- **Add a state.** Introduce `PendingCancel` (New/PartiallyFilled can request cancel, then it either
  confirms → Cancelled or resumes). Notice you add a class and touch only the states that transition
  into it — never a giant switch.
- **Entry/exit actions.** Give states an `OnEnter(context)` hook (e.g. stamp a timestamp). Where does
  the context call it?
- **Illegal-transition policy.** Compare throwing vs. returning a result vs. ignoring. Which suits an
  order-management system?

## When to use it

- **Use it** when an object's behavior depends on its state and it has many state-dependent
  operations, or when you have a state machine with non-trivial transitions.
- **Real-world finance:** order/trade lifecycles, settlement status, account/KYC status, workflow and
  approval state machines, connection/session state.
- **Avoid it** for two trivial states with one guard (an `enum` + `if` is fine), or when states share
  so much behavior that separate classes add more ceremony than clarity.

## Done when

- [ ] All four states implement `Fill` and `Cancel`.
- [ ] `dotnet test --filter "FullyQualifiedName~State"` is fully green.
- [ ] You can explain how adding a new state avoids editing the other states' code.
