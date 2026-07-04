namespace DesignPatternsBootcamp.Structural.Decorator;

/// <summary>
/// The <b>Component</b>: anything that can quote a running <see cref="Total"/> and describe how it
/// was built up. Charges are added by wrapping one of these in another one of these.
/// </summary>
public interface IPricedTrade
{
    decimal Total();
    string Description { get; }
}

/// <summary>The <b>Concrete Component</b>: the bare trade, priced at its notional.</summary>
public sealed class BaseTrade : IPricedTrade
{
    private readonly decimal _notional;

    public BaseTrade(string symbol, decimal notional)
    {
        Description = symbol;
        _notional = notional;
    }

    public string Description { get; }
    public decimal Total() => _notional;
}

/// <summary>
/// The <b>base Decorator</b>: it <i>is</i> an <see cref="IPricedTrade"/> and <i>wraps</i> one
/// (<see cref="Inner"/>). Concrete decorators delegate to <c>Inner</c> and add their own charge on
/// top — so any charge can wrap any priced trade, including another decorator. Stack freely.
/// </summary>
public abstract class TradeChargeDecorator : IPricedTrade
{
    protected readonly IPricedTrade Inner;

    protected TradeChargeDecorator(IPricedTrade inner) => Inner = inner;

    public abstract decimal Total();
    public abstract string Description { get; }
}

// ---------------------------------------------------------------------------------------------
//  Concrete Decorators. Each adds ONE charge. Implement Total() and Description for all three.
// ---------------------------------------------------------------------------------------------

/// <summary>Percentage commission on whatever it wraps.</summary>
public sealed class CommissionDecorator : TradeChargeDecorator
{
    private readonly decimal _rate;

    public CommissionDecorator(IPricedTrade inner, decimal rate) : base(inner) => _rate = rate;

    public override decimal Total() =>
        throw new NotImplementedException("TODO(student): Inner.Total() plus commission (Inner.Total() × _rate).");

    public override string Description =>
        throw new NotImplementedException("TODO(student): \"<inner description> + commission\".");
}

/// <summary>A flat per-trade exchange fee.</summary>
public sealed class ExchangeFeeDecorator : TradeChargeDecorator
{
    private readonly decimal _fee;

    public ExchangeFeeDecorator(IPricedTrade inner, decimal fee) : base(inner) => _fee = fee;

    public override decimal Total() =>
        throw new NotImplementedException("TODO(student): Inner.Total() plus the flat _fee.");

    public override string Description =>
        throw new NotImplementedException("TODO(student): \"<inner description> + exchange fee\".");
}

/// <summary>Percentage tax on whatever it wraps (so it taxes any charges already added underneath).</summary>
public sealed class TaxDecorator : TradeChargeDecorator
{
    private readonly decimal _rate;

    public TaxDecorator(IPricedTrade inner, decimal rate) : base(inner) => _rate = rate;

    public override decimal Total() =>
        throw new NotImplementedException("TODO(student): Inner.Total() × (1 + _rate).");

    public override string Description =>
        throw new NotImplementedException("TODO(student): \"<inner description> + tax\".");
}
