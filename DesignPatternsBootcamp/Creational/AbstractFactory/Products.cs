namespace DesignPatternsBootcamp.Creational.AbstractFactory;

// ---------------------------------------------------------------------------------------------
//  The two product families. Every market must supply BOTH a fee schedule and a settlement
//  policy, and they must belong to the SAME market (same currency, same conventions).
// ---------------------------------------------------------------------------------------------

/// <summary>Abstract Product A: how much commission a market charges.</summary>
public interface IFeeSchedule
{
    Money Commission(decimal notional);
}

/// <summary>Abstract Product B: how a market settles a trade.</summary>
public interface ISettlementPolicy
{
    int SettlementDays { get; }
    string Currency { get; }
}

// ---- Concrete Product A ---------------------------------------------------------------------
//  These fee schedules are stubbed. Port the maths from LegacyTradeDesk.Commission(...).

public sealed class UsFeeSchedule : IFeeSchedule
{
    public Money Commission(decimal notional) =>
        throw new NotImplementedException(
            "TODO(student): port the \"US\" branch of LegacyTradeDesk.Commission — max($1, 0.1% of notional), in USD.");
}

public sealed class EuFeeSchedule : IFeeSchedule
{
    public Money Commission(decimal notional) =>
        throw new NotImplementedException(
            "TODO(student): port the \"EU\" branch of LegacyTradeDesk.Commission — €1.20 + 0.2% of notional, in EUR.");
}

// ---- Concrete Product B (provided complete) -------------------------------------------------
//  Settlement conventions are just data; they are given so you can focus on assembling families.

public sealed class UsSettlementPolicy : ISettlementPolicy
{
    public int SettlementDays => 1;      // T+1
    public string Currency => "USD";
}

public sealed class EuSettlementPolicy : ISettlementPolicy
{
    public int SettlementDays => 2;      // T+2
    public string Currency => "EUR";
}
