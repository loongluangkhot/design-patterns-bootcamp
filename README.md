# Design Patterns Bootcamp

A hands-on workbook for the 23 classic (Gang of Four) design patterns, taught through **fintech**
scenarios in C#. Each pattern is a **build-from-scratch kata**: you're given working-but-rigid
"legacy" code and a test suite, and you build the pattern yourself — the interfaces, the classes,
the structure, the logic — until the tests pass. Nothing is stubbed.

## Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download) — check with `dotnet --version`
- Comfort with C# and object-oriented programming: classes, interfaces, inheritance, polymorphism

## Layout

```
DesignPatternsBootcamp.sln
├─ DesignPatternsBootcamp/               ← the code you edit (a class library)
│  ├─ Creational/                        Week 1
│  │  ├─ FactoryMethod/                  ← every pattern folder has the same shape:
│  │  │  ├─ README.md                    ←   the lesson: challenge, pattern, UML, target API
│  │  │  ├─ Legacy/                      ←   the "before" code that smells (given)
│  │  │  └─ *.cs                         ←   data types are given; you create the pattern classes
│  │  ├─ AbstractFactory/  Builder/  Prototype/  Singleton/
│  │  └─ Integration/                    ← end-of-week capstone (combines the week's patterns)
│  ├─ Structural/                        Week 2
│  │  ├─ Adapter/  Bridge/  Composite/  Decorator/  Facade/  Flyweight/  Proxy/
│  │  └─ Integration/                    ← capstone
│  └─ Behavioral/                        Weeks 3 & 4
│     ├─ ChainOfResponsibility/  Command/  Interpreter/  Iterator/  Mediator/  Memento/
│     ├─ Observer/  State/  Strategy/  TemplateMethod/  Visitor/
│     ├─ Integration/                    ← Week 3 capstone
│     └─ FinalProject/                   ← Week 4 capstone (the finale)
└─ DesignPatternsBootcamp.Tests/         ← the tests that grade your work
   └─ Creational/  Structural/  Behavioral/
```

All 23 patterns are here. Work through them in the order laid out in [STATUS.md](./STATUS.md); each
week ends with a capstone (`Integration`/`FinalProject`) that composes several patterns together.

## The workflow for every pattern

1. **Read the folder's `README.md`.** It explains the business challenge, why the legacy code is
   painful, the pattern that fixes it, a UML diagram, and the **Target API** you must build.
2. **Read the `Legacy/` code.** A working implementation with a real design flaw (a growing
   `switch`, a telescoping constructor, a shared mutable template, …) — the thing you'll replace.
3. **Uncomment that pattern's test file.** Each kata's tests ship commented out (so the whole
   project builds before you start). Deleting the `/* … */` is step one — now the project **won't
   compile**, and the "type `X` could not be found" errors are your to-do list.
4. **Build the pattern from scratch.** Create the interfaces, classes, and logic the tests expect
   (the README's *Target API* gives you the names and signatures; the numbers come from the tests).
   Get it compiling, then green.
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

> **Tip:** A freshly-cloned solution builds green with **zero** tests running, because every kata's
> tests start commented out. When you uncomment a kata to begin it, that code will *stop compiling*
> until you've created the types it references — that's expected, and the compiler errors are your
> checklist. Only the kata you're actively working on needs to compile.

## Tracking progress

Work through the patterns in the order laid out in **[STATUS.md](./STATUS.md)**, and flip each item
from ⏳ to ✅ as you finish it. A pattern is done when its test class is fully **GREEN**.

## A note on realism

The scenarios are deliberately simplified fintech models — enough to make the design tension real
without drowning you in domain detail. They are teaching aids, not production financial code.
