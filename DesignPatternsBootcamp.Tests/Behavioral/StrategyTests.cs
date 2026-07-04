using DesignPatternsBootcamp.Behavioral.Strategy;
using DesignPatternsBootcamp.Behavioral.Strategy.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class StrategyTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. One switch, an overloaded "parameter".
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_calculator_switches_on_a_fee_type_string()
    {
        var calc = new LegacyFeeCalculator();

        Assert.Equal(5m, calc.Calculate("flat", 100_000m, 5m));
        Assert.Equal(100m, calc.Calculate("percentage", 100_000m, 0.001m));
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the three strategies implement Calculate.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Flat_fee_is_constant()
    {
        var strategy = new FlatFee(5m);

        Assert.Equal(5m, strategy.Calculate(100m));
        Assert.Equal(5m, strategy.Calculate(1_000_000m));
    }

    [Fact]
    public void Percentage_fee_scales_with_notional()
    {
        Assert.Equal(100m, new PercentageFee(0.001m).Calculate(100_000m));
    }

    [Fact]
    public void Tiered_fee_picks_the_right_band()
    {
        var strategy = new TieredFee();

        Assert.Equal(50m, strategy.Calculate(50_000m));    // 0.1%
        Assert.Equal(100m, strategy.Calculate(200_000m));  // 0.05%
    }

    [Fact]
    public void The_calculator_swaps_strategies_at_runtime()
    {
        var calc = new FeeCalculator(new FlatFee(5m));
        Assert.Equal(5m, calc.FeeFor(100_000m));

        calc.UseStrategy(new PercentageFee(0.001m));
        Assert.Equal(100m, calc.FeeFor(100_000m));
    }
}
