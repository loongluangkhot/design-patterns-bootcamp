namespace DesignPatternsBootcamp.Behavioral.ChainOfResponsibility;

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

/// <summary>
/// The <b>Handler</b>. It knows how to do its own check and how to pass the order to the next
/// handler — but not what the other handlers are. Chains are assembled with <see cref="SetNext"/>.
///
/// <see cref="Validate"/> (the chaining machinery) is provided. Your job is each concrete
/// handler's <see cref="Check"/>.
/// </summary>
public abstract class OrderValidator
{
    private OrderValidator? _next;

    /// <summary>Link the next handler and return it, so chains read fluently.</summary>
    public OrderValidator SetNext(OrderValidator next)
    {
        _next = next;
        return next;
    }

    public ValidationResult Validate(Order order)
    {
        string? reason = Check(order);
        if (reason is not null)
            return ValidationResult.Reject(reason); // I handled it (by rejecting) — stop here.

        return _next?.Validate(order) ?? ValidationResult.Pass(); // pass along, or approve at the end.
    }

    /// <summary>Return null if this rule passes, or a rejection reason if it fails.</summary>
    protected abstract string? Check(Order order);
}

// ---------------------------------------------------------------------------------------------
//  Concrete Handlers — one rule each. Implement Check for all four.
// ---------------------------------------------------------------------------------------------

public sealed class PositiveQuantityValidator : OrderValidator
{
    protected override string? Check(Order order) =>
        throw new NotImplementedException("TODO(student): reject with \"Quantity must be positive.\" when Quantity <= 0, else null.");
}

public sealed class PriceBandValidator : OrderValidator
{
    protected override string? Check(Order order) =>
        throw new NotImplementedException("TODO(student): reject with \"Price must be positive.\" when Price <= 0, else null.");
}

public sealed class NotionalLimitValidator : OrderValidator
{
    private readonly decimal _limit;

    public NotionalLimitValidator(decimal limit) => _limit = limit;

    protected override string? Check(Order order) =>
        throw new NotImplementedException("TODO(student): reject with \"Notional exceeds limit.\" when order.Notional > _limit, else null.");
}

public sealed class RestrictedSymbolValidator : OrderValidator
{
    private readonly HashSet<string> _restricted;

    public RestrictedSymbolValidator(params string[] restricted) => _restricted = new HashSet<string>(restricted);

    protected override string? Check(Order order) =>
        throw new NotImplementedException("TODO(student): reject with $\"Symbol {order.Symbol} is restricted.\" when it's in _restricted, else null.");
}
