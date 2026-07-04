using DesignPatternsBootcamp.Creational.FactoryMethod;
using DesignPatternsBootcamp.Creational.FactoryMethod.Legacy;

namespace DesignPatternsBootcamp.Tests.Creational;

public class FactoryMethodTests
{
    private static PaymentRequest SampleRequest(decimal amount = 100m) =>
        new(amount, "USD", "acct-source", "acct-dest");

    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — these already PASS. They pin down the behaviour your refactor must keep.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_card_fee_is_2_9_percent_plus_30_cents()
    {
        var result = new LegacyPaymentGateway().Pay(PaymentMethod.Card, SampleRequest(100m));

        Assert.True(result.Success);
        Assert.Equal(3.20m, result.Fee); // 100 * 0.029 + 0.30
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — these start RED and turn GREEN as you implement the Factory Method.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Card_service_creates_a_card_processor()
    {
        Assert.IsType<CardPaymentProcessor>(new CardPaymentService().CreateProcessor());
    }

    [Fact]
    public void Ach_service_creates_an_ach_processor()
    {
        Assert.IsType<AchPaymentProcessor>(new AchPaymentService().CreateProcessor());
    }

    [Fact]
    public void Wire_service_creates_a_wire_processor()
    {
        Assert.IsType<WirePaymentProcessor>(new WirePaymentService().CreateProcessor());
    }

    [Fact]
    public void Card_payment_matches_the_legacy_fee_and_confirmation()
    {
        var result = new CardPaymentService().Pay(SampleRequest(100m));

        Assert.True(result.Success);
        Assert.StartsWith("CARD-", result.Confirmation);
        Assert.Equal(3.20m, result.Fee);
    }

    [Fact]
    public void Ach_payment_is_a_flat_25_cents()
    {
        var result = new AchPaymentService().Pay(SampleRequest(5000m));

        Assert.True(result.Success);
        Assert.StartsWith("ACH-", result.Confirmation);
        Assert.Equal(0.25m, result.Fee);
    }

    [Fact]
    public void Wire_payment_is_a_flat_15_dollars()
    {
        var result = new WirePaymentService().Pay(SampleRequest(5000m));

        Assert.True(result.Success);
        Assert.StartsWith("WIRE-", result.Confirmation);
        Assert.Equal(15.00m, result.Fee);
    }

    [Fact]
    public void Shared_validation_lives_in_the_base_creator_for_every_rail()
    {
        // The amount check is written ONCE in PaymentService.Pay, not copied into each processor.
        Assert.False(new CardPaymentService().Pay(SampleRequest(0m)).Success);
        Assert.False(new AchPaymentService().Pay(SampleRequest(-1m)).Success);
        Assert.False(new WirePaymentService().Pay(SampleRequest(-0.01m)).Success);
    }
}
