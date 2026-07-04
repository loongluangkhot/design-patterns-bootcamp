// ============================================================================
//  STEP 1 OF THIS KATA — UNCOMMENT THESE TESTS.
//
//  They ship commented out so the rest of the test project still builds before
//  you start. To begin this kata: delete the "/*" on the line just below AND
//  the "*/" on the very last line of this file, then run:
//
//      dotnet test --filter "FullyQualifiedName~Decorator"
//
//  It will FAIL TO BUILD at first — that is expected. Each compiler error
//  ("the type or namespace 'BaseTrade' could not be found", …) is an item on
//  your to-do list: a type you must create yourself. See this kata's README.md
//  for the target API. Keep building until it compiles, then turns green.
// ============================================================================
/* 
using DesignPatternsBootcamp.Structural.Decorator;
using DesignPatternsBootcamp.Structural.Decorator.Legacy;

namespace DesignPatternsBootcamp.Tests.Structural;

public class DecoratorTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — passes as soon as you uncomment (LegacyTradePricer is provided).
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_pricer_applies_every_charge_in_one_hard_coded_order()
    {
        decimal total = new LegacyTradePricer().Price(
            10_000m, commission: true, commissionRate: 0.001m,
            exchangeFee: true, exchangeAmount: 5m, tax: true, taxRate: 0.10m);

        Assert.Equal(11016.5m, total); // 10000 →×1.001=10010 →+5=10015 →×1.10=11016.5
    }

    // -----------------------------------------------------------------------------------------
    //  Your build — RED (and, at first, won't compile) until you create the types below.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Base_trade_total_is_just_the_notional()
    {
        Assert.Equal(10_000m, new BaseTrade("AAPL", 10_000m).Total());
    }

    [Fact]
    public void Commission_adds_a_percentage_of_what_it_wraps()
    {
        var trade = new CommissionDecorator(new BaseTrade("AAPL", 10_000m), 0.001m);

        Assert.Equal(10_010m, trade.Total());
    }

    [Fact]
    public void Charges_stack_in_the_order_you_wrap_them()
    {
        IPricedTrade trade =
            new TaxDecorator(
                new ExchangeFeeDecorator(
                    new CommissionDecorator(new BaseTrade("AAPL", 10_000m), 0.001m),
                    5m),
                0.10m);

        Assert.Equal(11016.5m, trade.Total()); // same numbers as the legacy full stack
    }

    [Fact]
    public void Description_reads_as_the_layers_you_applied()
    {
        IPricedTrade trade =
            new TaxDecorator(
                new ExchangeFeeDecorator(
                    new CommissionDecorator(new BaseTrade("AAPL", 10_000m), 0.001m),
                    5m),
                0.10m);

        Assert.Equal("AAPL + commission + exchange fee + tax", trade.Description);
    }

    [Fact]
    public void Order_matters_when_charges_are_not_all_the_same_kind()
    {
        // Is the exchange fee itself taxable? The composition order decides.
        var taxThenFee = new ExchangeFeeDecorator(new TaxDecorator(new BaseTrade("AAPL", 10_000m), 0.10m), 5m);
        var feeThenTax = new TaxDecorator(new ExchangeFeeDecorator(new BaseTrade("AAPL", 10_000m), 5m), 0.10m);

        Assert.Equal(11005m, taxThenFee.Total());   // tax the 10,000 → 11,000, then add 5
        Assert.Equal(11005.5m, feeThenTax.Total()); // add 5 → 10,005, then tax it → 11,005.5
        Assert.NotEqual(taxThenFee.Total(), feeThenTax.Total());
    }
}
 */
