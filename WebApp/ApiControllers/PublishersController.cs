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
    /// API controller for Publishers
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PublishersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private PublisherMapper _publisherMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public PublishersController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _publisherMapper = new PublisherMapper(Mapper);
        }

        // GET: api/Publishers
        /// <summary>
        /// Get all Publishers
        /// </summary>
        /// <returns>List of Publishers</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<Publisher>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            var bllPublishers = await _bll.Publishers.GetAllAsync();

            List<Publisher> returnPublishers = bllPublishers.Select(publisher => _publisherMapper.Map(publisher)!).ToList();
            return Ok(returnPublishers);
        }

        // GET: api/Publishers/5
        /// <summary>
        /// Get one Publisher based on id
        /// </summary>
        /// <param name="id">Id of the Publisher</param>
        /// <returns>Publisher</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Publisher), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Publisher>> GetPublisher(Guid id)
        {
            var publisher = await _bll.Publishers.FirstOrDefaultAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            var returnPublisher = _publisherMapper.Map(publisher)!;
            
            return returnPublisher;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing Publisher based on id
        /// </summary>
        /// <param name="id">Id of the Publisher</param>
        /// <param name="publisher">Updated Publisher</param>
        /// <returns>Nop content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPublisher(Guid id, PublisherAdd publisher)
        {
            var bllPublisher = PublisherMapper.MapToBll(publisher);

            _bll.Publishers.Update(bllPublisher);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PublisherExists(id))
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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Publisher
        /// </summary>
        /// <param name="publisher">Publisher that is being added</param>
        /// <returns>Added Publisher</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Publisher), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Publisher>> PostPublisher(PublisherAdd publisher)
        {
            var bllPublisher = PublisherMapper.MapToBll(publisher);
            
            var addedPublisher = _bll.Publishers.Add(bllPublisher);
            await _bll.SaveChangesAsync();

            var returnPublisher = _publisherMapper.Map(addedPublisher)!;

            return CreatedAtAction("GetPublisher", 
                new
                {
                    id = returnPublisher.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnPublisher);
        }

        // DELETE: api/Publishers/5
        /// <summary>
        /// Delete an existing Publisher based on id
        /// </summary>
        /// <param name="id">Id of the Publisher</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            var publisher = await _bll.Publishers.FirstOrDefaultAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _bll.Publishers.Remove(publisher);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PublisherExists(Guid id)
        {
            return await _bll.Publishers.ExistsAsync(id);
        }
    }
}
