namespace DesignPatternsBootcamp.Structural.Adapter.ThirdParty;

// =============================================================================================
//  SIMULATED THIRD-PARTY SDKs — pretend these come from NuGet packages you cannot change.
//  Their shapes are deliberately incompatible with each other and with our IPaymentGateway.
//  This is the reality Adapter exists to tame.
// =============================================================================================

// ---- "AcmePay": request/response objects, amounts in whole dollars, boolean `Ok`. -----------

public sealed class AcmeRequest
{
    public decimal DollarAmount { get; set; }
    public string CurrencyCode { get; set; } = "";
    public string AccountRef { get; set; } = "";
}

public sealed class AcmeResponse
{
    public bool Ok { get; init; }
    public string AcmeTxnId { get; init; } = "";
}

public sealed class AcmePayClient
{
    public AcmeResponse SubmitPayment(AcmeRequest request) =>
        new() { Ok = true, AcmeTxnId = $"ACME-{request.AccountRef}" };
}

// ---- "GlobalPay": amounts in integer CENTS, an `out` parameter, a bool return. --------------

public sealed class GlobalPayApi
{
    /// <summary>Records what it was last asked to charge, so tests can prove unit conversion.</summary>
    public long? LastAmountInCents { get; private set; }

    public bool Pay(long amountInCents, string iso4217, string account, out string confirmationCode)
    {
        LastAmountInCents = amountInCents;
        confirmationCode = $"GP-{account}";
        return true;
    }
}
