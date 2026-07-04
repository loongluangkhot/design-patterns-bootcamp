using DesignPatternsBootcamp.Structural.Composite;
using DesignPatternsBootcamp.Structural.Composite.Legacy;

namespace DesignPatternsBootcamp.Tests.Structural;

public class CompositeTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Valuation must recurse by hand over two typed lists.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_valuation_recurses_by_hand()
    {
        var equities = new LegacyPortfolio();
        equities.Holdings.Add(new LegacyHolding { Symbol = "AAPL", Quantity = 10, Price = 150m });

        var total = new LegacyPortfolio();
        total.SubPortfolios.Add(equities);
        total.Holdings.Add(new LegacyHolding { Symbol = "BND", Quantity = 100, Price = 80m });

        Assert.Equal(9500m, LegacyValuation.TotalValue(total)); // 1500 + 8000
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until Position and Portfolio implement MarketValue().
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Position_value_is_quantity_times_price()
    {
        Assert.Equal(1500m, new Position("AAPL", 10, 150m).MarketValue());
    }

    [Fact]
    public void A_flat_portfolio_sums_its_positions()
    {
        var equities = new Portfolio("Equities")
            .Add(new Position("AAPL", 10, 150m))
            .Add(new Position("MSFT", 5, 400m));

        Assert.Equal(3500m, equities.MarketValue());
    }

    [Fact]
    public void Nested_portfolios_are_valued_by_the_very_same_call()
    {
        var equities = new Portfolio("Equities")
            .Add(new Position("AAPL", 10, 150m))
            .Add(new Position("MSFT", 5, 400m));
        var bonds = new Portfolio("Bonds")
            .Add(new Position("BND", 100, 80m));

        var total = new Portfolio("Total")
            .Add(equities)
            .Add(bonds);

        Assert.Equal(11500m, total.MarketValue()); // 3500 + 8000
    }

    [Fact]
    public void A_leaf_and_a_branch_are_interchangeable()
    {
        var bonds = new Portfolio("Bonds").Add(new Position("BND", 100, 80m));

        var mixed = new Portfolio("Mixed")
            .Add(new Position("AAPL", 10, 150m)) // a leaf...
            .Add(bonds);                          // ...and a branch, side by side

        Assert.Equal(9500m, mixed.MarketValue()); // 1500 + 8000
        Assert.IsAssignableFrom<IPortfolioComponent>(new Position("X", 1, 1m));
        Assert.IsAssignableFrom<IPortfolioComponent>(mixed);
    }
}
