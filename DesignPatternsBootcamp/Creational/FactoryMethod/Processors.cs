namespace DesignPatternsBootcamp.Creational.FactoryMethod;

// ---------------------------------------------------------------------------------------------
//  Concrete Products.
//  Each class owns the behaviour of exactly ONE rail. Port the matching branch of
//  LegacyPaymentGateway.Pay(...) into each Process(...) method below.
// ---------------------------------------------------------------------------------------------

public sealed class CardPaymentProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request) =>
        throw new NotImplementedException(
            "TODO(student): move the 'Card' branch of LegacyPaymentGateway here (2.9% + $0.30 fee).");
}

public sealed class AchPaymentProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request) =>
        throw new NotImplementedException(
            "TODO(student): move the 'Ach' branch of LegacyPaymentGateway here (flat $0.25 fee).");
}

public sealed class WirePaymentProcessor : IPaymentProcessor
{
    public PaymentResult Process(PaymentRequest request) =>
        throw new NotImplementedException(
            "TODO(student): move the 'Wire' branch of LegacyPaymentGateway here (flat $15.00 fee).");
}
