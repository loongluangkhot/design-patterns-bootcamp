namespace DesignPatternsBootcamp.Behavioral.Mediator;

/// <summary>The <b>Mediator</b> interface — the single hub colleagues talk through.</summary>
public interface IDeskMediator
{
    OrderOutcome Submit(Order order);
}

/// <summary>
/// The <b>Concrete Mediator</b>: it owns the interaction logic that would otherwise be tangled
/// across the colleagues. Risk, Execution, and Notification never reference each other — the
/// mediator wires their collaboration. Adding a colleague changes only this class.
///
/// Your task is to implement <see cref="Submit"/>.
/// </summary>
public sealed class TradingDeskMediator : IDeskMediator
{
    private readonly RiskComponent _risk;
    private readonly ExecutionComponent _execution;
    private readonly NotificationComponent _notifications;

    public TradingDeskMediator(RiskComponent risk, ExecutionComponent execution, NotificationComponent notifications)
    {
        _risk = risk;
        _execution = execution;
        _notifications = notifications;
    }

    /// <remarks>
    /// TODO(student): coordinate the colleagues:
    ///   1. Ask <c>_risk.Approve(order, out var reason)</c>. If it fails, notify
    ///      <c>$"Order {order.Id} rejected: {reason}"</c> and return an un-executed outcome carrying
    ///      the reason — do NOT call execution.
    ///   2. Otherwise execute (<c>_execution.Execute(order)</c> returns a reference), notify
    ///      <c>$"Order {order.Id} executed: {executionRef}"</c>, and return an executed outcome with
    ///      that reference.
    /// </remarks>
    public OrderOutcome Submit(Order order) =>
        throw new NotImplementedException(
            "TODO(student): coordinate risk -> execution -> notification. See <remarks>.");
}
