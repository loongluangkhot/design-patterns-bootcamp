using DesignPatternsBootcamp.Structural.Adapter.ThirdParty;

namespace DesignPatternsBootcamp.Structural.Adapter.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the checkout talks to each provider's SDK directly, behind a <c>switch</c>.
///
/// The client is now welded to every vendor's private API shape: it knows AcmePay uses request
/// objects and AcmePay's <c>Ok</c>/<c>AcmeTxnId</c>, and that GlobalPay wants cents and an
/// <c>out</c> parameter. That knowledge — including the dollars→cents conversion — leaks into
/// business code and is duplicated everywhere a payment is taken.
///
/// Smells: adding a provider edits this switch; providers cannot be treated uniformly (no common
/// type); translation logic is scattered. Adapter gives every provider one shape so the client
/// stops caring which one it is.
/// </summary>
public sealed class LegacyCheckout
{
    private readonly AcmePayClient _acme = new();
    private readonly GlobalPayApi _global = new();

    public string Pay(string provider, decimal amount, string currency, string account)
    {
        switch (provider)
        {
            case "acme":
                AcmeResponse r = _acme.SubmitPayment(new AcmeRequest
                {
                    DollarAmount = amount,
                    CurrencyCode = currency,
                    AccountRef = account,
                });
                return r.Ok ? r.AcmeTxnId : "DECLINED";

            case "global":
                long cents = (long)Math.Round(amount * 100m);
                bool ok = _global.Pay(cents, currency, account, out string code);
                return ok ? code : "DECLINED";

            default:
                throw new ArgumentOutOfRangeException(nameof(provider), provider, "Unknown provider.");
        }
    }
}
