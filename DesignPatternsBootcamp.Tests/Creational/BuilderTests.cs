using DesignPatternsBootcamp.Creational.Builder;
using DesignPatternsBootcamp.Creational.Builder.Legacy;

namespace DesignPatternsBootcamp.Tests.Creational;

public class BuilderTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Shows the "wall of nulls" call site we want to kill.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_order_needs_a_wall_of_nulls_even_for_a_market_order()
    {
        var order = new LegacyOrder("AAPL", "Buy", 100, "Market",
            null, null, "Day", "ACC-1", null, null);

        Assert.Equal("AAPL", order.Symbol);
        Assert.Equal(100, order.Quantity);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until Build() is implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Builds_a_simple_market_order_with_sensible_defaults()
    {
        var order = TradeOrderBuilder.Create()
            .Buy(100, "AAPL")
            .ForAccount("ACC-1")
            .Build();

        Assert.Equal(Side.Buy, order.Side);
        Assert.Equal(100, order.Quantity);
        Assert.Equal("AAPL", order.Symbol);
        Assert.Equal(OrderType.Market, order.OrderType);   // default
        Assert.Null(order.LimitPrice);
        Assert.Null(order.StopPrice);
        Assert.Equal(TimeInForce.Day, order.TimeInForce);  // default
        Assert.Empty(order.Allocations);
    }

    [Fact]
    public void Builds_a_gtc_limit_order_reading_like_a_sentence()
    {
        var order = TradeOrderBuilder.Create()
            .Sell(50, "MSFT")
            .Limit(300.50m)
            .GoodTilCanceled()
            .ForAccount("ACC-2")
            .WithNote("trim position")
            .Build();

        Assert.Equal(Side.Sell, order.Side);
        Assert.Equal(OrderType.Limit, order.OrderType);
        Assert.Equal(300.50m, order.LimitPrice);
        Assert.Equal(TimeInForce.GoodTilCanceled, order.TimeInForce);
        Assert.Equal("trim position", order.Note);
    }

    [Fact]
    public void Builds_a_stop_limit_order_with_both_prices()
    {
        var order = TradeOrderBuilder.Create()
            .Buy(10, "TSLA")
            .StopLimit(stopPrice: 250m, limitPrice: 255m)
            .ForAccount("ACC-3")
            .Build();

        Assert.Equal(OrderType.StopLimit, order.OrderType);
        Assert.Equal(250m, order.StopPrice);
        Assert.Equal(255m, order.LimitPrice);
    }

    [Fact]
    public void Allocations_that_sum_to_the_quantity_are_accepted()
    {
        var order = TradeOrderBuilder.Create()
            .Buy(100, "AAPL")
            .ForAccount("ACC-1")
            .AllocateTo("SUB-A", 60)
            .AllocateTo("SUB-B", 40)
            .Build();

        Assert.Equal(2, order.Allocations.Count);
    }

    [Fact]
    public void Allocations_that_do_not_sum_to_the_quantity_are_rejected()
    {
        var builder = TradeOrderBuilder.Create()
            .Buy(100, "AAPL")
            .ForAccount("ACC-1")
            .AllocateTo("SUB-A", 60)
            .AllocateTo("SUB-B", 30); // sums to 90, not 100

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void A_non_positive_quantity_is_rejected_at_build_time()
    {
        var builder = TradeOrderBuilder.Create().Buy(0, "AAPL").ForAccount("ACC-1");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void An_order_without_an_account_is_rejected()
    {
        var builder = TradeOrderBuilder.Create().Buy(100, "AAPL");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void An_order_without_a_symbol_is_rejected()
    {
        var builder = TradeOrderBuilder.Create().Buy(100, "").ForAccount("ACC-1");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Fact]
    public void Built_orders_are_immutable_snapshots_not_live_views_of_the_builder()
    {
        var builder = TradeOrderBuilder.Create()
            .Buy(100, "AAPL")
            .ForAccount("ACC-1")
            .AllocateTo("SUB-A", 100);

        TradeOrder first = builder.Build();

        // Keep using the builder after building. The already-built order must not change.
        builder.AllocateTo("SUB-B", 50);

        Assert.Single(first.Allocations);
    }
}
