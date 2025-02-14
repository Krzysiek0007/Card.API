using Card.API.Models;

namespace Card.API.Strategies
{
    public class DebitCardActionStrategy : ICardActionStrategy
    {
        public CardType CardType => CardType.Debit;

        private readonly Dictionary<CardStatus, ICardStatusActionStrategy> _statusStrategies;

        public DebitCardActionStrategy(IEnumerable<ICardStatusActionStrategy> statusStrategies)
        {
            _statusStrategies = statusStrategies.ToDictionary(s => s.CardStatus);
        }

        public IEnumerable<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var actions = new List<CardAction>();

            if (_statusStrategies.TryGetValue(cardDetails.CardStatus, out var strategy))
            {
                actions.AddRange(strategy.GetAllowedActions(cardDetails));
            }

            actions.Remove(CardAction.ACTION5);

            return actions;
        }
    }
}