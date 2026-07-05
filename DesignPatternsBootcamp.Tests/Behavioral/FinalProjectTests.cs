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
using DesignPatternsBootcamp.Behavioral.FinalProject;
using Ob = DesignPatternsBootcamp.Behavioral.Observer;

namespace DesignPatternsBootcamp.Tests.Behavioral;

/// <summary>
/// Week 4 final project. Requires Day 1a (Observer) AND Day 1b (State) implemented, plus
/// TradingSession.Fill/Cancel.
/// </summary>
public class FinalProjectTests
{
    [Fact]
    public void Filling_advances_the_state_machine_and_notifies_observers()
    {
        var publisher = new Ob.OrderStatusPublisher();
        var audit = new Ob.AuditLog();
        publisher.Subscribe(audit);
        var session = new TradingSession("O1", 100, publisher);

        session.Fill(40);
        Assert.Equal("PartiallyFilled", session.Status);
        Assert.Equal(new[] { "O1:PartiallyFilled" }, audit.Entries);

        session.Fill(60);
        Assert.Equal("Filled", session.Status);
        Assert.Equal(new[] { "O1:PartiallyFilled", "O1:Filled" }, audit.Entries);
    }

    [Fact]
    public void Cancelling_advances_the_state_machine_and_notifies_observers()
    {
        var publisher = new Ob.OrderStatusPublisher();
        var notifier = new Ob.ClientNotifier();
        publisher.Subscribe(notifier);
        var session = new TradingSession("O2", 100, publisher);

        session.Cancel();

        Assert.Equal("Cancelled", session.Status);
        Assert.Equal(1, notifier.NotificationsSent);
        Assert.Equal(Ob.OrderStatus.Cancelled, notifier.LastStatus);
    }

    [Fact]
    public void Every_subscribed_observer_sees_the_whole_lifecycle()
    {
        var publisher = new Ob.OrderStatusPublisher();
        var audit = new Ob.AuditLog();
        var notifier = new Ob.ClientNotifier();
        publisher.Subscribe(audit);
        publisher.Subscribe(notifier);
        var session = new TradingSession("O3", 100, publisher);

        session.Fill(100);

        Assert.Equal("Filled", session.Status);
        Assert.Equal(new[] { "O3:Filled" }, audit.Entries);
        Assert.Equal(1, notifier.NotificationsSent);
    }
}
*/
