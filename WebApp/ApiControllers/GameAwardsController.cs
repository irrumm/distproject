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
    /// API controller for GameAwards
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameAwardsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private GameAwardMapper _gameAwardMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public GameAwardsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _gameAwardMapper = new GameAwardMapper(Mapper);
        }

        // GET: api/GameAwards
        /// <summary>
        /// Get all GameAwards
        /// </summary>
        /// <returns>List of GameAwards</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GameAward>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameAward>>> GetGameAwards()
        {
            var bllGameAwards = await _bll.GameAwards.GetAllApiAsync();

            List<GameAward> returnGameAwards = bllGameAwards.Select(gameAward => _gameAwardMapper.Map(gameAward)!).ToList();
            return Ok(returnGameAwards);
        }
        
        // GET: api/GameAwards/Game/5
        /// <summary>
        /// Get all GameAwards by GameInfo
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>List of GameAwards</returns>
        [HttpGet("Game/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GameAward>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameAward>>> GetGameAwardsByGameInfo(Guid id)
        {
            var bllGameAwards = await _bll.GameAwards.GetAllByGameApiAsync(id);

            List<GameAward> returnGameAwards = bllGameAwards.Select(gameAward => _gameAwardMapper.Map(gameAward)!).ToList();
            return Ok(returnGameAwards);
        }
        
        // GET: api/GameAwards/Award/5
        /// <summary>
        /// Get all GameAwards by Award
        /// </summary>
        /// <param name="id">Id of the Award</param>
        /// <returns>List of GameAwards</returns>
        [HttpGet("Award/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GameAward>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameAward>>> GetGameAwardsByAward(Guid id)
        {
            var bllGameAwards = await _bll.GameAwards.GetAllByAwardApiAsync(id);

            List<GameAward> returnGameAwards = bllGameAwards.Select(gameAward => _gameAwardMapper.Map(gameAward)!).ToList();
            return Ok(returnGameAwards);
        }

        // GET: api/GameAwards/5
        /// <summary>
        /// Get one GameAward based on id
        /// </summary>
        /// <param name="id">Id of the GameAward</param>
        /// <returns>GameAward</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GameAward), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameAward>> GetGameAward(Guid id)
        {
            var gameAward = await _bll.GameAwards.FirstOrDefaultApiAsync(id);

            if (gameAward == null)
            {
                return NotFound();
            }

            var returnGameAward = _gameAwardMapper.Map(gameAward)!;

            return returnGameAward;
        }

        // PUT: api/GameAwards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing GameAward based on id
        /// </summary>
        /// <param name="id">Id of the GameAward</param>
        /// <param name="gameAward">Updated GameAward</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutGameAward(Guid id, GameAwardAdd gameAward)
        {
            var bllGameAward = GameAwardMapper.MapToBll(gameAward);

            _bll.GameAwards.Update(bllGameAward);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameAwardExists(id))
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

        // POST: api/GameAwards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new GameAward
        /// </summary>
        /// <param name="gameAward">GameAward that is being added</param>
        /// <returns>Added GameAward</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GameAward), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GameAward>> PostGameAward(GameAwardAdd gameAward)
        {
            var bllGameAward = GameAwardMapper.MapToBll(gameAward);
            
            var addedGameAward = _bll.GameAwards.Add(bllGameAward);
            await _bll.SaveChangesAsync();

            var returnGameAward = _gameAwardMapper.Map(addedGameAward)!;

            return CreatedAtAction("GetGameAward", 
                new
                {
                    id = returnGameAward.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnGameAward);
        }

        // DELETE: api/GameAwards/5
        /// <summary>
        /// Delete an existing GameAward based on id
        /// </summary>
        /// <param name="id">Id of the GameAward</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGameAward(Guid id)
        {
            var gameAward = await _bll.GameAwards.FirstOrDefaultAsync(id);
            if (gameAward == null)
            {
                return NotFound();
            }

            _bll.GameAwards.Remove(gameAward);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GameAwardExists(Guid id)
        {
            return await _bll.GameAwards.ExistsAsync(id);
        }
    }
}
