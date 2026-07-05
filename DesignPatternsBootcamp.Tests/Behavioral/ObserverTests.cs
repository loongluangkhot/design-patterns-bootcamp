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
using DesignPatternsBootcamp.Behavioral.Observer;
using DesignPatternsBootcamp.Behavioral.Observer.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class ObserverTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. The book hard-codes a call to each interested party.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_book_hard_codes_every_subscriber()
    {
        var book = new LegacyOrderBook();

        book.ChangeStatus("O1", OrderStatus.Filled);

        Assert.Equal(new[] { "O1:Filled" }, book.AuditEntries);
        Assert.Equal(1, book.ClientNotifications);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until Subscribe/Unsubscribe/ChangeStatus are implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Subscribed_observers_are_notified_on_a_status_change()
    {
        var publisher = new OrderStatusPublisher();
        var audit = new AuditLog();
        var notifier = new ClientNotifier();
        publisher.Subscribe(audit);
        publisher.Subscribe(notifier);

        publisher.ChangeStatus("O1", OrderStatus.Filled);

        Assert.Equal(new[] { "O1:Filled" }, audit.Entries);
        Assert.Equal(1, notifier.NotificationsSent);
        Assert.Equal(OrderStatus.Filled, notifier.LastStatus);
    }

    [Fact]
    public void Every_change_reaches_every_subscriber()
    {
        var publisher = new OrderStatusPublisher();
        var audit = new AuditLog();
        publisher.Subscribe(audit);

        publisher.ChangeStatus("O1", OrderStatus.PartiallyFilled);
        publisher.ChangeStatus("O1", OrderStatus.Filled);

        Assert.Equal(new[] { "O1:PartiallyFilled", "O1:Filled" }, audit.Entries);
    }

    [Fact]
    public void Unsubscribed_observers_stop_receiving_updates()
    {
        var publisher = new OrderStatusPublisher();
        var notifier = new ClientNotifier();
        publisher.Subscribe(notifier);

        publisher.ChangeStatus("O1", OrderStatus.New);
        publisher.Unsubscribe(notifier);
        publisher.ChangeStatus("O1", OrderStatus.Filled);

        Assert.Equal(1, notifier.NotificationsSent); // only the first change
    }

    [Fact]
    public void A_brand_new_observer_type_just_subscribes_no_publisher_change()
    {
        var publisher = new OrderStatusPublisher();
        var audit = new AuditLog();
        publisher.Subscribe(audit);

        publisher.ChangeStatus("O2", OrderStatus.Cancelled);

        Assert.Single(audit.Entries);
    }
}
*/
