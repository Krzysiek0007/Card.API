using Card.API.Models;

namespace Card.API.Strategies
{
    public class RestrictedCardStatusActionStrategy : ICardStatusActionStrategy
    {
        public CardStatus CardStatus => CardStatus.Restricted;

        public IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var actions = new List<CardAction>();
            
            actions.AddRange(new[]
            {
                CardAction.ACTION3,
                CardAction.ACTION4,
                CardAction.ACTION5,
                CardAction.ACTION9
            });

            return actions;
        }
    }
}
