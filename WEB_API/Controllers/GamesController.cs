using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using WEB_API.Data;
using WEB_API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WEB_API.Controllers
{
    public class GameDetails {
        [JsonProperty("developer")]
        public string developer { get; set; }

        [JsonProperty("genre")]
        public string genre { get; set; }

        [JsonProperty("language")]
        public string language { get; set; }

        [JsonProperty("release_year")]
        public string release_year { get; set; }

        [JsonProperty("version")]
        public string version { get; set; }
    }

    public class GameTemp {
        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("image")]
        public string image { get; set; }

        [JsonProperty("images")]
        public List<string> images { get; set; }

        [JsonProperty("info")]
        public GameDetails info { get; set; }

        [JsonProperty("system_requirements")]
        public Dictionary<string, string> system_requirements { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }
    }

    public class CategoryTemp {
        [JsonProperty("category_name")]
        public string category_name { get; set; }

        [JsonProperty("games")]
        public Dictionary<string, GameTemp> games { get; set; }
    }

    public class RootObject {
        public Dictionary<string, CategoryTemp> data { get; set; }
    }

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
        public async void PostAllGames() {
            using (var file = new StreamReader("games.json")) {
                string jsonString = file.ReadToEnd();
                var rootObject = JsonConvert.DeserializeObject<Dictionary<string, CategoryTemp>>(jsonString);

                foreach (var categoryEntry in rootObject.Values) {
                    var category = categoryEntry.category_name;
                    _gameDbContext.Category.Add(new Category { Name = category });
                    await _gameDbContext.SaveChangesAsync();

                    foreach (var gameEntry in categoryEntry.games) {
                        var gameId = Guid.NewGuid().ToString();

                        //System.InvalidOperationException:
                        //"The instance of entity type 'Info' cannot be tracked because another instance with the same key value for {'GameId'} is already being tracked.
                        //When attaching existing entities, ensure that only one entity instance with a given key value is attached.
                        //Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values."

                        //var existingInfo = _gameDbContext.Info.SingleOrDefault(info => info.GameId == gameId);
                        //if(existingInfo != null) _gameDbContext.Entry(existingInfo).State = EntityState.Detached;

                        //var g = new Game { Title = "" };
                        //g.SystemRequirements = new SystemRequirements { };
                        //g.Info = new Info { };
                        //_gameDbContext.Games.Add(g);

                        _gameDbContext.Games.Add(new Game
                        {
                            Id = gameId,
                            Title = gameEntry.Value.title,
                            Description = gameEntry.Value.description,
                            Image = gameEntry.Value.image,
                            Images = String.Join("; ", gameEntry.Value.images),
                            Info = new Info
                            {
                                GameId = gameId,
                                Developer = gameEntry.Value.info.developer,
                                Genre = gameEntry.Value.info.genre,
                                Language = gameEntry.Value.info.language,
                                ReleaseYear = gameEntry.Value.info.release_year,
                                Version = gameEntry.Value.info.version,
                            },
                            SystemRequirements = new SystemRequirements
                            {
                                GameId = gameId,
                                CPU = gameEntry.Value.system_requirements["CPU"],
                                GPU = gameEntry.Value.system_requirements["GPU"],
                                HDD = gameEntry.Value.system_requirements["HDD"],
                                OS = gameEntry.Value.system_requirements["OS"],
                                RAM = gameEntry.Value.system_requirements["RAM"],
                            },
                            CategoryId = _gameDbContext.Category.OrderBy(c => c.Id).Last().Id,
                        });
                        await _gameDbContext.SaveChangesAsync();
                    }
                }
            }
        }

        [HttpGet(Name = "GetAllGames")]
        public IEnumerable<Game> GetAllGames() {
            return _gameDbContext.Games.ToList();
        }
    }
}
