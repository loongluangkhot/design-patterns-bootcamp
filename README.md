# Design Patterns Bootcamp

A hands-on workbook for the 23 classic (Gang of Four) design patterns, taught through **fintech**
scenarios in C#. Each pattern is a small **refactoring kata**: you start from working-but-rigid
code, feel the pain it causes, then refactor it into the pattern until the tests pass.

## Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download) — check with `dotnet --version`
- Comfort with C# and object-oriented programming: classes, interfaces, inheritance, polymorphism

## Layout

```
DesignPatternsBootcamp.sln
├─ DesignPatternsBootcamp/            ← the code you edit (a class library)
│  └─ Creational/
│     ├─ FactoryMethod/               ← one folder per pattern
│     │  ├─ README.md                 ← the lesson: challenge, pattern, UML, steps
│     │  ├─ Legacy/                   ← the "before" code that smells
│     │  └─ *.cs                      ← the skeletons you fill in
│     ├─ AbstractFactory/
│     ├─ Builder/
│     ├─ Prototype/
│     ├─ Singleton/
│     └─ Integration/                 ← end-of-week capstone
└─ DesignPatternsBootcamp.Tests/      ← the tests that grade your work
   └─ Creational/
```

The `Structural/` and `Behavioral/` folders arrive in later weeks — see [STATUS.md](./STATUS.md) for
the full roadmap.

## The workflow for every pattern

1. **Read the folder's `README.md`.** It explains the business challenge, why the legacy code is
   painful, the pattern that fixes it, a UML diagram, and step-by-step instructions.
2. **Read the `Legacy/` code.** A working implementation with a real design flaw (a growing
   `switch`, a telescoping constructor, a shared mutable template, …).
3. **Run the tests and watch them fail (RED).** Failures are expected — the skeletons throw
   `NotImplementedException` on purpose.
4. **Refactor.** Move logic out of the legacy code into the pattern skeletons, following the steps.
5. **Run the tests until they pass (GREEN).**

## Running the tests

From the repo root:

```bash
# Run every exercise
dotnet test

# Run just one pattern while you work on it (fast feedback loop)
dotnet test --filter "FullyQualifiedName~FactoryMethod"
dotnet test --filter "FullyQualifiedName~Builder"
```

> **Tip:** `dotnet build` compiles everything even while skeletons are unimplemented
> (`NotImplementedException` is a *runtime* error, not a compile error), so the solution always
> builds. A failing test means "not implemented yet," not "broken project."

## Tracking progress

Work through the patterns in the order laid out in **[STATUS.md](./STATUS.md)**, and flip each item
from ⏳ to ✅ as you finish it. A pattern is done when its test class is fully **GREEN**.

## A note on realism

The scenarios are deliberately simplified fintech models — enough to make the design tension real
without drowning you in domain detail. They are teaching aids, not production financial code.
