namespace DesignPatternsBootcamp.Behavioral.Interpreter;

/// <summary>The <b>Context</b> a rule is evaluated against — one trade's facts.</summary>
public record TradeContext(string Symbol, decimal Notional);

/// <summary>
/// The <b>Abstract Expression</b>. Every node in a rule — a leaf like "symbol is AAPL" or a branch
/// like "A AND B" — is one of these and knows how to <see cref="Interpret"/> itself against a context.
/// Rules become <b>trees of objects</b> you can assemble at runtime instead of hard-coded <c>if</c>s.
/// </summary>
public interface IRuleExpression
{
    bool Interpret(TradeContext context);
}

// ---- Terminal Expressions (leaves): they read the context directly. -------------------------

public sealed class SymbolIs : IRuleExpression
{
    private readonly string _symbol;

    public SymbolIs(string symbol) => _symbol = symbol;

    public bool Interpret(TradeContext context) =>
        throw new NotImplementedException("TODO(student): true when context.Symbol equals _symbol.");
}

public sealed class NotionalAtLeast : IRuleExpression
{
    private readonly decimal _threshold;

    public NotionalAtLeast(decimal threshold) => _threshold = threshold;

    public bool Interpret(TradeContext context) =>
        throw new NotImplementedException("TODO(student): true when context.Notional >= _threshold.");
}

// ---- Nonterminal Expressions (branches): they combine other expressions. --------------------

public sealed class And : IRuleExpression
{
    private readonly IRuleExpression _left;
    private readonly IRuleExpression _right;

    public And(IRuleExpression left, IRuleExpression right)
    {
        _left = left;
        _right = right;
    }

    public bool Interpret(TradeContext context) =>
        throw new NotImplementedException("TODO(student): both _left and _right must interpret to true.");
}

public sealed class Or : IRuleExpression
{
    private readonly IRuleExpression _left;
    private readonly IRuleExpression _right;

    public Or(IRuleExpression left, IRuleExpression right)
    {
        _left = left;
        _right = right;
    }

    public bool Interpret(TradeContext context) =>
        throw new NotImplementedException("TODO(student): either _left or _right interprets to true.");
}

public sealed class Not : IRuleExpression
{
    private readonly IRuleExpression _inner;

    public Not(IRuleExpression inner) => _inner = inner;

    public bool Interpret(TradeContext context) =>
        throw new NotImplementedException("TODO(student): the inverse of _inner's interpretation.");
}
