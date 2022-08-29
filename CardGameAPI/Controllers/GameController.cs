using CardGameAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameSession _gameSession;

        public GameController(IGameSession gameSession)
        {
            _gameSession = gameSession;
        }

        [HttpGet("Start")]
        public async Task<IActionResult> Start()
        {
            if (_gameSession.StartGame())
            {
                var card = _gameSession.Deck.DealOne();
                _gameSession.CurrentCard = card;
                return Ok(_gameSession);
            }
            return Ok(_gameSession);
        }

        [HttpGet("End")]
        public async Task<IActionResult> End()
        {
            if (_gameSession.EndGame()) return Ok(_gameSession);
            return BadRequest("No game was Active.");

        }

        [HttpGet("Shuffle")]
        public async Task<IActionResult> Shuffle()
        {
            _gameSession.Deck.Shuffle();
            return Ok("Shuffle");
        }

        [HttpGet("Deal")]
        public async Task<IActionResult> Deal(bool? higher)
        {
            var card = _gameSession.Deck.DealOne();
            if (higher is null)
            {

                if (card.Value == _gameSession.CurrentCard.Value)
                {
                    _gameSession.CurrentCard = card;
                    _gameSession.score += 5;
                    return Ok(_gameSession);
                }

            }
            else if (higher is not null && higher is true)
            {
                if (card.Value > _gameSession.CurrentCard.Value)
                {
                    _gameSession.CurrentCard = card;
                    _gameSession.score++;
                    return Ok(_gameSession);
                }

            }
            else
            {
                if (card.Value < _gameSession.CurrentCard.Value)
                {
                    _gameSession.CurrentCard = card;
                    _gameSession.score++;
                    return Ok(_gameSession);
                }
            }


            _gameSession.CurrentCard = card;
            _gameSession.EndGame();
            return Ok(_gameSession);

        }





        [HttpGet("Info")]
        public async Task<IActionResult> Info()
        {
            return Ok(_gameSession);
        }
    }
}
