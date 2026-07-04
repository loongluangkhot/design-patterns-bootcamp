namespace DesignPatternsBootcamp.Behavioral.State;

/// <summary>
/// The <b>State</b> interface. Each state knows what <see cref="Fill"/> and <see cref="Cancel"/> mean
/// <i>for that state</i> — including moving the context to the next state, or refusing the operation.
/// The <c>Name</c> of each state is provided; you implement the transitions.
/// </summary>
public interface IOrderState
{
    string Name { get; }
    void Fill(OrderContext context, int quantity);
    void Cancel(OrderContext context);
}

public sealed class NewState : IOrderState
{
    public string Name => "New";

    /// <remarks>
    /// TODO(student): add <paramref name="quantity"/> to <c>context.FilledQuantity</c>, then move
    /// <c>context.State</c> to a <see cref="FilledState"/> if fully filled
    /// (FilledQuantity &gt;= Quantity) or a <see cref="PartiallyFilledState"/> otherwise.
    /// </remarks>
    public void Fill(OrderContext context, int quantity) =>
        throw new NotImplementedException("TODO(student): fill, then transition to Filled or PartiallyFilled.");

    /// <remarks>TODO(student): move context.State to a new CancelledState.</remarks>
    public void Cancel(OrderContext context) =>
        throw new NotImplementedException("TODO(student): transition to Cancelled.");
}

public sealed class PartiallyFilledState : IOrderState
{
    public string Name => "PartiallyFilled";

    /// <remarks>TODO(student): same fill logic as NewState — accumulate, then Filled or stay PartiallyFilled.</remarks>
    public void Fill(OrderContext context, int quantity) =>
        throw new NotImplementedException("TODO(student): fill, then transition to Filled or stay PartiallyFilled.");

    /// <remarks>TODO(student): move context.State to a new CancelledState.</remarks>
    public void Cancel(OrderContext context) =>
        throw new NotImplementedException("TODO(student): transition to Cancelled.");
}

public sealed class FilledState : IOrderState
{
    public string Name => "Filled";

    /// <remarks>TODO(student): a filled order can't be filled again — throw InvalidOperationException.</remarks>
    public void Fill(OrderContext context, int quantity) =>
        throw new NotImplementedException("TODO(student): throw InvalidOperationException — illegal transition.");

    /// <remarks>TODO(student): a filled order can't be cancelled — throw InvalidOperationException.</remarks>
    public void Cancel(OrderContext context) =>
        throw new NotImplementedException("TODO(student): throw InvalidOperationException — illegal transition.");
}

public sealed class CancelledState : IOrderState
{
    public string Name => "Cancelled";

    /// <remarks>TODO(student): a cancelled order can't be filled — throw InvalidOperationException.</remarks>
    public void Fill(OrderContext context, int quantity) =>
        throw new NotImplementedException("TODO(student): throw InvalidOperationException — illegal transition.");

    /// <remarks>TODO(student): a cancelled order can't be cancelled again — throw InvalidOperationException.</remarks>
    public void Cancel(OrderContext context) =>
        throw new NotImplementedException("TODO(student): throw InvalidOperationException — illegal transition.");
}
