namespace DesignPatternsBootcamp.Structural.Flyweight.Legacy;

/// <summary>
/// THE "BEFORE" CODE — every order carries its OWN copy of the instrument's reference data. Book a
/// million orders on AAPL and you have a million identical copies of "USD / 0.01 / 100 / NASDAQ"
/// sitting in memory.
///
/// The counter <see cref="ReferenceDataCopies"/> stands in for that waste: it ticks up once per
/// order, because each order re-materialises the same static data. Flyweight shares one instance
/// instead, so the count would be "one per distinct symbol", not "one per order".
/// </summary>
public sealed class LegacyOrder
{
    public static int ReferenceDataCopies { get; private set; }

    public string Symbol { get; }
    public string Currency { get; }
    public decimal TickSize { get; }
    public int LotSize { get; }
    public string Exchange { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public LegacyOrder(string symbol, int quantity, decimal price)
    {
        ReferenceDataCopies++; // a fresh copy of identical reference data, per order

        (Symbol, Currency, TickSize, LotSize, Exchange) = symbol switch
        {
            "AAPL" => ("AAPL", "USD", 0.01m, 100, "NASDAQ"),
            "MSFT" => ("MSFT", "USD", 0.01m, 100, "NASDAQ"),
            "SAP" => ("SAP", "EUR", 0.01m, 1, "XETRA"),
            _ => throw new KeyNotFoundException(symbol),
        };
        Quantity = quantity;
        Price = price;
    }
}
