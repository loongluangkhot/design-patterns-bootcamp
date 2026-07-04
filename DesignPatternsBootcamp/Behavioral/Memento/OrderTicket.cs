namespace DesignPatternsBootcamp.Behavioral.Memento;

/// <summary>
/// The <b>Originator</b>: an order ticket being edited. It can snapshot its own state into a
/// <see cref="Memento"/> and restore itself from one — without ever letting outside code read or
/// tamper with that snapshot.
///
/// Your task is <see cref="Save"/> and <see cref="Restore"/>.
/// </summary>
public sealed class OrderTicket
{
    public string Symbol { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public OrderTicket(string symbol, int quantity, decimal price)
    {
        Symbol = symbol;
        Quantity = quantity;
        Price = price;
    }

    /// <summary>
    /// The <b>Memento</b>. Its state is <c>internal</c>, so code outside this assembly (the caretaker
    /// and the tests) can hold and pass one around but cannot read or forge its contents. Only the
    /// originator (this class) can look inside. That opacity is the whole point of Memento.
    /// </summary>
    public sealed class Memento
    {
        internal string Symbol { get; }
        internal int Quantity { get; }
        internal decimal Price { get; }

        internal Memento(string symbol, int quantity, decimal price)
        {
            Symbol = symbol;
            Quantity = quantity;
            Price = price;
        }
    }

    /// <remarks>TODO(student): capture the current state — return a new Memento built from Symbol, Quantity, Price.</remarks>
    public Memento Save() =>
        throw new NotImplementedException("TODO(student): return new Memento(Symbol, Quantity, Price).");

    /// <remarks>TODO(student): copy the memento's state back into this ticket's Symbol, Quantity, Price.</remarks>
    public void Restore(Memento memento) =>
        throw new NotImplementedException("TODO(student): set Symbol/Quantity/Price from memento.");
}
