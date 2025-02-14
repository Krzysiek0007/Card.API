using Card.API.Models;
using Card.API.Strategies;

namespace Card.API.Services
{
    public class CardActionService : ICardActionService
    {
        private readonly ICardService _cardService;
        private readonly Dictionary<CardType, ICardActionStrategy> _strategies;

        public CardActionService(ICardService cardService, IEnumerable<ICardActionStrategy> strategies)
        {
            _cardService = cardService;
            _strategies = strategies.ToDictionary(s => s.CardType);
        }

        public async Task<IEnumerable<CardAction>> GetAllowedActionsAsync(string userId, string cardNumber)
        {
            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);

            if (cardDetails == null)
                throw new Exception("Nie znaleziono karty.");

            if (_strategies.TryGetValue(cardDetails.CardType, out var strategy))
            {
                return strategy.GetAllowedActions(cardDetails);
            }

            throw new Exception("Nieobsługiwany typ karty.");
        }
    }
}