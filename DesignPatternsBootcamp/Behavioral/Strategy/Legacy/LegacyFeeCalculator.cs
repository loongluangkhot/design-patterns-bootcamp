namespace DesignPatternsBootcamp.Behavioral.Strategy.Legacy;

/// <summary>
/// THE "BEFORE" CODE — every fee algorithm is crammed into one method behind a <c>switch</c> on a
/// fee-type string, with a single <c>parameter</c> that means something different in each branch.
///
/// Smells: the algorithms are tangled together; you can't hold "the fee policy" as a value or swap
/// it at runtime; the overloaded <c>parameter</c> is a trap; and adding a fee type edits this method.
/// The Strategy pattern turns each algorithm into its own object behind a common interface.
/// </summary>
public sealed class LegacyFeeCalculator
{
    public decimal Calculate(string feeType, decimal notional, decimal parameter)
    {
        switch (feeType)
        {
            case "flat":
                return parameter;
            case "percentage":
                return Math.Round(notional * parameter, 2);
            case "tiered":
                return notional < 100_000m
                    ? Math.Round(notional * 0.001m, 2)
                    : Math.Round(notional * 0.0005m, 2);
            default:
                throw new ArgumentOutOfRangeException(nameof(feeType), feeType, "Unknown fee type.");
        }
    }
}
