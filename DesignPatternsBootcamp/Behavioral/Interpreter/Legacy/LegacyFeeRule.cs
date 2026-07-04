namespace DesignPatternsBootcamp.Behavioral.Interpreter.Legacy;

/// <summary>
/// THE "BEFORE" CODE — a single fee rule, hard-coded in a boolean expression.
///
/// Want "MSFT or AAPL over 100k"? Or a rule chosen from a config file, or edited by a compliance
/// user in a UI? You can't — the rule only exists as compiled code. Every new rule is new code, and
/// "the rule" can never be treated as data you build, store, or combine at runtime.
///
/// The Interpreter pattern represents each piece of the rule as an object, so a rule becomes a tree
/// you assemble on the fly and evaluate against a context.
/// </summary>
public sealed class LegacyFeeRule
{
    public bool Applies(TradeContext context) =>
        context.Symbol == "AAPL" && context.Notional >= 100_000m;
}
