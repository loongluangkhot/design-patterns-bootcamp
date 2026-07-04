using DesignPatternsBootcamp.Behavioral.ChainOfResponsibility;
using DesignPatternsBootcamp.Behavioral.ChainOfResponsibility.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class ChainOfResponsibilityTests
{
    private static OrderValidator BuildChain()
    {
        var head = new PositiveQuantityValidator();
        head.SetNext(new PriceBandValidator())
            .SetNext(new NotionalLimitValidator(1_000_000m))
            .SetNext(new RestrictedSymbolValidator("XYZ"));
        return head;
    }

    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. One method, every rule, all config passed in at once.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_validator_runs_all_rules_in_one_method()
    {
        var restricted = new HashSet<string> { "XYZ" };

        string? reason = new LegacyOrderValidator()
            .Validate(new Order("AAPL", 0, 150m), 1_000_000m, restricted);

        Assert.Equal("Quantity must be positive.", reason);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until each handler's Check is implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void A_clean_order_passes_the_whole_chain()
    {
        ValidationResult result = BuildChain().Validate(new Order("AAPL", 10, 150m));

        Assert.True(result.Approved);
        Assert.Null(result.Reason);
    }

    [Fact]
    public void The_quantity_rule_rejects_a_zero_quantity()
    {
        ValidationResult result = BuildChain().Validate(new Order("AAPL", 0, 150m));

        Assert.False(result.Approved);
        Assert.Equal("Quantity must be positive.", result.Reason);
    }

    [Fact]
    public void The_notional_rule_rejects_an_oversized_order()
    {
        ValidationResult result = BuildChain().Validate(new Order("AAPL", 100_000, 100m)); // 10,000,000

        Assert.Equal("Notional exceeds limit.", result.Reason);
    }

    [Fact]
    public void The_restricted_symbol_rule_rejects_a_blocked_name()
    {
        ValidationResult result = BuildChain().Validate(new Order("XYZ", 10, 150m));

        Assert.Equal("Symbol XYZ is restricted.", result.Reason);
    }

    [Fact]
    public void The_first_failing_handler_short_circuits_the_chain()
    {
        // Zero quantity AND a restricted symbol → we get the FIRST failure and the chain stops.
        ValidationResult result = BuildChain().Validate(new Order("XYZ", 0, 150m));

        Assert.Equal("Quantity must be positive.", result.Reason);
    }
}
