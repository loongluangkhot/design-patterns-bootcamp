using DesignPatternsBootcamp.Behavioral.State;
using DesignPatternsBootcamp.Behavioral.State.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class StateTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Every operation re-checks a status string.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_order_guards_transitions_with_status_checks()
    {
        var order = new LegacyOrder(100);

        order.Fill(40);
        Assert.Equal("PartiallyFilled", order.Status);
        order.Fill(60);
        Assert.Equal("Filled", order.Status);

        Assert.Throws<InvalidOperationException>(() => order.Cancel());
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the four states implement their transitions.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void A_new_order_starts_in_the_new_state()
    {
        Assert.Equal("New", new OrderContext(100).Status);
    }

    [Fact]
    public void A_partial_fill_moves_to_partially_filled()
    {
        var order = new OrderContext(100);

        order.Fill(40);

        Assert.Equal("PartiallyFilled", order.Status);
        Assert.Equal(40, order.FilledQuantity);
    }

    [Fact]
    public void Filling_the_remainder_moves_to_filled()
    {
        var order = new OrderContext(100);

        order.Fill(40);
        order.Fill(60);

        Assert.Equal("Filled", order.Status);
    }

    [Fact]
    public void A_working_order_can_be_cancelled()
    {
        var order = new OrderContext(100);

        order.Cancel();

        Assert.Equal("Cancelled", order.Status);
    }

    [Fact]
    public void A_filled_order_cannot_be_filled_or_cancelled()
    {
        var order = new OrderContext(100);
        order.Fill(100);

        Assert.Throws<InvalidOperationException>(() => order.Fill(1));
        Assert.Throws<InvalidOperationException>(() => order.Cancel());
    }

    [Fact]
    public void A_cancelled_order_rejects_further_operations()
    {
        var order = new OrderContext(100);
        order.Cancel();

        Assert.Throws<InvalidOperationException>(() => order.Cancel());
        Assert.Throws<InvalidOperationException>(() => order.Fill(1));
    }
}
