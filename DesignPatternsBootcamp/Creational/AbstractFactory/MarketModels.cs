namespace DesignPatternsBootcamp.Creational.AbstractFactory;

/// <summary>An amount of money in a specific currency.</summary>
public record Money(decimal Amount, string Currency);

/// <summary>What we tell the desk once a trade has been booked in a given market.</summary>
public record BookingSummary(Money Commission, int SettlementDays, string SettlementCurrency);
