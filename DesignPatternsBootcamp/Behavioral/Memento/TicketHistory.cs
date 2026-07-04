namespace DesignPatternsBootcamp.Behavioral.Memento;

/// <summary>
/// The <b>Caretaker</b> (provided): it keeps a stack of checkpoints and can roll a ticket back. Note
/// what it does NOT do — it never reads a memento's contents. It only stores the opaque tokens the
/// originator hands it and gives them back on undo. That's the caretaker's proper role.
/// </summary>
public sealed class TicketHistory
{
    private readonly Stack<OrderTicket.Memento> _checkpoints = new();

    public int Count => _checkpoints.Count;

    /// <summary>Checkpoint the ticket's current state.</summary>
    public void Save(OrderTicket ticket) => _checkpoints.Push(ticket.Save());

    /// <summary>Roll the ticket back to the most recent checkpoint, if any.</summary>
    public void Undo(OrderTicket ticket)
    {
        if (_checkpoints.Count > 0)
            ticket.Restore(_checkpoints.Pop());
    }
}
