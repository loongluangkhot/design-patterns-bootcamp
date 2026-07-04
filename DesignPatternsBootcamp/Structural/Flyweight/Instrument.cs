namespace DesignPatternsBootcamp.Structural.Flyweight;

/// <summary>
/// The <b>Flyweight</b>: an instrument's static reference data. It is <b>intrinsic</b> state —
/// identical for every order on that symbol and never changing — so it should exist <b>once</b> and
/// be shared, not copied into every order. Immutable, so sharing is safe.
/// </summary>
public sealed record Instrument(string Symbol, string Currency, decimal TickSize, int LotSize, string Exchange);

/// <summary>
/// An order (provided complete). Its <b>extrinsic</b> state — <see cref="Quantity"/> and
/// <see cref="Price"/> — is unique per order; its intrinsic reference data is a <b>shared</b>
/// <see cref="Instrument"/> handed in from the factory, not a private copy.
/// </summary>
public sealed class Order
{
    public Instrument Instrument { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public Order(Instrument instrument, int quantity, decimal price)
    {
        Instrument = instrument;
        Quantity = quantity;
        Price = price;
    }
}
