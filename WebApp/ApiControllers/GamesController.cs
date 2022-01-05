using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Games
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private GameMapper _gameMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public GamesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _gameMapper = new GameMapper(Mapper);
        }

        // GET: api/Games
        /// <summary>
        /// Get all Games
        /// </summary>
        /// <returns>List of Games</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<Game>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var bllGames = await _bll.Games.GetAllApiAsync();

            List<Game> returnGames = bllGames.Select(game => _gameMapper.Map(game)!).ToList();
            return Ok(returnGames);
        }
        
        // GET: api/Games/GameInfo/5
        /// <summary>
        /// Get all Games by GameInfo Id
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>List of Games</returns>
        [HttpGet("GameInfo/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<Game>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByGameInfo(Guid id)
        {
            var bllGames = await _bll.Games.GetAllByGameApiAsync(id);

            List<Game> returnGames = bllGames.Select(game => _gameMapper.Map(game)!).ToList();
            return Ok(returnGames);
        }
        
        // GET: api/Games/GameInfo/5/true
        /// <summary>
        /// Get all Games by GameInfo Id based on their availability
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <param name="available">Game's availability</param>
        /// <returns>List of available Games</returns>
        [HttpGet("GameInfo/{id}/{available}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<Game>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByGameAndAvailability(Guid id, bool available)
        {
            var bllGames = await _bll.Games.GetAllAvailableByGameApiAsync(id, available);

            List<Game> returnGames = bllGames.Select(game => _gameMapper.Map(game)!).ToList();
            return Ok(returnGames);
        }
        
        // GET: api/Games/GameInfo/5/One
        /// <summary>
        /// Get one available Game by GameInfo Id
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>Game</returns>
        [HttpGet("GameInfo/{id}/One")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Game>> GetAvailableGame(Guid id)
        {
            var game = await _bll.Games.GetAvailableGameApiAsync(id);
            
            if (game == null)
            {
                return NotFound();
            }

            var returnGame = _gameMapper.Map(game)!;

            return returnGame;
        }

        // GET: api/Games/5
        /// <summary>
        /// Get one Game based on id
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <returns>Game</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            var game = await _bll.Games.FirstOrDefaultApiAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            var returnGame = _gameMapper.Map(game)!;

            return returnGame;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing Game based on id
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <param name="game">Updated Game</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutGame(Guid id, GameAdd game)
        {
            var bllGame = GameMapper.MapToBll(game);

            _bll.Games.Update(bllGame);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Game
        /// </summary>
        /// <param name="game">Game that is being added</param>
        /// <returns>Added Game</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Game>> PostGame(GameAdd game)
        {
            var bllGame = GameMapper.MapToBll(game);
            
            var addedGame = _bll.Games.Add(bllGame);
            await _bll.SaveChangesAsync();

            var returnGame = _gameMapper.Map(addedGame)!;

            return CreatedAtAction("GetGame", 
                new
                {
                    id = returnGame.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnGame);
        }

        // DELETE: api/Games/5
        /// <summary>
        /// Delete an existing Game based on id
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var game = await _bll.Games.FirstOrDefaultAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _bll.Games.Remove(game);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GameExists(Guid id)
        {
            return await _bll.Games.ExistsAsync(id);
        }
    }
}
