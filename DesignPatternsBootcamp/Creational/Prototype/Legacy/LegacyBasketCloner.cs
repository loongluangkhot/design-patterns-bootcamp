namespace DesignPatternsBootcamp.Creational.Prototype.Legacy;

// The legacy world uses its OWN basket types (independent of the pattern you build), so this "before"
// code stands on its own.

public sealed class LegacyOrderLine
{
    public required string Symbol { get; set; }
    public required int Quantity { get; set; }
}

public sealed class LegacyBasket
{
    public required string Name { get; set; }
    public required List<LegacyOrderLine> Lines { get; set; }
}

/// <summary>
/// THE "BEFORE" CODE — it "copies" a basket by copying the scalar fields and REUSING the same
/// <c>Lines</c> list (and the same <c>LegacyOrderLine</c> objects inside it).
///
/// That is a <b>shallow copy</b>, and it is a landmine: editing the "copy" reaches back and mutates
/// the original — and every other basket cloned from it. The Prototype pattern fixes this by giving
/// each object a <c>DeepClone</c> that copies all the way down.
/// </summary>
public sealed class LegacyBasketCloner
{
    public LegacyBasket Copy(LegacyBasket basket) => new()
    {
        Name = basket.Name,
        Lines = basket.Lines, // BUG: shares the list reference and every LegacyOrderLine in it.
    };
}
