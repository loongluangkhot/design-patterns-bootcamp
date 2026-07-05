namespace DesignPatternsBootcamp.Behavioral.TemplateMethod;

/// <summary>A trade to appear on a report. (Provided data type; you build the report template + subclasses.)</summary>
public record Trade(string Symbol, int Quantity, decimal Price);
