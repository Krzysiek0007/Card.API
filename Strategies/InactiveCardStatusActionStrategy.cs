using Card.API.Models;

namespace Card.API.Strategies
{
    public class InactiveCardStatusActionStrategy : ICardStatusActionStrategy
    {
        public CardStatus CardStatus => CardStatus.Inactive;

        public IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var actions = new List<CardAction>();

            actions.AddRange(new[]
            {
                CardAction.ACTION2,
                CardAction.ACTION3,
                CardAction.ACTION4,
                CardAction.ACTION5,
                CardAction.ACTION8,
                CardAction.ACTION9,
                CardAction.ACTION10,
                CardAction.ACTION11,
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
