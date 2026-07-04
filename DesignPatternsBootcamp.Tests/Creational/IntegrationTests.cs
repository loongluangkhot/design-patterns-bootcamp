using DesignPatternsBootcamp.Creational.AbstractFactory;
using DesignPatternsBootcamp.Creational.Builder;
using DesignPatternsBootcamp.Creational.Integration;

namespace DesignPatternsBootcamp.Tests.Creational;

/// <summary>
/// Week 1 capstone. These require Day 1b (Abstract Factory) AND Day 2 (Builder) to be implemented,
/// plus TradeDesk.BookLimitBuy — they prove the patterns compose.
/// </summary>
public class IntegrationTests
{
    [Fact]
    public void Booking_a_us_limit_buy_builds_the_order_and_prices_it()
    {
        var desk = new TradeDesk(new UsMarketFactory());

        TradeTicket ticket = desk.BookLimitBuy("AAPL", quantity: 100, limitPrice: 150m, account: "ACC-1");

        // Builder produced a validated, immutable limit order:
        Assert.Equal(Side.Buy, ticket.Order.Side);
        Assert.Equal(OrderType.Limit, ticket.Order.OrderType);
        Assert.Equal(100, ticket.Order.Quantity);
        Assert.Equal(150m, ticket.Order.LimitPrice);
        Assert.Equal("ACC-1", ticket.Order.Account);

        // Abstract Factory priced it in the US market: notional 15,000 → max($1, 0.1%) = $15.00.
        Assert.Equal(new Money(15.00m, "USD"), ticket.Commission);
        Assert.Equal(1, ticket.SettlementDays);
        Assert.Equal("USD", ticket.SettlementCurrency);
    }

    [Fact]
    public void Booking_an_eu_limit_buy_uses_eu_fees_and_settlement()
    {
        var desk = new TradeDesk(new EuMarketFactory());

        TradeTicket ticket = desk.BookLimitBuy("SAP", quantity: 100, limitPrice: 120m, account: "ACC-2");

        // notional 12,000 → €1.20 + 0.2% = €25.20, settling T+2 in EUR.
        Assert.Equal(new Money(25.20m, "EUR"), ticket.Commission);
        Assert.Equal(2, ticket.SettlementDays);
        Assert.Equal("EUR", ticket.SettlementCurrency);
    }

    [Fact]
    public void A_desk_prices_and_settles_every_trade_in_one_consistent_market()
    {
        var desk = new TradeDesk(new UsMarketFactory());

        TradeTicket ticket = desk.BookLimitBuy("MSFT", 10, 400m, "ACC-3");

        // Because commission and settlement both came from the one market factory, they agree.
        Assert.Equal(ticket.Commission.Currency, ticket.SettlementCurrency);
    }
}
