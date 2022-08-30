using CardGameAPI.Data;
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
        private readonly CardGameAPIContext _context;

        public GameController(IGameSession gameSession, CardGameAPIContext context)
        {
            _gameSession = gameSession;
            _context = context;
        }

        [HttpGet("Highscore")]
        public async Task<IActionResult> GetHighscores()
        {
            return Ok(_context.Highscore.ToList().OrderByDescending(h => h.Score));
        }

        [HttpGet("Start")]
        public async Task<IActionResult> Start(string name)
        {
            if (_gameSession.StartGame(name))
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
            if (_gameSession.EndGame())
            {
                if (_gameSession.score >= 3)
                {
                    _context.Highscore.Add(new Highscore() { Name = _gameSession.Name, Score = _gameSession.score });
                    await _context.SaveChangesAsync();

                }
                return Ok(_gameSession);
            }
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
            if (_gameSession.score >= 3)
            {
                if (_context.Highscore.Count() > 0)
                {
                    if (_context.Highscore.Where(x => x.Score == _gameSession.score).Count() > 0)
                    {
                        var s = _context.Highscore.Where(x => x.Score == _gameSession.score).First();
                        if (s is not null) _context.Highscore.Remove(s);
                    }
                }
                _context.Highscore.Add(new Highscore() { Name = _gameSession.Name, Score = _gameSession.score });
                await _context.SaveChangesAsync();

            }
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
