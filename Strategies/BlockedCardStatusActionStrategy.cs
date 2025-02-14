using Card.API.Models;

namespace Card.API.Strategies
{
    public class BlockedCardStatusActionStrategy : ICardStatusActionStrategy
    {
        public CardStatus CardStatus => CardStatus.Blocked;

        public IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var actions = new List<CardAction>();

            actions.AddRange(new[]
            {
                CardAction.ACTION3,
                CardAction.ACTION4,
                CardAction.ACTION5,
                CardAction.ACTION8,
                CardAction.ACTION9
            });

            if (cardDetails.IsPinSet)
            {
                actions.Add(CardAction.ACTION6);
                actions.Add(CardAction.ACTION7);
            }

            return actions;
        }
    }
}