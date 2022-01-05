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
    /// API controller for GamePictures
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamePicturesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private GamePictureMapper _gamePictureMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public GamePicturesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _gamePictureMapper = new GamePictureMapper(Mapper);
        }

        // GET: api/GamePictures
        /// <summary>
        /// Get all GamePictures
        /// </summary>
        /// <returns>List of GamePictures</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GamePicture>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GamePicture>>> GetGamePictures()
        {
            var bllGamePictures = await _bll.GamePictures.GetAllApiAsync();

            List<GamePicture> returnGamePictures = bllGamePictures.Select(gamePicture => _gamePictureMapper.Map(gamePicture)!).ToList();
            return Ok(returnGamePictures);
        }
        
        // GET: api/GamePictures/Game/5
        /// <summary>
        /// Get all GamePictures by GameInfo
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>List of GamePictures</returns>
        [HttpGet("Game/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<GamePicture>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GamePicture>>> GetGamePicturesByGame(Guid id)
        {
            var bllGamePictures = await _bll.GamePictures.GetAllByGameApiAsync(id);

            List<GamePicture> returnGamePictures = bllGamePictures.Select(gamePicture => _gamePictureMapper.Map(gamePicture)!).ToList();
            return Ok(returnGamePictures);
        }

        // GET: api/GamePictures/5
        /// <summary>
        /// Get one GamePicture based on id
        /// </summary>
        /// <param name="id">Id of the GamePicture</param>
        /// <returns>GamePicture</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GamePicture), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GamePicture>> GetGamePicture(Guid id)
        {
            var gamePicture = await _bll.GamePictures.FirstOrDefaultApiAsync(id);

            if (gamePicture == null)
            {
                return NotFound();
            }

            var returnGamePicture = _gamePictureMapper.Map(gamePicture)!;

            return returnGamePicture;
        }

        // PUT: api/GamePictures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing GamePicture based on id
        /// </summary>
        /// <param name="id">Id of the GamePicture</param>
        /// <param name="gamePicture">Updated GamePicture</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutGamePicture(Guid id, GamePictureAdd gamePicture)
        {
            var bllGamePicture = GamePictureMapper.MapToBll(gamePicture);

            _bll.GamePictures.Update(bllGamePicture);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GamePictureExists(id))
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

        // POST: api/GamePictures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new GamePicture
        /// </summary>
        /// <param name="gamePicture">GamePicture that is being added</param>
        /// <returns>Added GamePicture</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(GamePicture), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GamePicture>> PostGamePicture(GamePictureAdd gamePicture)
        {
            var bllGamePicture = GamePictureMapper.MapToBll(gamePicture);
            
            var addedGamePicture = _bll.GamePictures.Add(bllGamePicture);
            await _bll.SaveChangesAsync();

            var returnGamePicture = _gamePictureMapper.Map(addedGamePicture)!;

            return CreatedAtAction("GetGamePicture", 
                new
                {
                    id = returnGamePicture.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnGamePicture);
        }

        // DELETE: api/GamePictures/5
        /// <summary>
        /// Delete an existing GamePicture based on id
        /// </summary>
        /// <param name="id">Id of the GamePicture</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGamePicture(Guid id)
        {
            var gamePicture = await _bll.GamePictures.FirstOrDefaultAsync(id);
            if (gamePicture == null)
            {
                return NotFound();
            }

            _bll.GamePictures.Remove(gamePicture);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> GamePictureExists(Guid id)
        {
            return await _bll.GamePictures.ExistsAsync(id);
        }
    }
}
