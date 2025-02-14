using Card.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Card.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardActionsController : ControllerBase
    {
        private readonly ICardActionService _cardActionService;

        public CardActionsController(ICardActionService cardActionService)
        {
            _cardActionService = cardActionService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllowedActions([FromBody] CardRequest request)
        {
            try
            {
                var actions = await _cardActionService.GetAllowedActionsAsync(request.UserId, request.CardNumber);
                return Ok(new { AllowedActions = actions });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    public record CardRequest(string UserId, string CardNumber);
}
