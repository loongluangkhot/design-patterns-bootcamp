namespace DesignPatternsBootcamp.Behavioral.Command.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the blotter mutates its state directly and immediately. It works, but the
/// actions are not <b>things</b>: you can't reverse one, replay one, queue one, or log one.
///
/// Once <see cref="CancelOrder"/> runs, the order (and its details) is simply gone — there is nowhere
/// the "how to undo this" lived. Adding multi-level undo/redo here means hand-stashing previous state
/// on every method and hand-rolling a history, all tangled into the blotter. The Command pattern
/// turns each action into an object that carries its own Execute + Undo, so a single generic invoker
/// can give the whole blotter undo/redo.
/// </summary>
public sealed class LegacyBlotter
{
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Order> Orders => _orders;
    public int Count => _orders.Count;

    public void AddOrder(Order order) => _orders.Add(order);

    public void CancelOrder(string id) => _orders.RemoveAll(o => o.Id == id);

    public void AmendQuantity(string id, int quantity)
    {
        Order? order = _orders.FirstOrDefault(o => o.Id == id);
        if (order is not null)
            order.Quantity = quantity;
    }
}
