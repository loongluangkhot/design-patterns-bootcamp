namespace DesignPatternsBootcamp.Behavioral.Memento.Legacy;

/// <summary>
/// THE "BEFORE" CODE — a plain ticket whose every field is public. To snapshot it, external code
/// copies each field out; to restore, it copies each field back (see the legacy test).
///
/// Smells: the ticket's entire internal state is exposed just to enable undo; the snapshot logic
/// lives outside the ticket and is duplicated at every save site; and if the ticket gains a field
/// (say, Side), every one of those sites silently forgets to save it. Memento lets the ticket
/// capture its own state into an opaque token, so callers snapshot without seeing the internals.
/// </summary>
public sealed class LegacyTicket
{
    public string Symbol { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public LegacyTicket(string symbol, int quantity, decimal price)
    {
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
    }
}
