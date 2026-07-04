namespace DesignPatternsBootcamp.Creational.FactoryMethod;

/// <summary>The payment rails our fintech app can send money over.</summary>
public enum PaymentMethod
{
    Card,
    Ach,
    Wire,
}

/// <summary>An outbound payment we have been asked to move.</summary>
public record PaymentRequest(
    decimal Amount,
    string Currency,
    string SourceAccount,
    string DestinationAccount);

/// <summary>The outcome of attempting to move a payment.</summary>
public record PaymentResult(bool Success, string? Confirmation, decimal Fee, string Message)
{
    public static PaymentResult Ok(string confirmation, decimal fee, string message) =>
        new(true, confirmation, fee, message);

    public static PaymentResult Rejected(string message) =>
        new(false, null, 0m, message);
}
