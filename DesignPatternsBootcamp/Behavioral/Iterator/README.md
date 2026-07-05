# Iterator — Position Collection Traversal

> **Week 3 · Day 2b · Behavioral**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Iterator"`
> **This is a build-from-scratch kata:** you create every type yourself. Nothing is stubbed.

## The scenario

A trading book holds positions. Callers need to walk it — sometimes all positions, sometimes just
the longs — without caring (or knowing) whether it's backed by a list, an array, or a live feed.

## The challenge

Open [`Legacy/LegacyBook.cs`](./Legacy/LegacyBook.cs). The book just exposes its internal list:

```csharp
public List<Position> Positions { get; } = new();
```

| Smell | What it costs you |
|-------|-------------------|
| **Representation leaks** | Every caller now depends on it being a `List`. Change it and they all break. |
| **Duplicated traversal** | "Long positions only" gets re-written at every call site. |
| **No encapsulated strategies** | The collection can't offer its own meaningful ways to walk it. |

We want to hand callers a **way to traverse**, not the container itself.

## The pattern: Iterator

> **Intent:** Provide a way to access the elements of an aggregate object sequentially without
> exposing its underlying representation. — *Gang of Four*

- The **Aggregate** (`Book`) exposes iterators instead of its storage.
- The **Iterator** walks the elements one at a time.

**C# builds this pattern into the language.** `IEnumerable<T>`/`IEnumerator<T>` *are* the Aggregate
and Iterator roles, and the **`yield`** keyword generates the concrete iterator (a state machine) for
you. Implementing `IEnumerable<Position>` gives you `foreach` and all of LINQ for free — and the
internal list stays private.

### UML

```mermaid
classDiagram
    class IEnumerable~Position~ {
        <<interface>>
        +GetEnumerator() IEnumerator~Position~
    }
    class IEnumerator~Position~ {
        <<interface>>
        +MoveNext() bool
        +Current Position
    }
    class Book {
        -List~Position~ positions
        +Add(Position)
        +GetEnumerator() IEnumerator~Position~
        +LongPositions() IEnumerable~Position~
    }
    IEnumerable~Position~ <|.. Book
    IEnumerable~Position~ ..> IEnumerator~Position~ : creates
```

## Target API — what the (commented-out) tests expect

You must create this type so the tests compile and pass. **The name and members are fixed by the
tests; everything inside is your design.**

| Type | Shape | Behaviour the tests pin down |
|------|-------|------------------------------|
| `Book` | implements `IEnumerable<Position>`; `void Add(Position position)`; `IEnumerator<Position> GetEnumerator()`; `IEnumerable<Position> LongPositions()` | `Add` stores a position **privately**; `foreach` and LINQ (`Count()`, `Select`, …) walk every position in insertion order; `LongPositions()` yields only positions with a positive quantity. There is no public list to index into — callers can only traverse. |

**Provided (do not create):** `Position` (a `record`; positive quantity = long) in
[`Position.cs`](./Position.cs), plus `LegacyBook` under [`Legacy/`](./Legacy/).

## Your task (from scratch)

1. **Uncomment the tests.** In [`IteratorTests.cs`](../../../DesignPatternsBootcamp.Tests/Behavioral/IteratorTests.cs)
   delete the `/*` and `*/`. Now `dotnet test --filter "FullyQualifiedName~Iterator"` **won't
   compile** — that's step one done. Each "type or namespace could not be found" error is a type on
   your to-do list.
2. **Create the `Book`.** Add a new `.cs` file; give `Book` a *private* backing store and a public
   `Add`. Implement `IEnumerable<Position>` — a `GetEnumerator()` that `yield return`s each position
   unlocks `foreach (var p in book)` and all of LINQ (`book.Count()`, `book.Where(...)`, …) while the
   storage stays hidden.
3. **Add the long-only traversal.** `LongPositions()` — `yield return` only the positions with a
   positive quantity. `Callers_can_only_traverse_the_book_not_index_into_it` proves there's no public
   list to reach into — exactly the encapsulation we wanted.
4. **Green.** `dotnet test --filter "FullyQualifiedName~Iterator"`.

### Stretch goals

- **Write the iterator by hand.** Implement a `class BookEnumerator : IEnumerator<Position>` (with
  `MoveNext`, `Current`, `Reset`) *without* `yield`, to see the state machine the compiler was
  generating for you.
- **More strategies.** Add `ByLargestNotional()` or `Shorts()`. Each is a few lines and hides the
  traversal from callers.
- **External vs. internal iterators.** `foreach` is an *external* iterator (caller pulls). A method
  like `ForEach(Action<Position>)` is *internal* (collection pushes). When is each nicer?

## When to use it

- **Use it** to expose sequential access to a collection while hiding its structure, or to offer
  several meaningful ways to traverse it.
- **Real-world finance:** position/order books, paged result sets, streaming market data, tree/graph
  walks over portfolios. In C#, reach for `IEnumerable<T>` + `yield` by default.
- **Avoid it** (as a hand-rolled pattern) in C# when a plain `IEnumerable<T>`/LINQ already does the
  job — don't build a bespoke iterator class where `yield` suffices.

## Done when

- [ ] You created the `Book` (with `GetEnumerator` and `LongPositions`, both via `yield`) from scratch.
- [ ] `dotnet test --filter "FullyQualifiedName~Iterator"` is fully green.
- [ ] You can explain how `foreach` and LINQ started working from just `GetEnumerator`.
