using Card.API.Models;

namespace Card.API.Strategies
{
    public class OrderedCardStatusActionStrategy : ICardStatusActionStrategy
    {
        public CardStatus CardStatus => CardStatus.Ordered;

        public IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var actions = new List<CardAction>();

            actions.AddRange(new[]
            {
                CardAction.ACTION3,
                CardAction.ACTION4,
                CardAction.ACTION5,
                CardAction.ACTION8,
                CardAction.ACTION9,
                CardAction.ACTION10,
                CardAction.ACTION12,
                CardAction.ACTION13
            });

            if (cardDetails.IsPinSet)
            {
                actions.Add(CardAction.ACTION6);
            }

            if (!cardDetails.IsPinSet)
            {
                actions.Add(CardAction.ACTION7);
            }

            return actions;
        }
    }
}
