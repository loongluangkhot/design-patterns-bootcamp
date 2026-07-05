// ============================================================================
//  STEP 1 OF THIS KATA — UNCOMMENT THESE TESTS.
//  They ship commented out so the rest of the test project still builds before
//  you start this kata. To begin: delete the "/*" on the line below AND the
//  "*/" on the very last line, then run this kata's tests (its README has the
//  filter + the target API). It will NOT compile at first — that is expected:
//  each "type or namespace ... could not be found" error is a type you must
//  create yourself. Build until it compiles, then turns green.
// ============================================================================
/*
using DesignPatternsBootcamp.Behavioral.Visitor;
using DesignPatternsBootcamp.Behavioral.Visitor.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class VisitorTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Every analytic re-switches on the instrument type.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_analytics_switches_on_type_in_every_operation()
    {
        var analytics = new LegacyAnalytics();

        Assert.Equal(1500m, analytics.MarketValue(new Equity("AAPL", 10, 150m)));
        Assert.Equal(1000m, analytics.MarketValue(new Bond("US-T", 1000m, 0.05m)));
        Assert.Equal(500m, analytics.MarketValue(new Option("AAPL", 1, 5m))); // 1 × 5 × 100
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the two visitors implement their three Visit methods.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void The_market_value_visitor_values_each_instrument_type()
    {
        var mv = new MarketValueVisitor();

        Assert.Equal(1500m, new Equity("AAPL", 10, 150m).Accept(mv));
        Assert.Equal(1000m, new Bond("US-T", 1000m, 0.05m).Accept(mv));
        Assert.Equal(500m, new Option("AAPL", 1, 5m).Accept(mv));
    }

    [Fact]
    public void The_tax_visitor_applies_a_type_specific_rate()
    {
        var tax = new TaxVisitor();

        Assert.Equal(225m, new Equity("AAPL", 10, 150m).Accept(tax));    // 1500 × 0.15
        Assert.Equal(12.5m, new Bond("US-T", 1000m, 0.05m).Accept(tax)); // 1000 × 0.05 × 0.25
        Assert.Equal(100m, new Option("AAPL", 1, 5m).Accept(tax));       // 500 × 0.20
    }

    [Fact]
    public void A_mixed_portfolio_is_valued_by_a_single_visitor()
    {
        var instruments = new List<IInstrument>
        {
            new Equity("AAPL", 10, 150m),
            new Bond("US-T", 1000m, 0.05m),
            new Option("AAPL", 1, 5m),
        };

        var mv = new MarketValueVisitor();

        Assert.Equal(3000m, instruments.Sum(i => i.Accept(mv))); // 1500 + 1000 + 500
    }

    [Fact]
    public void Adding_an_operation_is_a_new_visitor_not_a_change_to_the_instruments()
    {
        // TaxVisitor is a brand-new operation over the SAME instrument classes — none of them changed.
        var equity = new Equity("AAPL", 10, 150m);

        Assert.Equal(1500m, equity.Accept(new MarketValueVisitor()));
        Assert.Equal(225m, equity.Accept(new TaxVisitor()));
    }
}
*/
