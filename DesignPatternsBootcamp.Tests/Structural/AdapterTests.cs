// ============================================================================
//  STEP 1 OF THIS KATA — UNCOMMENT THESE TESTS.
//  They ship commented out so the rest of the test project still builds before
//  you start this kata. To begin: delete the "/*" on the line below AND the
//  "*/" on the very last line, then run this kata's tests (its README has the
//  filter + the target API). It will NOT compile at first — that is expected:
//  each "type or namespace ... could not be found" error is a type you must
//  create yourself. Build until it compiles, then turns green.
// ============================================================================
/*
using DesignPatternsBootcamp.Structural.Adapter;
using DesignPatternsBootcamp.Structural.Adapter.Legacy;
using DesignPatternsBootcamp.Structural.Adapter.ThirdParty;

namespace DesignPatternsBootcamp.Tests.Structural;

public class AdapterTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. The client is welded to each vendor's private API.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_checkout_routes_to_the_acme_sdk()
    {
        Assert.Equal("ACME-acct-1", new LegacyCheckout().Pay("acme", 100m, "USD", "acct-1"));
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the two adapters are implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Acme_adapter_exposes_the_sdk_as_an_IPaymentGateway()
    {
        IPaymentGateway gateway = new AcmePayAdapter(new AcmePayClient());

        GatewayResult result = gateway.Charge(100m, "USD", "acct-1");

        Assert.True(result.Approved);
        Assert.Equal("ACME-acct-1", result.Reference);
        Assert.Equal(100m, result.AmountCharged);
        Assert.Equal("USD", result.Currency);
    }

    [Fact]
    public void Global_adapter_converts_dollars_to_integer_cents()
    {
        var api = new GlobalPayApi();
        IPaymentGateway gateway = new GlobalPayAdapter(api);

        GatewayResult result = gateway.Charge(123.45m, "USD", "acct-2");

        Assert.True(result.Approved);
        Assert.Equal("GP-acct-2", result.Reference);
        Assert.Equal(12345L, api.LastAmountInCents); // the adapter did the $ → ¢ translation
    }

    [Fact]
    public void Both_providers_can_be_treated_uniformly_through_the_target_interface()
    {
        var gateways = new List<IPaymentGateway>
        {
            new AcmePayAdapter(new AcmePayClient()),
            new GlobalPayAdapter(new GlobalPayApi()),
        };

        Assert.All(gateways, gw => Assert.True(gw.Charge(50m, "USD", "acct-9").Approved));
    }
}
*/
