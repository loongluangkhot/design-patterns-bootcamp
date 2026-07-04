namespace DesignPatternsBootcamp.Behavioral.Observer;

public enum OrderStatus { New, PartiallyFilled, Filled, Cancelled }

/// <summary>
/// The <b>Observer</b>: anything that wants to react to order status changes. The publisher calls
/// this without knowing (or caring) what the observer actually does.
/// </summary>
public interface IOrderObserver
{
    void OnStatusChanged(string orderId, OrderStatus status);
}

/// <summary>
/// The <b>Subject</b>: it keeps a list of observers and broadcasts each status change to all of them.
/// It has zero knowledge of the concrete observers — add or remove them freely, and the publisher
/// never changes. Your task is to implement the three methods.
/// </summary>
public sealed class OrderStatusPublisher
{
    private readonly List<IOrderObserver> _observers = new();

    public void Subscribe(IOrderObserver observer) =>
        throw new NotImplementedException("TODO(student): add observer to _observers.");

    public void Unsubscribe(IOrderObserver observer) =>
        throw new NotImplementedException("TODO(student): remove observer from _observers.");

    /// <remarks>TODO(student): notify EVERY subscribed observer by calling OnStatusChanged(orderId, status).</remarks>
    public void ChangeStatus(string orderId, OrderStatus status) =>
        throw new NotImplementedException("TODO(student): call OnStatusChanged on every observer in _observers.");
}
