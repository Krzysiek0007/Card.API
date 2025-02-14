using Card.API.Models;

namespace Card.API.Services
{
    public interface ICardActionService
    {
        Task<IEnumerable<CardAction>> GetAllowedActionsAsync(string userId, string cardNumber);
    }
}
