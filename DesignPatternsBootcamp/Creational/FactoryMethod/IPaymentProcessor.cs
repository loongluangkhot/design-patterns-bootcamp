namespace DesignPatternsBootcamp.Creational.FactoryMethod;

/// <summary>
/// The <b>Product</b> in Factory Method terms: something that can process one payment.
/// Each rail (card, ACH, wire) gets its own concrete implementation.
/// </summary>
public interface IPaymentProcessor
{
    PaymentResult Process(PaymentRequest request);
}
