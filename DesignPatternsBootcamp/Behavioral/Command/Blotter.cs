namespace DesignPatternsBootcamp.Behavioral.Command;

/// <summary>A working order on the blotter. Quantity is mutable — amendments happen in place.</summary>
public sealed class Order
{
    public string Id { get; }
    public string Symbol { get; }
    public int Quantity { get; set; }

    public Order(string id, string symbol, int quantity)
    {
        Id = id;
        Symbol = symbol;
        Quantity = quantity;
    }
}

/// <summary>
/// The <b>Receiver</b> (provided): the thing commands actually operate on. It knows how to add,
/// remove, and find orders — but nothing about undo/redo or history.
/// </summary>
public sealed class Blotter
{
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Order> Orders => _orders;
    public int Count => _orders.Count;

    public void Add(Order order) => _orders.Add(order);
    public void Remove(string id) => _orders.RemoveAll(o => o.Id == id);
    public Order? Find(string id) => _orders.FirstOrDefault(o => o.Id == id);
}

/// <summary>
/// The <b>Command</b> interface: a request turned into an object that can run itself — and, because
/// this is an <i>undoable</i> command, reverse itself.
/// </summary>
public interface ICommand
{
    void Execute();
    void Undo();
}
