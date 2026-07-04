namespace DesignPatternsBootcamp.Structural.Adapter;

/// <summary>The normalised result our application understands, whatever provider produced it.</summary>
public record GatewayResult(bool Approved, string Reference, decimal AmountCharged, string Currency);

/// <summary>
/// The <b>Target</b> interface — the single, uniform way our app wants to charge money. Every
/// payment provider, no matter how weird its own API is, must be made to look like this.
/// </summary>
public interface IPaymentGateway
{
    GatewayResult Charge(decimal amount, string currency, string account);
}
