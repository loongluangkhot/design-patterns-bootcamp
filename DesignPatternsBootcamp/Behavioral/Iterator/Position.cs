namespace DesignPatternsBootcamp.Behavioral.Iterator;

/// <summary>A single position. Positive quantity = long, negative = short. (Provided data type; you build the Book that iterates them.)</summary>
public record Position(string Symbol, int Quantity);
