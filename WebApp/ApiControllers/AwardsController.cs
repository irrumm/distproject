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
    /// API controller fo Awards
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AwardsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private AwardMapper _awardMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public AwardsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _awardMapper = new AwardMapper(Mapper);
        }

        // GET: api/Awards
        /// <summary>
        /// Get all Awards
        /// </summary>
        /// <returns>List of Awards</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Award>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Award>>> GetAwards()
        {
            var bllAwards = await _bll.Awards.GetAllAsync();

            List<Award> returnAwards = bllAwards.Select(award => _awardMapper.Map(award)!).ToList();
            return Ok(returnAwards);
        }

        // GET: api/Awards/5
        /// <summary>
        /// Get one Award based on id
        /// </summary>
        /// <param name="id">Id of the Award</param>
        /// <returns>Award</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Award), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Award>> GetAward(Guid id)
        {
            var award = await _bll.Awards.FirstOrDefaultAsync(id);

            if (award == null)
            {
                return NotFound();
            }

            var returnAward = _awardMapper.Map(award);

            return returnAward!;
        }

        // PUT: api/Awards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing Award based on id
        /// </summary>
        /// <param name="id">Id of the Award</param>
        /// <param name="award">Updated Award</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutAward(Guid id, AwardAdd award)
        {
            var bllAward = AwardMapper.MapToBll(award);
            
            _bll.Awards.Update(bllAward);
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AwardExists(id))
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

        // POST: api/Awards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Award
        /// </summary>
        /// <param name="award">Award that is being added</param>
        /// <returns>Added Award</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Award), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Award>> PostAward(AwardAdd award)
        {
            var bllAward = AwardMapper.MapToBll(award);
            
            var addedAward = _bll.Awards.Add(bllAward);
            await _bll.SaveChangesAsync();

            var returnAward = _awardMapper.Map(addedAward)!;

            return CreatedAtAction("GetAward", 
                new
                {
                    id = returnAward.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnAward);
        }

        // DELETE: api/Awards/5
        /// <summary>
        /// Delete an existing Award based on id
        /// </summary>
        /// <param name="id">Id of the Award</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteAward(Guid id)
        {
            var award = await _bll.Awards.FirstOrDefaultAsync(id);
            if (award == null)
            {
                return NotFound();
            }

            _bll.Awards.Remove(award);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AwardExists(Guid id)
        {
            return await _bll.Awards.ExistsAsync(id);
        }
    }
}
