namespace DesignPatternsBootcamp.Creational.FactoryMethod;

/// <summary>
/// The <b>Creator</b>. It owns the workflow that is identical for every rail (here: validation),
/// and defers the one thing that varies — <i>which</i> processor to build — to subclasses via the
/// abstract <see cref="CreateProcessor"/> <b>factory method</b>.
///
/// Notice there is NO <c>switch</c> on <see cref="PaymentMethod"/> anywhere. Adding a new rail
/// means adding a new subclass, never editing this class. That is the whole point.
/// </summary>
public abstract class PaymentService
{
    /// <summary>The factory method: each subclass decides which concrete processor to create.</summary>
    public abstract IPaymentProcessor CreateProcessor();

    /// <summary>
    /// The template operation shared by all rails. It relies on the product returned by the
    /// factory method without ever knowing the concrete type.
    /// </summary>
    public PaymentResult Pay(PaymentRequest request)
    {
        if (request.Amount <= 0)
            return PaymentResult.Rejected("Amount must be positive.");

        IPaymentProcessor processor = CreateProcessor();
        return processor.Process(request);
    }
}

// ---------------------------------------------------------------------------------------------
//  Concrete Creators. Each overrides the factory method to build its rail's processor.
// ---------------------------------------------------------------------------------------------

public sealed class CardPaymentService : PaymentService
{
    public override IPaymentProcessor CreateProcessor() =>
        throw new NotImplementedException("TODO(student): return a new CardPaymentProcessor.");
}

public sealed class AchPaymentService : PaymentService
{
    public override IPaymentProcessor CreateProcessor() =>
        throw new NotImplementedException("TODO(student): return a new AchPaymentProcessor.");
}

public sealed class WirePaymentService : PaymentService
{
    public override IPaymentProcessor CreateProcessor() =>
        throw new NotImplementedException("TODO(student): return a new WirePaymentProcessor.");
}
