namespace DesignPatternsBootcamp.Creational.AbstractFactory;

/// <summary>
/// The <b>Abstract Factory</b>. One method per product family. A client that holds an
/// <see cref="IMarketFactory"/> is guaranteed to receive a fee schedule and a settlement policy
/// that belong to the <i>same</i> market — it is impossible to accidentally pair US fees with
/// EU settlement, because a single factory produces both.
/// </summary>
public interface IMarketFactory
{
    IFeeSchedule CreateFeeSchedule();
    ISettlementPolicy CreateSettlementPolicy();
}

// ---- Concrete Factories: each assembles one consistent market family. -----------------------

public sealed class UsMarketFactory : IMarketFactory
{
    public IFeeSchedule CreateFeeSchedule() =>
        throw new NotImplementedException("TODO(student): return the US fee schedule.");

    public ISettlementPolicy CreateSettlementPolicy() =>
        throw new NotImplementedException("TODO(student): return the US settlement policy.");
}

public sealed class EuMarketFactory : IMarketFactory
{
    public IFeeSchedule CreateFeeSchedule() =>
        throw new NotImplementedException("TODO(student): return the EU fee schedule.");

    public ISettlementPolicy CreateSettlementPolicy() =>
        throw new NotImplementedException("TODO(student): return the EU settlement policy.");
}
