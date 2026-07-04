using DesignPatternsBootcamp.Structural.Adapter.ThirdParty;

namespace DesignPatternsBootcamp.Structural.Adapter;

// ---------------------------------------------------------------------------------------------
//  The Adapters. Each wraps one incompatible SDK (the "Adaptee") and makes it satisfy our
//  IPaymentGateway (the "Target"), translating arguments in and results out.
// ---------------------------------------------------------------------------------------------

public sealed class AcmePayAdapter : IPaymentGateway
{
    private readonly AcmePayClient _client;

    public AcmePayAdapter(AcmePayClient client) => _client = client;

    public GatewayResult Charge(decimal amount, string currency, string account) =>
        throw new NotImplementedException(
            "TODO(student): build an AcmeRequest, call _client.SubmitPayment, and map AcmeResponse " +
            "back into a GatewayResult (Approved = Ok, Reference = AcmeTxnId).");
}

public sealed class GlobalPayAdapter : IPaymentGateway
{
    private readonly GlobalPayApi _api;

    public GlobalPayAdapter(GlobalPayApi api) => _api = api;

    public GatewayResult Charge(decimal amount, string currency, string account) =>
        throw new NotImplementedException(
            "TODO(student): convert dollars → integer cents, call _api.Pay(..., out var code), and " +
            "map the bool + out-param into a GatewayResult (Reference = code).");
}
