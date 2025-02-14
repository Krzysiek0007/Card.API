using Card.API.Models;

namespace Card.API.Strategies
{
    public interface ICardStatusActionStrategy
    {
        CardStatus CardStatus { get; }
        IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails);
    }
}
