namespace DesignPatternsBootcamp.Behavioral.Observer;

// ---------------------------------------------------------------------------------------------
//  Concrete Observers (provided). Each reacts to a status change in its own way. Notice they
//  know nothing about the publisher or about each other.
// ---------------------------------------------------------------------------------------------

/// <summary>Writes an audit trail of every status change it sees.</summary>
public sealed class AuditLog : IOrderObserver
{
    private readonly List<string> _entries = new();

    public IReadOnlyList<string> Entries => _entries;

    public void OnStatusChanged(string orderId, OrderStatus status) =>
        _entries.Add($"{orderId}:{status}");
}

/// <summary>Pretends to push a message to the client on every change.</summary>
public sealed class ClientNotifier : IOrderObserver
{
    public int NotificationsSent { get; private set; }
    public OrderStatus? LastStatus { get; private set; }

    public void OnStatusChanged(string orderId, OrderStatus status)
    {
        NotificationsSent++;
        LastStatus = status;
    }
}
