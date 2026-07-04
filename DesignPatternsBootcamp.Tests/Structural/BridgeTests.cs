using DesignPatternsBootcamp.Structural.Bridge;
using DesignPatternsBootcamp.Structural.Bridge.Legacy;

namespace DesignPatternsBootcamp.Tests.Structural;

public class BridgeTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. One class per (indicator × feed) combination.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_primary_last_trade_is_the_final_primary_tick()
    {
        Assert.Equal(102m, new PrimaryLastTrade().Value("AAPL"));
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the two refined indicators are implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Last_trade_over_the_primary_feed()
    {
        Assert.Equal(102m, new LastTradeIndicator(new PrimaryFeed()).Value("AAPL"));
    }

    [Fact]
    public void Moving_average_over_the_primary_feed()
    {
        Assert.Equal(101m, new MovingAverageIndicator(new PrimaryFeed()).Value("AAPL")); // (100+101+102)/3
    }

    [Fact]
    public void Last_trade_over_the_backup_feed()
    {
        Assert.Equal(104m, new LastTradeIndicator(new BackupFeed()).Value("AAPL"));
    }

    // The pay-off: 2 indicators × 2 feeds = 4 behaviours, built from only 2 + 2 pieces and NO
    // combination classes. Swap either axis freely.
    [Fact]
    public void Any_indicator_combines_with_any_feed()
    {
        Assert.Equal(102m, new LastTradeIndicator(new PrimaryFeed()).Value("AAPL"));
        Assert.Equal(104m, new LastTradeIndicator(new BackupFeed()).Value("AAPL"));
        Assert.Equal(101m, new MovingAverageIndicator(new PrimaryFeed()).Value("AAPL"));
        Assert.Equal(102m, new MovingAverageIndicator(new BackupFeed()).Value("AAPL")); // (100+104)/2
    }
}
