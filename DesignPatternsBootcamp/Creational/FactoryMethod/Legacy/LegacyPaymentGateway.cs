namespace DesignPatternsBootcamp.Creational.FactoryMethod.Legacy;

/// <summary>
/// THE "BEFORE" CODE — this works today, but it is where the pain lives.
///
/// Every payment rail is jammed into one method behind a <c>switch</c>. Notice the smells:
///   • Open/Closed violation  — adding a rail (say, real-time payments) means EDITING this method.
///   • Single-Responsibility  — validation, fee maths, and rail-specific behaviour all live together.
///   • Hard to unit-test       — you cannot test "card fee logic" without going through the gateway.
///   • Shotgun surgery         — a change to how wires work forces a change to this shared method.
///
/// Your job (see README.md) is to refactor this into the Factory Method pattern WITHOUT changing
/// the observable behaviour that the tests pin down.
/// </summary>
public class LegacyPaymentGateway
{
    public PaymentResult Pay(PaymentMethod method, PaymentRequest request)
    {
        if (request.Amount <= 0)
            return PaymentResult.Rejected("Amount must be positive.");

        switch (method)
        {
            case PaymentMethod.Card:
                // 2.9% + 30¢ — the classic card-processing fee.
                decimal cardFee = Math.Round(request.Amount * 0.029m + 0.30m, 2);
                return PaymentResult.Ok($"CARD-{request.DestinationAccount}", cardFee, "Card payment captured.");

            case PaymentMethod.Ach:
                // Flat 25¢ per ACH transfer.
                return PaymentResult.Ok($"ACH-{request.DestinationAccount}", 0.25m, "ACH transfer submitted.");

            case PaymentMethod.Wire:
                // Flat $15 wire fee.
                return PaymentResult.Ok($"WIRE-{request.DestinationAccount}", 15.00m, "Wire instruction sent.");

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(method), method, "Unsupported payment method.");
        }
    }
}
