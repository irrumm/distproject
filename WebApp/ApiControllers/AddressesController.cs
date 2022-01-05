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
    /// API controller for Addresses
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private AddressMapper _addressMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public AddressesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _addressMapper = new AddressMapper(Mapper);
        }

        // GET: api/Addresses
        /// <summary>
        /// Get all Addresses
        /// </summary>
        /// <returns>List of Addresses</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Address>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var bllAddresses = await _bll.Addresses.GetAllAsync();

            List<Address> returnAddresses = bllAddresses.Select(address => _addressMapper.Map(address)!).ToList();
            return Ok(returnAddresses);
        }

        // GET: api/Addresses/5
        /// <summary>
        /// Get one Address based on id
        /// </summary>
        /// <param name="id">Id of the Address</param>
        /// <returns>Address</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            var address = await _bll.Addresses.FirstOrDefaultAsync(id);

            if (address == null)
            {
                return NotFound();
            }
            
            var returnAddress = _addressMapper.Map(address);

            return returnAddress!;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing Address based on id
        /// </summary>
        /// <param name="id">Id of the Address</param>
        /// <param name="address">Updated Address</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutAddress(Guid id, AddressAdd address)
        {
            var bllAddress = AddressMapper.MapToBll(address);
            bllAddress.Id = id;

            _bll.Addresses.Update(bllAddress);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Address
        /// </summary>
        /// <param name="address">Address that is being added</param>
        /// <returns>Added Address</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.DTO.v1.Address>> PostAddress(AddressAdd address)
        {
            var bllAddress = AddressMapper.MapToBll(address);
            
            var addedAddress = _bll.Addresses.Add(bllAddress);
            await _bll.SaveChangesAsync();

            var returnAddress = _addressMapper.Map(addedAddress);

            return CreatedAtAction("GetAddress", 
                new
                {
                    id = returnAddress!.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnAddress);
        }

        // DELETE: api/Addresses/5
        /// <summary>
        /// Delete an existing Address based on id
        /// </summary>
        /// <param name="id">Id of the Address</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _bll.Addresses.FirstOrDefaultAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _bll.Addresses.Remove(address);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AddressExists(Guid id)
        {
            return await _bll.Addresses.ExistsAsync(id);
        }
    }
}
