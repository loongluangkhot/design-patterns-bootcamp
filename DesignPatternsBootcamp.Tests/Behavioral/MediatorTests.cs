using DesignPatternsBootcamp.Behavioral.Mediator;
using DesignPatternsBootcamp.Behavioral.Mediator.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class MediatorTests
{
    private static TradingDeskMediator BuildDesk(
        out ExecutionComponent execution, out NotificationComponent notifications, decimal limit = 1_000_000m)
    {
        execution = new ExecutionComponent();
        notifications = new NotificationComponent();
        return new TradingDeskMediator(new RiskComponent(limit), execution, notifications);
    }

    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. One class hard-wires the entire flow.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_entry_hard_wires_the_whole_flow()
    {
        var entry = new LegacyOrderEntry();

        Assert.StartsWith("EXECUTED: EXE-O1", entry.Submit(new Order("O1", "AAPL", 10, 150m), 1_000_000m));
        Assert.StartsWith("REJECTED", entry.Submit(new Order("O2", "AAPL", 100, 150m), 1000m));
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the mediator's Submit coordinates the colleagues.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void An_approved_order_is_executed_and_notified()
    {
        var desk = BuildDesk(out ExecutionComponent execution, out NotificationComponent notifications);

        OrderOutcome outcome = desk.Submit(new Order("O1", "AAPL", 10, 150m)); // notional 1,500 < 1M

        Assert.True(outcome.Executed);
        Assert.Equal("EXE-O1", outcome.ExecutionRef);
        Assert.Equal(1, execution.Executions);
        Assert.Contains(notifications.Sent, m => m.Contains("executed"));
    }

    [Fact]
    public void A_rejected_order_is_not_executed_but_is_still_notified()
    {
        var desk = BuildDesk(out ExecutionComponent execution, out NotificationComponent notifications, limit: 1000m);

        OrderOutcome outcome = desk.Submit(new Order("O2", "AAPL", 100, 150m)); // notional 15,000 > 1,000

        Assert.False(outcome.Executed);
        Assert.Null(outcome.ExecutionRef);
        Assert.Equal("Exceeds risk limit.", outcome.Message);
        Assert.Equal(0, execution.Executions); // execution was never asked to run
        Assert.Contains(notifications.Sent, m => m.Contains("rejected"));
    }

    [Fact]
    public void The_mediator_coordinates_colleagues_that_never_touch_each_other()
    {
        var desk = BuildDesk(out ExecutionComponent execution, out NotificationComponent notifications);

        desk.Submit(new Order("O3", "MSFT", 5, 400m));

        Assert.Equal(1, execution.Executions);
        Assert.Single(notifications.Sent);
    }
}
