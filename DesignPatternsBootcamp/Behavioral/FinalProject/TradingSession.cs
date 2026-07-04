using St = DesignPatternsBootcamp.Behavioral.State;
using Ob = DesignPatternsBootcamp.Behavioral.Observer;

namespace DesignPatternsBootcamp.Behavioral.FinalProject;

/// <summary>
/// WEEK 4 FINAL PROJECT — a live trading session that composes two behavioral patterns you built:
///
///   • <b>State</b> (Day 1b) drives the order through its lifecycle (New → PartiallyFilled →
///     Filled / Cancelled), enforcing legal transitions.
///   • <b>Observer</b> (Day 1a) broadcasts each new status to any number of subscribers (audit,
///     client notifier, …).
///
/// Every fill or cancel advances the state machine and then announces the result. Complete
/// <b>Day 1a</b> and <b>Day 1b</b> first — this session calls into both.
/// </summary>
public sealed class TradingSession
{
    private readonly string _orderId;
    private readonly St.OrderContext _order;
    private readonly Ob.OrderStatusPublisher _publisher;

    public TradingSession(string orderId, int quantity, Ob.OrderStatusPublisher publisher)
    {
        _orderId = orderId;
        _order = new St.OrderContext(quantity);
        _publisher = publisher;
    }

    public string Status => _order.Status;

    // Provided: the State machine's status name lines up 1:1 with the Observer's enum member names.
    private static Ob.OrderStatus ToStatus(string stateName) => Enum.Parse<Ob.OrderStatus>(stateName);

    /// <remarks>
    /// TODO(student): advance the state machine then announce the new status:
    ///   1. <c>_order.Fill(quantity)</c>
    ///   2. <c>_publisher.ChangeStatus(_orderId, ToStatus(_order.Status))</c>
    /// </remarks>
    public void Fill(int quantity) =>
        throw new NotImplementedException("TODO(student): fill the order (State), then publish the new status (Observer).");

    /// <remarks>TODO(student): the same two steps, but with <c>_order.Cancel()</c>.</remarks>
    public void Cancel() =>
        throw new NotImplementedException("TODO(student): cancel the order (State), then publish the new status (Observer).");
}
