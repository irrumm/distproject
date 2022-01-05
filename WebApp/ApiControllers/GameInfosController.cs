using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for GameInfos
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameInfosController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private GameInfoMapper _gameInfoMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public GameInfosController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _gameInfoMapper = new GameInfoMapper(Mapper);
        }
        
        // GET: api/GameInfos
        /// <summary>
        /// Get all GameInfos
        /// </summary>
        /// <returns>List of GameInfos</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GameInfo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameInfo>>> GetGameInfos()
        {
            var bllGameInfos = await _bll.GameInfos.GetAllWithCountsAsync();

            List<GameInfo> returnGameInfos = bllGameInfos.Select(gameInfo => _gameInfoMapper.Map(gameInfo)!).ToList();
            return Ok(returnGameInfos);
        }

        // POST: api/GameInfos
        /// <summary>
        /// Get all GameInfos filtered
        /// </summary>
        /// <param name="filters">Filters of the GameInfo</param>
        /// <returns>List of GameInfos</returns>
        [HttpPost("Filtered")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<GameInfo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameInfo>>> GetGameInfoFilters(GameInfoFilters filters)
        {
            var bllGameInfos = await _bll.GameInfos.GetAllFiltered(filters.Categories, filters.Languages, filters.Publishers, filters.MinCost, filters.MaxCost);

            List<GameInfo> returnGameInfos = bllGameInfos.Select(gameInfo => _gameInfoMapper.Map(gameInfo)!).ToList();

            return Ok(returnGameInfos);
        }

        // GET: api/GameInfosTitle/Search/abc
        /// <summary>
        /// Get all GameInfos by title
        /// </summary>
        /// <param name="title">Title of the GameInfo</param>
        /// <returns>List of GameInfos</returns>
        [HttpGet("Search/{title}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GameInfo>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GameInfo>>> GetGameInfosByTitle(string title)
        {
            var bllGameInfos = await _bll.GameInfos.GetAllByTitleApiAsync(title);

            List<GameInfo> returnGameInfos = bllGameInfos.Select(gameInfo => _gameInfoMapper.Map(gameInfo)!).ToList();
            return Ok(returnGameInfos);
        }

        // GET: api/GameInfos/5
        /// <summary>
        /// Get one GameInfo based on id
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>GameInfo</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(GameInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameInfo>> GetGameInfo(Guid id)
        {
            var gameInfo = await _bll.GameInfos.FirstOrDefaultWithCountsAsync(id);

            if (gameInfo == null)
            {
                return NotFound();
            }

            var returnGameInfo = _gameInfoMapper.Map(gameInfo)!;

            return returnGameInfo;
        }

        // PUT: api/GameInfos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing GameInfo based on id
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <param name="gameInfo">Updated GameInfo</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutGameInfo(Guid id, GameInfoAdd gameInfo)
        {
            var bllGameInfo = GameInfoMapper.MapToBll(gameInfo);
            
            _bll.GameInfos.Update(bllGameInfo);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameInfoExists(id))
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

        // POST: api/GameInfos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new GameInfo
        /// </summary>
        /// <param name="gameInfo">GameInfo that is being added</param>
        /// <returns>Added GameInfo</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GameInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GameInfo>> PostGameInfo(GameInfoAdd gameInfo)
        {
            var bllGameInfo = GameInfoMapper.MapToBll(gameInfo);
            
            var addedGameInfo = _bll.GameInfos.Add(bllGameInfo);
            await _bll.SaveChangesAsync();

            var returnGameInfo = _gameInfoMapper.Map(addedGameInfo)!;

            return CreatedAtAction("GetGameInfo", 
                new
                {
                    id = returnGameInfo.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnGameInfo);
        }

        // DELETE: api/GameInfos/5
        /// <summary>
        /// Delete an existing GameInfo based on id
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGameInfo(Guid id)
        {
            var gameInfo = await _bll.GameInfos.FirstOrDefaultAsync(id);
            if (gameInfo == null)
            {
                return NotFound();
            }

            _bll.GameInfos.Remove(gameInfo);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GameInfoExists(Guid id)
        {
            return await _bll.GameInfos.ExistsAsync(id);
        }
    }
}
