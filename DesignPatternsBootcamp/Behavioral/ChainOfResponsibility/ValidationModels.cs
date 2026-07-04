namespace DesignPatternsBootcamp.Behavioral.ChainOfResponsibility;

// Provided data types. You build the validator chain; these are the values it works with.

/// <summary>An incoming order to validate.</summary>
public record Order(string Symbol, int Quantity, decimal Price)
{
    public decimal Notional => Quantity * Price;
}

/// <summary>The outcome of running an order through the validation chain.</summary>
public record ValidationResult(bool Approved, string? Reason)
{
    public static ValidationResult Pass() => new(true, null);
    public static ValidationResult Reject(string reason) => new(false, reason);
}
