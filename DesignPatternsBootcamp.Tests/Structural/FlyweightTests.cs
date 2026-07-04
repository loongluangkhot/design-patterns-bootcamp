using DesignPatternsBootcamp.Structural.Flyweight;
using DesignPatternsBootcamp.Structural.Flyweight.Legacy;

namespace DesignPatternsBootcamp.Tests.Structural;

public class FlyweightTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Every order copies the reference data (the waste).
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_orders_each_carry_their_own_copy_of_reference_data()
    {
        int before = LegacyOrder.ReferenceDataCopies;

        _ = new LegacyOrder("AAPL", 10, 150m);
        _ = new LegacyOrder("AAPL", 20, 151m);
        _ = new LegacyOrder("AAPL", 30, 152m);

        Assert.Equal(before + 3, LegacyOrder.ReferenceDataCopies); // 3 orders → 3 copies of the same data
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until InstrumentFactory.Get interns instruments.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Same_symbol_returns_the_very_same_shared_instance()
    {
        var factory = new InstrumentFactory();

        Assert.Same(factory.Get("AAPL"), factory.Get("AAPL"));
    }

    [Fact]
    public void Different_symbols_return_different_instances()
    {
        var factory = new InstrumentFactory();

        Assert.NotSame(factory.Get("AAPL"), factory.Get("MSFT"));
    }

    [Fact]
    public void The_pool_holds_one_flyweight_per_distinct_symbol()
    {
        var factory = new InstrumentFactory();

        factory.Get("AAPL");
        factory.Get("AAPL");
        factory.Get("AAPL");
        factory.Get("MSFT");
        factory.Get("MSFT");

        Assert.Equal(2, factory.DistinctInstrumentCount);
    }

    [Fact]
    public void The_flyweight_carries_the_reference_data()
    {
        Instrument aapl = new InstrumentFactory().Get("AAPL");

        Assert.Equal("USD", aapl.Currency);
        Assert.Equal(0.01m, aapl.TickSize);
        Assert.Equal("NASDAQ", aapl.Exchange);
    }

    [Fact]
    public void Many_orders_share_a_single_instrument_object()
    {
        var factory = new InstrumentFactory();

        var o1 = new Order(factory.Get("AAPL"), 10, 150m);
        var o2 = new Order(factory.Get("AAPL"), 20, 151m);
        var o3 = new Order(factory.Get("AAPL"), 30, 152m);

        Assert.Same(o1.Instrument, o2.Instrument);
        Assert.Same(o2.Instrument, o3.Instrument);
        Assert.Equal(1, factory.DistinctInstrumentCount); // one flyweight backs all three orders
    }
}
