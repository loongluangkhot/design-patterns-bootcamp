using DesignPatternsBootcamp.Behavioral.Interpreter;
using DesignPatternsBootcamp.Behavioral.Interpreter.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class InterpreterTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. The rule exists only as compiled code.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_rule_is_hard_coded_in_a_boolean_expression()
    {
        var rule = new LegacyFeeRule();

        Assert.True(rule.Applies(new TradeContext("AAPL", 200_000m)));
        Assert.False(rule.Applies(new TradeContext("AAPL", 50_000m)));
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until each expression's Interpret is implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Symbol_terminal_matches_the_symbol()
    {
        Assert.True(new SymbolIs("AAPL").Interpret(new TradeContext("AAPL", 1m)));
        Assert.False(new SymbolIs("AAPL").Interpret(new TradeContext("MSFT", 1m)));
    }

    [Fact]
    public void Notional_terminal_matches_at_or_above_the_threshold()
    {
        Assert.True(new NotionalAtLeast(100_000m).Interpret(new TradeContext("X", 100_000m)));
        Assert.False(new NotionalAtLeast(100_000m).Interpret(new TradeContext("X", 99_999m)));
    }

    [Fact]
    public void And_requires_both_sides()
    {
        IRuleExpression rule = new And(new SymbolIs("AAPL"), new NotionalAtLeast(100_000m));

        Assert.True(rule.Interpret(new TradeContext("AAPL", 200_000m)));
        Assert.False(rule.Interpret(new TradeContext("AAPL", 50_000m)));
        Assert.False(rule.Interpret(new TradeContext("MSFT", 200_000m)));
    }

    [Fact]
    public void Or_requires_either_side()
    {
        IRuleExpression rule = new Or(new SymbolIs("AAPL"), new SymbolIs("MSFT"));

        Assert.True(rule.Interpret(new TradeContext("MSFT", 1m)));
        Assert.False(rule.Interpret(new TradeContext("GOOG", 1m)));
    }

    [Fact]
    public void Not_inverts_its_inner_expression()
    {
        IRuleExpression rule = new Not(new SymbolIs("AAPL"));

        Assert.True(rule.Interpret(new TradeContext("MSFT", 1m)));
        Assert.False(rule.Interpret(new TradeContext("AAPL", 1m)));
    }

    [Fact]
    public void Expressions_compose_into_an_arbitrary_tree()
    {
        // (AAPL OR MSFT) AND notional >= 100k — assembled from objects, not written as code.
        IRuleExpression rule = new And(
            new Or(new SymbolIs("AAPL"), new SymbolIs("MSFT")),
            new NotionalAtLeast(100_000m));

        Assert.True(rule.Interpret(new TradeContext("MSFT", 150_000m)));
        Assert.False(rule.Interpret(new TradeContext("MSFT", 50_000m)));
        Assert.False(rule.Interpret(new TradeContext("GOOG", 150_000m)));
    }
}
