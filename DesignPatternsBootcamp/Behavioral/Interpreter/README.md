# Interpreter — Fee Rule Language

> **Week 3 · Day 2a · Behavioral**
> Run just this kata: `dotnet test --filter "FullyQualifiedName~Interpreter"`

## The scenario

Compliance and desk heads keep inventing fee/eligibility rules: "AAPL over $100k", "AAPL or MSFT",
"anything that's *not* a restricted name". They want to define and change these rules without a code
release — ideally by combining simple building blocks.

## The challenge

Open [`Legacy/LegacyFeeRule.cs`](./Legacy/LegacyFeeRule.cs). The rule lives entirely in a compiled
boolean expression:

```csharp
public bool Applies(TradeContext context) => context.Symbol == "AAPL" && context.Notional >= 100_000m;
```

| Smell | What it costs you |
|-------|-------------------|
| **Rules are code, not data** | You can't store, load, edit, or combine a rule at runtime. |
| **Every rule is a new method** | Each variation needs a code change and a release. |
| **No composition** | There's no way to say "this rule OR that one" from existing pieces. |

We want a rule to be a **value we can build from parts** and evaluate.

## The pattern: Interpreter

> **Intent:** Given a language, define a representation for its grammar along with an interpreter
> that uses the representation to interpret sentences in the language. — *Gang of Four*

Each grammar rule becomes a class; a "sentence" becomes a **tree of these objects** (an AST):

- The **Abstract Expression** (`IRuleExpression`) declares `Interpret(context)`.
- **Terminal Expressions** (`SymbolIs`, `NotionalAtLeast`) are the leaves — they read the context.
- **Nonterminal Expressions** (`And`, `Or`, `Not`) combine sub-expressions, delegating to them.

Interpreting the tree is a recursive walk: each node interprets its children and combines the results.

### UML

```mermaid
classDiagram
    class IRuleExpression {
        <<interface>>
        +Interpret(TradeContext) bool
    }
    class SymbolIs
    class NotionalAtLeast
    class And
    class Or
    class Not
    IRuleExpression <|.. SymbolIs
    IRuleExpression <|.. NotionalAtLeast
    IRuleExpression <|.. And
    IRuleExpression <|.. Or
    IRuleExpression <|.. Not
    And o--> "2" IRuleExpression
    Or o--> "2" IRuleExpression
    Not o--> "1" IRuleExpression
```

`(AAPL OR MSFT) AND notional≥100k` as a tree:

```mermaid
flowchart TD
    A["And"] --> O["Or"]
    A --> N["NotionalAtLeast 100k"]
    O --> S1["SymbolIs AAPL"]
    O --> S2["SymbolIs MSFT"]
```

## Your task

Implement `Interpret` on all five expressions in [`RuleExpressions.cs`](./RuleExpressions.cs):

1. `SymbolIs` → `context.Symbol == _symbol`
2. `NotionalAtLeast` → `context.Notional >= _threshold`
3. `And` → `_left.Interpret(context) && _right.Interpret(context)`
4. `Or` → `_left.Interpret(context) || _right.Interpret(context)`
5. `Not` → `!_inner.Interpret(context)`

```bash
dotnet test --filter "FullyQualifiedName~Interpreter"
```

`Expressions_compose_into_an_arbitrary_tree` builds a nested rule from objects — the same rule the
legacy hard-coded, plus an `OR` it couldn't express — with no new "rule code".

### Stretch goals

- **Add operators.** `NotionalBelow`, `SymbolIn(params string[])`. Each is one small class; existing
  rules keep working.
- **Parsing is a separate job.** Interpreter defines the *tree + evaluation*; turning
  `"AAPL AND notional>=100000"` text into that tree is a *parser*. Write a tiny parser as a stretch
  and note how the two concerns divide.
- **Interpreter vs. Composite (Week 2).** Both are trees of a common interface. What makes this
  Interpreter (a grammar you *evaluate*) rather than plain Composite (a structure you *traverse*)?

## When to use it

- **Use it** for small, stable grammars/DSLs you evaluate often: business-rule engines, search/filter
  expressions, feature-flag conditions, simple query languages.
- **Real-world finance:** eligibility/fee rules, alert and surveillance conditions, screening filters,
  routing rules.
- **Avoid it** for large or fast-changing grammars — the class-per-rule explosion gets heavy; reach
  for a real parser generator or an existing expression library instead.

## Done when

- [ ] All five `Interpret` methods are implemented.
- [ ] `dotnet test --filter "FullyQualifiedName~Interpreter"` is fully green.
- [ ] You can build a brand-new rule at runtime without adding any code.
