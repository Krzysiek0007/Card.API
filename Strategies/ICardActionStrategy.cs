using Card.API.Models;

namespace Card.API.Strategies
{
    public interface ICardActionStrategy
    {
        CardType CardType { get; }
        IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails);
    }
}
