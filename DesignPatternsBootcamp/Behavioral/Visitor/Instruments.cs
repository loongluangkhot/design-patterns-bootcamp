namespace DesignPatternsBootcamp.Behavioral.Visitor;

/// <summary>
/// The <b>Element</b>: an instrument that can <see cref="Accept"/> a visitor. <c>Accept</c> is the
/// double-dispatch trick — the element calls back the visitor method for its OWN concrete type, so
/// the visitor runs the right code without any type checks.
/// </summary>
public interface IInstrument
{
    T Accept<T>(IInstrumentVisitor<T> visitor);
}

/// <summary>
/// The <b>Visitor</b>: one operation, with a method per instrument type. Add a new operation by
/// writing a new visitor — the instruments don't change.
/// </summary>
public interface IInstrumentVisitor<T>
{
    T VisitEquity(Equity equity);
    T VisitBond(Bond bond);
    T VisitOption(Option option);
}

// ---- Concrete Elements (provided). Each Accept dispatches to its own Visit method. -----------

public sealed class Equity : IInstrument
{
    public string Symbol { get; }
    public int Shares { get; }
    public decimal Price { get; }

    public Equity(string symbol, int shares, decimal price)
    {
        Symbol = symbol;
        Shares = shares;
        Price = price;
    }

    public T Accept<T>(IInstrumentVisitor<T> visitor) => visitor.VisitEquity(this);
}

public sealed class Bond : IInstrument
{
    public string Issuer { get; }
    public decimal FaceValue { get; }
    public decimal CouponRate { get; }

    public Bond(string issuer, decimal faceValue, decimal couponRate)
    {
        Issuer = issuer;
        FaceValue = faceValue;
        CouponRate = couponRate;
    }

    public T Accept<T>(IInstrumentVisitor<T> visitor) => visitor.VisitBond(this);
}

public sealed class Option : IInstrument
{
    public string Underlying { get; }
    public int Contracts { get; }
    public decimal Premium { get; }

    public Option(string underlying, int contracts, decimal premium)
    {
        Underlying = underlying;
        Contracts = contracts;
        Premium = premium;
    }

    public T Accept<T>(IInstrumentVisitor<T> visitor) => visitor.VisitOption(this);
}
