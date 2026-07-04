namespace DesignPatternsBootcamp.Structural.Flyweight;

/// <summary>
/// The <b>Flyweight Factory</b>: it hands out shared <see cref="Instrument"/> objects. Ask it for
/// "AAPL" a million times and it materialises the reference data <b>once</b>, then returns that same
/// instance every time. This is what turns "a million copies" into "one shared object".
///
/// Your task is to implement <see cref="Get"/> so it pools (interns) instruments.
/// </summary>
public sealed class InstrumentFactory
{
    // Raw reference data in primitive form. The factory builds an Instrument from a row the FIRST
    // time that symbol is requested.
    private static readonly IReadOnlyDictionary<string, (string Currency, decimal TickSize, int LotSize, string Exchange)> Catalog =
        new Dictionary<string, (string, decimal, int, string)>
        {
            ["AAPL"] = ("USD", 0.01m, 100, "NASDAQ"),
            ["MSFT"] = ("USD", 0.01m, 100, "NASDAQ"),
            ["SAP"] = ("EUR", 0.01m, 1, "XETRA"),
        };

    private readonly Dictionary<string, Instrument> _pool = new();

    /// <summary>How many distinct flyweights have been materialised (the size of the shared pool).</summary>
    public int DistinctInstrumentCount => _pool.Count;

    /// <remarks>
    /// TODO(student): return the SHARED instrument for <paramref name="symbol"/>:
    ///   • if it's already in <c>_pool</c>, return that same instance;
    ///   • otherwise build one from <c>Catalog[symbol]</c>, store it in <c>_pool</c>, and return it.
    /// Two calls with the same symbol must return the very same object (reference-equal).
    /// </remarks>
    public Instrument Get(string symbol) =>
        throw new NotImplementedException(
            "TODO(student): intern instruments — one shared instance per symbol. See <remarks>.");
}
