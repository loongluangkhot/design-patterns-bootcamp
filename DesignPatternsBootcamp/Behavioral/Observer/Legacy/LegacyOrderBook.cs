namespace DesignPatternsBootcamp.Behavioral.Observer.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the order book itself hard-codes a call to every interested party each time a
/// status changes. Audit here, client notification there, all baked into one method.
///
/// Smells: the book is coupled to every subscriber and their APIs; adding a new interested party (a
/// risk monitor, a P&amp;L feed, a compliance archive) means editing this method; and subscribers can't
/// be added or removed at runtime. Observer flips this around: the book just announces "status
/// changed" and any number of observers listen, with the book knowing none of them.
/// </summary>
public sealed class LegacyOrderBook
{
    public List<string> AuditEntries { get; } = new();
    public int ClientNotifications { get; private set; }

    public void ChangeStatus(string orderId, OrderStatus status)
    {
        AuditEntries.Add($"{orderId}:{status}"); // audit — hard-coded
        ClientNotifications++;                    // notify client — hard-coded
        // Want a risk monitor too? Edit this method. And this one. And every other one like it.
    }
}
