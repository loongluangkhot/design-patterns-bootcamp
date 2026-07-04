# Command — Undo/Redo for the Order Blotter

> **Week 3 · Day 1b · Behavioral**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Command"`
> **This is a build-from-scratch kata:** you create every type yourself. Nothing is stubbed.

## The scenario

Traders edit a blotter all day: add an order, cancel one, amend a quantity. They fat-finger things.
The blotter needs **undo** and **redo** — ideally many levels deep.

## The challenge

Open [`Legacy/LegacyBlotter.cs`](./Legacy/LegacyBlotter.cs). Each action mutates state directly:

```csharp
public void CancelOrder(string id) => _orders.RemoveAll(o => o.Id == id);
```

It works, but the *actions themselves aren't objects* — so there's nothing to reverse, replay, queue,
or log.

| Smell | What it costs you |
|-------|-------------------|
| **Actions aren't first-class** | You can't hold "the cancel that just happened" to undo it. |
| **Undo is bespoke** | Multi-level undo means hand-stashing prior state in every method + a hand-rolled history. |
| **No decoupling** | Whatever triggers an action is welded to how the blotter performs it. |

We want each action to be a **thing** that knows how to do *and* undo itself.

## The pattern: Command

> **Intent:** Encapsulate a request as an object, thereby letting you parameterize clients with
> different requests, queue or log requests, and support undoable operations. — *Gang of Four*

- The **Command** (`ICommand`) declares `Execute` and `Undo`.
- Each **Concrete Command** (`AddOrderCommand`, …) bundles a **Receiver** (the `Blotter`) with the
  arguments for one action, and knows how to perform and reverse it.
- The **Invoker** (`BlotterHistory`) runs commands and keeps the undo/redo stacks — without knowing
  what any command does.

The key move for undo: a command **captures whatever it needs to reverse itself before it changes
anything** (the cancelled order; the previous quantity).

### Participants

| Role | In this kata |
|------|--------------|
| Command | `ICommand` |
| Concrete Command | `AddOrderCommand`, `CancelOrderCommand`, `AmendQuantityCommand` |
| Receiver | `Blotter` |
| Invoker | `BlotterHistory` |

### UML

```mermaid
classDiagram
    class ICommand {
        <<interface>>
        +Execute()
        +Undo()
    }
    class AddOrderCommand
    class CancelOrderCommand
    class AmendQuantityCommand
    class BlotterHistory {
        +Do(ICommand)
        +Undo()
        +Redo()
    }
    class Blotter {
        +Add(Order)
        +Remove(string)
        +Find(string) Order
    }
    ICommand <|.. AddOrderCommand
    ICommand <|.. CancelOrderCommand
    ICommand <|.. AmendQuantityCommand
    BlotterHistory o--> "history" ICommand
    AddOrderCommand --> Blotter : receiver
    CancelOrderCommand --> Blotter : receiver
    AmendQuantityCommand --> Blotter : receiver
```

## Target API — what the (commented-out) tests expect

You must create these types so the tests compile and pass. **Names and constructor signatures are
fixed by the tests; everything inside is your design.**

| Type | Shape | Behaviour the tests pin down |
|------|-------|------------------------------|
| `AddOrderCommand` | `AddOrderCommand(Blotter blotter, Order order)` — implements `ICommand` | `Execute` adds the order; `Undo` removes it again |
| `CancelOrderCommand` | `CancelOrderCommand(Blotter blotter, string id)` — implements `ICommand` | `Execute` captures the order then removes it; `Undo` restores it **intact** (same quantity) |
| `AmendQuantityCommand` | `AmendQuantityCommand(Blotter blotter, string id, int newQuantity)` — implements `ICommand` | `Execute` saves the old quantity then sets the new one; `Undo` restores the old quantity |
| `BlotterHistory` | `BlotterHistory()`, `Do(ICommand)`, `Undo()`, `Redo()` | the **invoker**: `Do` runs a command and records it; `Undo` reverses the last; `Redo` reapplies the last undone; undo walks multiple actions back in order |

**Provided (do not create):** `Order` (mutable quantity), the `Blotter` **receiver**
(`Add`/`Remove`/`Find`/`Count`), and the `ICommand` interface (`Execute`/`Undo`) — all in
[`Blotter.cs`](./Blotter.cs) — plus `LegacyBlotter` under [`Legacy/`](./Legacy/).

## Your task (from scratch)

1. **Uncomment the tests.** In [`CommandTests.cs`](../../../DesignPatternsBootcamp.Tests/Behavioral/CommandTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~Command"` **won't
   compile** — that's step one done. Each "type or namespace could not be found" error is a type on
   your to-do list.
2. **Create the three commands.** Add a new `.cs` file; write `AddOrderCommand`, `CancelOrderCommand`,
   and `AmendQuantityCommand`, each implementing `ICommand` over the provided `Blotter`. The key move:
   **capture whatever you need to reverse the action before you mutate** (the cancelled order; the
   previous quantity).
3. **Create the invoker.** Write `BlotterHistory` with `Do`, `Undo`, and `Redo` — two stacks behind
   the scenes, and a fresh `Do` must clear the redo stack. `Undo_walks_multiple_actions_back_in_order`
   pins the multi-level behaviour: because every action is a command on a stack, deep undo/redo falls
   out for free.
4. **Green.** `dotnet test --filter "FullyQualifiedName~Command"`.

### Stretch goals

- **Build your own invoker.** Re-implement `BlotterHistory` from scratch — the two stacks and why a
  fresh `Do` must clear the redo stack.
- **Macro command.** A `CompositeCommand` that holds a list of commands and Executes/Undoes them as
  one unit (undo in reverse order). Basis for "cancel this whole basket".
- **Command vs. Memento (Day 3b).** Command remembers *how to reverse an action*; Memento snapshots
  *state to roll back to*. When is each cheaper?

## When to use it

- **Use it** for undo/redo, queuing/scheduling work, logging/replaying actions, macro recording, or
  decoupling "what triggers an action" from "who performs it".
- **Real-world finance:** order-blotter edits, trade-amendment audit trails, batch/scheduled jobs,
  transactional "apply then maybe roll back" operations, wizard back/next.
- **Avoid it** when an action never needs to be reversed, queued, or logged — wrapping a one-off call
  in a command object is just ceremony.

## Done when

- [ ] You created the three commands and the `BlotterHistory` invoker from scratch.
- [ ] `dotnet test --filter "FullyQualifiedName~Command"` is fully green.
- [ ] You can explain why each command captures reversal state *before* mutating.
