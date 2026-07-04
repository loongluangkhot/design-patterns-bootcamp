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
using DesignPatternsBootcamp.Structural.Composite;
using DesignPatternsBootcamp.Structural.Integration;
using DesignPatternsBootcamp.Structural.Proxy;

namespace DesignPatternsBootcamp.Tests.Structural;

/// <summary>
/// Week 2 capstone. Requires Day 4 (Proxy) implemented, plus LivePortfolioValuation.
/// (Composite's tree structure is used directly, so its MarketValue kata need not be done.)
/// </summary>
public class StructuralIntegrationTests
{
    private static LivePortfolioValuation Valuation(out RealPriceService backend)
    {
        backend = new RealPriceService();
        IPriceService prices = new CachingPriceProxy(backend);
        return new LivePortfolioValuation(prices);
    }

    [Fact]
    public void Values_a_composite_book_at_live_prices()
    {
        LivePortfolioValuation valuation = Valuation(out _);

        var book = new Portfolio("Book")
            .Add(new Position("AAPL", 10, price: 0m)) // stored book price is ignored; we reprice live
            .Add(new Position("MSFT", 5, price: 0m));

        // Live: AAPL=150, MSFT=400 → 10×150 + 5×400 = 3500
        Assert.Equal(3500m, valuation.MarketValue(book));
    }

    [Fact]
    public void Nested_sub_portfolios_are_priced_recursively()
    {
        LivePortfolioValuation valuation = Valuation(out _);

        var equities = new Portfolio("Equities")
            .Add(new Position("AAPL", 10, 0m))
            .Add(new Position("MSFT", 5, 0m));
        var hedge = new Portfolio("Hedge")
            .Add(new Position("AAPL", 3, 0m));
        var book = new Portfolio("Book").Add(equities).Add(hedge);

        // (10×150 + 5×400) + (3×150) = 3500 + 450 = 3950
        Assert.Equal(3950m, valuation.MarketValue(book));
    }

    [Fact]
    public void Each_distinct_symbol_is_priced_only_once_across_the_whole_book()
    {
        LivePortfolioValuation valuation = Valuation(out RealPriceService backend);

        var equities = new Portfolio("Equities")
            .Add(new Position("AAPL", 10, 0m))
            .Add(new Position("MSFT", 5, 0m));
        var hedge = new Portfolio("Hedge")
            .Add(new Position("AAPL", 3, 0m)); // AAPL appears a second time

        var book = new Portfolio("Book").Add(equities).Add(hedge);

        valuation.MarketValue(book);

        Assert.Equal(2, backend.CallCount); // AAPL + MSFT — the caching proxy deduped AAPL
    }
}
*/
