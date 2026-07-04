namespace DesignPatternsBootcamp.Behavioral.Command;

// ---------------------------------------------------------------------------------------------
//  Concrete Commands. Each bundles a receiver + the arguments for one action, and knows how to
//  Execute it and Undo it. To undo, a command must capture whatever it needs BEFORE it changes
//  anything. Implement Execute and Undo for all three.
// ---------------------------------------------------------------------------------------------

public sealed class AddOrderCommand : ICommand
{
    private readonly Blotter _blotter;
    private readonly Order _order;

    public AddOrderCommand(Blotter blotter, Order order)
    {
        _blotter = blotter;
        _order = order;
    }

    public void Execute() =>
        throw new NotImplementedException("TODO(student): add _order to the blotter.");

    public void Undo() =>
        throw new NotImplementedException("TODO(student): remove _order (by its Id) from the blotter.");
}

public sealed class CancelOrderCommand : ICommand
{
    private readonly Blotter _blotter;
    private readonly string _orderId;
    private Order? _removed; // capture the cancelled order so Undo can put it back

    public CancelOrderCommand(Blotter blotter, string orderId)
    {
        _blotter = blotter;
        _orderId = orderId;
    }

    public void Execute() =>
        throw new NotImplementedException("TODO(student): find the order and stash it in _removed, then remove it from the blotter.");

    public void Undo() =>
        throw new NotImplementedException("TODO(student): if _removed is not null, add it back to the blotter.");
}

public sealed class AmendQuantityCommand : ICommand
{
    private readonly Blotter _blotter;
    private readonly string _orderId;
    private readonly int _newQuantity;
    private int _previousQuantity; // capture the old quantity so Undo can restore it

    public AmendQuantityCommand(Blotter blotter, string orderId, int newQuantity)
    {
        _blotter = blotter;
        _orderId = orderId;
        _newQuantity = newQuantity;
    }

    public void Execute() =>
        throw new NotImplementedException("TODO(student): find the order; save its current Quantity into _previousQuantity, then set it to _newQuantity.");

    public void Undo() =>
        throw new NotImplementedException("TODO(student): find the order and restore its Quantity to _previousQuantity.");
}
