namespace DesignPatternsBootcamp.Behavioral.ChainOfResponsibility.Legacy;

/// <summary>
/// THE "BEFORE" CODE — one method that runs every rule in a fixed sequence of <c>if</c>s, and needs
/// every rule's configuration passed in at once.
///
/// Smells: you can't reorder, skip, or reuse an individual rule; a rule can't be unit-tested on its
/// own; adding a rule edits this method (and its parameter list); and different desks that want
/// different rule sets all share this one hard-coded pipeline. Chain of Responsibility turns each
/// rule into a standalone handler you can assemble into whatever pipeline a caller needs.
/// </summary>
public sealed class LegacyOrderValidator
{
    public string? Validate(Order order, decimal notionalLimit, ISet<string> restrictedSymbols)
    {
        if (order.Quantity <= 0)
            return "Quantity must be positive.";
        if (order.Price <= 0)
            return "Price must be positive.";
        if (order.Notional > notionalLimit)
            return "Notional exceeds limit.";
        if (restrictedSymbols.Contains(order.Symbol))
            return $"Symbol {order.Symbol} is restricted.";
        return null;
    }
}
