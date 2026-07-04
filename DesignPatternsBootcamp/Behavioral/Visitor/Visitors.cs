namespace DesignPatternsBootcamp.Behavioral.Visitor;

// ---------------------------------------------------------------------------------------------
//  Concrete Visitors. Each is ONE operation over all instrument types. Implement the three Visit
//  methods for each — the per-type logic that used to live in a big type switch now lives here,
//  grouped by operation.
// ---------------------------------------------------------------------------------------------

/// <summary>Values each instrument at its current market value.</summary>
public sealed class MarketValueVisitor : IInstrumentVisitor<decimal>
{
    public decimal VisitEquity(Equity equity) =>
        throw new NotImplementedException("TODO(student): Shares × Price.");

    public decimal VisitBond(Bond bond) =>
        throw new NotImplementedException("TODO(student): FaceValue.");

    public decimal VisitOption(Option option) =>
        throw new NotImplementedException("TODO(student): Contracts × Premium × 100 (contract multiplier).");
}

/// <summary>Estimates tax with a type-specific rule per instrument.</summary>
public sealed class TaxVisitor : IInstrumentVisitor<decimal>
{
    public decimal VisitEquity(Equity equity) =>
        throw new NotImplementedException("TODO(student): 15% of market value → Shares × Price × 0.15.");

    public decimal VisitBond(Bond bond) =>
        throw new NotImplementedException("TODO(student): 25% of coupon income → FaceValue × CouponRate × 0.25.");

    public decimal VisitOption(Option option) =>
        throw new NotImplementedException("TODO(student): 20% of market value → Contracts × Premium × 100 × 0.20.");
}
