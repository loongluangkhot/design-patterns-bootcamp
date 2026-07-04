namespace DesignPatternsBootcamp.Behavioral.State;

/// <summary>
/// The <b>Context</b> (provided): a working order. It doesn't contain any transition rules itself —
/// it delegates <see cref="Fill"/> and <see cref="Cancel"/> to whatever <see cref="IOrderState"/> it
/// is currently in. The states drive the transitions by setting <c>State</c> (and updating
/// <c>FilledQuantity</c>).
/// </summary>
public sealed class OrderContext
{
    public int Quantity { get; }
    public int FilledQuantity { get; internal set; }
    public IOrderState State { get; internal set; }

    public OrderContext(int quantity)
    {
        Quantity = quantity;
        State = new NewState(); // orders begin life as New
    }

    /// <summary>The current state's name — "New", "PartiallyFilled", "Filled", or "Cancelled".</summary>
    public string Status => State.Name;

    public void Fill(int quantity) => State.Fill(this, quantity);

    public void Cancel() => State.Cancel(this);
}
