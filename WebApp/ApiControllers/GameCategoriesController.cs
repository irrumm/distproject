using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Contracts.DAL.App;
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
    /// API controller for GameCategories
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private GameCategoryMapper _gameCategoryMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public GameCategoriesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _gameCategoryMapper = new GameCategoryMapper(Mapper);
        }

        // GET: api/GameCategories
        /// <summary>
        /// Get all GameCategories
        /// </summary>
        /// <returns>List of GameCategories</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GameCategory>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.GameCategory>>> GetGameCategories()
        {
            var bllGameCategories = await _bll.GameCategories.GetAllApiAsync();

            List<GameCategory> returnGameCategories = bllGameCategories.Select(gameCategory => _gameCategoryMapper.Map(gameCategory)!).ToList();
            return Ok(returnGameCategories);
        }
        
        // GET: api/GameCategories/Game/5
        /// <summary>
        /// Get all GameCategories by GameInfo
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>List of GameCategories</returns>
        [HttpGet("Game/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GameCategory>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.GameCategory>>> GetGameCategoriesByGame(Guid id)
        {
            var bllGameCategories = await _bll.GameCategories.GetAllByGameApiAsync(id);

            List<GameCategory> returnGameCategories = bllGameCategories.Select(gameCategory => _gameCategoryMapper.Map(gameCategory)!).ToList();
            return Ok(returnGameCategories);
        }

        // GET: api/GameCategories/5
        /// <summary>
        /// Get one GameCategory based on id
        /// </summary>
        /// <param name="id">Id of the GameCategory</param>
        /// <returns>GameCategory</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GameCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GameCategory>> GetGameCategory(Guid id)
        {
            var gameCategory = await _bll.GameCategories.FirstOrDefaultApiAsync(id);

            if (gameCategory == null)
            {
                return NotFound();
            }

            var returnGameCategory = _gameCategoryMapper.Map(gameCategory)!;

            return returnGameCategory;
        }

        // PUT: api/GameCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing GameCategory based on id
        /// </summary>
        /// <param name="id">Id of the GameCategory</param>
        /// <param name="gameCategory">Updated GameCategory</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutGameCategory(Guid id, GameCategoryAdd gameCategory)
        {
            var bllGameCategory = GameCategoryMapper.MapToBll(gameCategory);

            _bll.GameCategories.Update(bllGameCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameCategoryExists(id))
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

        // POST: api/GameCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new GameCategory
        /// </summary>
        /// <param name="gameCategory">GameCategory that is being added</param>
        /// <returns>Added GameCategory</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GameCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GameCategory>> PostGameCategory(GameCategoryAdd gameCategory)
        {
            var bllGameCategory = GameCategoryMapper.MapToBll(gameCategory);
            
            var addedGameCategory = _bll.GameCategories.Add(bllGameCategory);
            await _bll.SaveChangesAsync();

            var returnGameCategory = _gameCategoryMapper.Map(addedGameCategory)!;

            return CreatedAtAction("GetGameCategory", 
                new
                {
                    id = returnGameCategory.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnGameCategory);
        }

        // DELETE: api/GameCategories/5
        /// <summary>
        /// Delete an existing GameCategory based on id
        /// </summary>
        /// <param name="id">Id of the GameCategory</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGameCategory(Guid id)
        {
            var gameCategory = await _bll.GameCategories.FirstOrDefaultAsync(id);
            if (gameCategory == null)
            {
                return NotFound();
            }

            _bll.GameCategories.Remove(gameCategory);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GameCategoryExists(Guid id)
        {
            return await _bll.GameCategories.ExistsAsync(id);
        }
    }
}
