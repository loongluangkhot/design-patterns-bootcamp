namespace DesignPatternsBootcamp.Behavioral.Strategy;

/// <summary>
/// The <b>Context</b> (provided): it holds a strategy and delegates the fee calculation to it. It
/// can be handed a different strategy at any time via <see cref="UseStrategy"/> — no conditionals,
/// no knowledge of how any fee is computed.
/// </summary>
public sealed class FeeCalculator
{
    private IFeeStrategy _strategy;

    public FeeCalculator(IFeeStrategy strategy) => _strategy = strategy;

    public void UseStrategy(IFeeStrategy strategy) => _strategy = strategy;

    public decimal FeeFor(decimal notional) => _strategy.Calculate(notional);
}
