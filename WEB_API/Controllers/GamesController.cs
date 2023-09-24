using Microsoft.AspNetCore.Mvc;
using WEB_API.Data;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase {
        private readonly ILogger<GamesController> _logger;
        private GamesDbContext _gameDbContext;

        public GamesController(ILogger<GamesController> logger, GamesDbContext gamesDbContext) {
            _logger = logger;
            _gameDbContext = gamesDbContext;
        }

        [HttpPost(Name = "ImportAllGames")]
        public IEnumerable<Game> PostAllGames() {

            return _gameDbContext.Games.ToList();
        }

        [HttpGet(Name = "GetAllGames")]
        public IEnumerable<Game> GetAllGames() {
            return _gameDbContext.Games.ToList();
        }
    }
}
