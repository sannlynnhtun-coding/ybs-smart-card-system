using Microsoft.AspNetCore.Mvc;
using YbsSmartCardSystem.Domain.Features.Card;
using YbsSmartCardSystem.Domain.Features.Card.Models;

namespace YbsSmartCardSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : BaseController
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public IActionResult CardList([FromQuery] CardListRequestModel request)
        {
            var result = _cardService.GetList(request);
            return Execute(result);
        }

        [HttpGet("{cardId}")]
        public IActionResult CardDetail(int cardId)
        {
            var result = _cardService.GetById(cardId);
            return Execute(result);
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] CardCreateRequestModel request)
        {
            var result = _cardService.Create(request);
            return Execute(result);
        }
    }
}
