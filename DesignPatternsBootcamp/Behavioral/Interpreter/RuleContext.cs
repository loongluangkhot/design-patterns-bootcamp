namespace DesignPatternsBootcamp.Behavioral.Interpreter;

/// <summary>The context a rule is evaluated against — one trade's facts. (Provided data type; you build the expression classes.)</summary>
public record TradeContext(string Symbol, decimal Notional);
