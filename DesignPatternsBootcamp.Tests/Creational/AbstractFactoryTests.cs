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
using DesignPatternsBootcamp.Creational.AbstractFactory;
using DesignPatternsBootcamp.Creational.AbstractFactory.Legacy;

namespace DesignPatternsBootcamp.Tests.Creational;

public class AbstractFactoryTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Pins the fee maths your products must reproduce.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_us_commission_is_ten_basis_points_with_a_dollar_floor()
    {
        var (amount, currency) = new LegacyTradeDesk().Commission("US", 100_000m);

        Assert.Equal(100.00m, amount); // 0.1% of 100k
        Assert.Equal("USD", currency);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the products and factories are implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Us_factory_builds_a_us_family()
    {
        var market = new UsMarketFactory();

        Assert.IsType<UsFeeSchedule>(market.CreateFeeSchedule());
        Assert.IsType<UsSettlementPolicy>(market.CreateSettlementPolicy());
    }

    [Fact]
    public void Eu_factory_builds_an_eu_family()
    {
        var market = new EuMarketFactory();

        Assert.IsType<EuFeeSchedule>(market.CreateFeeSchedule());
        Assert.IsType<EuSettlementPolicy>(market.CreateSettlementPolicy());
    }

    [Fact]
    public void Us_commission_matches_the_legacy_maths()
    {
        Money big = new UsFeeSchedule().Commission(100_000m);
        Money tiny = new UsFeeSchedule().Commission(500m); // below the $1 floor

        Assert.Equal(new Money(100.00m, "USD"), big);
        Assert.Equal(new Money(1.00m, "USD"), tiny);
    }

    [Fact]
    public void Eu_commission_matches_the_legacy_maths()
    {
        Money fee = new EuFeeSchedule().Commission(100_000m); // 1.20 + 0.2% of 100k

        Assert.Equal(new Money(201.20m, "EUR"), fee);
    }

    // The whole point of Abstract Factory: a booking built from ONE factory is internally
    // consistent. The commission currency always equals the settlement currency.
    [Fact]
    public void A_booking_is_internally_consistent_for_the_us_market()
    {
        BookingSummary summary = new TradeBooking(new UsMarketFactory()).Book(100_000m);

        Assert.Equal("USD", summary.Commission.Currency);
        Assert.Equal("USD", summary.SettlementCurrency);
        Assert.Equal(1, summary.SettlementDays);
    }

    [Fact]
    public void A_booking_is_internally_consistent_for_the_eu_market()
    {
        BookingSummary summary = new TradeBooking(new EuMarketFactory()).Book(100_000m);

        Assert.Equal("EUR", summary.Commission.Currency);
        Assert.Equal("EUR", summary.SettlementCurrency);
        Assert.Equal(2, summary.SettlementDays);
    }
}
*/
