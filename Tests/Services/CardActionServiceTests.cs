using Card.API.Models;
using Card.API.Services;
using Card.API.Strategies;
using Moq;
using Xunit;

namespace Card.API.Tests.Services
{
    public class CardActionServiceTests
    {
        private readonly Mock<ICardService> _mockCardService;
        private readonly Mock<ICardActionStrategy> _mockStrategy;
        private readonly CardActionService _cardActionService;

        public CardActionServiceTests()
        {
            _mockCardService = new Mock<ICardService>();
            _mockStrategy = new Mock<ICardActionStrategy>();
            var strategies = new List<ICardActionStrategy> { _mockStrategy.Object };
            _cardActionService = new CardActionService(_mockCardService.Object, strategies);
        }

        [Fact]
        public async Task GetAllowedActionsAsync_CardDetailsNotFound_ThrowsException()
        {
            // Arrange
            _mockCardService.Setup(s => s.GetCardDetails(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((CardDetails?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _cardActionService.GetAllowedActionsAsync("User1", "card1"));
            Assert.Equal("Nie znaleziono karty.", exception.Message);
        }

        [Fact]
        public async Task GetAllowedActionsAsync_UnsupportedCardType_ThrowsException()
        {
            // Arrange
            var cardDetails = new CardDetails("card1", (CardType)13, CardStatus.Active, true);

            _mockCardService.Setup(s => s.GetCardDetails(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(cardDetails);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _cardActionService.GetAllowedActionsAsync("user1", "card1"));
            Assert.Equal("Nieobs³ugiwany typ karty.", exception.Message);
        }

        [Fact]
        public async Task GetAllowedActionsAsync_ValidCardDetails_ReturnsAllowedActions()
        {
            // Arrange
            var cardDetails = new CardDetails("card1", CardType.Prepaid, CardStatus.Active, true);

            var allowedActions = new List<CardAction> { CardAction.ACTION1, CardAction.ACTION2 };
            _mockCardService.Setup(s => s.GetCardDetails(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(cardDetails);
            _mockStrategy.Setup(s => s.CardType).Returns(CardType.Prepaid);
            _mockStrategy.Setup(s => s.GetAllowedActions(It.IsAny<CardDetails>()))
                .Returns(allowedActions);

            // Act
            var result = await _cardActionService.GetAllowedActionsAsync("user1", "card1");

            // Assert
            Assert.Equal(allowedActions, result);
        }
    }
}