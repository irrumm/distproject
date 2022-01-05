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
    /// API controller for PaymentMethods
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private PaymentMethodMapper _paymentMethodMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public PaymentMethodsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _paymentMethodMapper = new PaymentMethodMapper(Mapper);
        }

        // GET: api/PaymentMethods
        /// <summary>
        /// Get all PaymentMethods
        /// </summary>
        /// <returns>List of PaymentMethods</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethod>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            var bllPaymentMethods = await _bll.PaymentMethods.GetAllAsync();

            List<PaymentMethod> returnPaymentMethods = bllPaymentMethods.Select(paymentMethod => _paymentMethodMapper.Map(paymentMethod)!).ToList();
            return Ok(returnPaymentMethods);
        }

        // GET: api/PaymentMethods/5
        /// <summary>
        /// Get one PaymentMethod based on id
        /// </summary>
        /// <param name="id">Id of the PaymentMethod</param>
        /// <returns>PaymentMethod</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(PaymentMethod), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            var returnPaymentMethod = _paymentMethodMapper.Map(paymentMethod)!;

            return returnPaymentMethod;
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing PaymentMethod based on id
        /// </summary>
        /// <param name="id">Id of the PaymentMethod</param>
        /// <param name="paymentMethod">Updated PaymentMethod</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethodAdd paymentMethod)
        {
            var bllPaymentMethod = PaymentMethodMapper.MapToBll(paymentMethod);

            _bll.PaymentMethods.Update(bllPaymentMethod);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentMethodExists(id))
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

        // POST: api/PaymentMethods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new PaymentMethod
        /// </summary>
        /// <param name="paymentMethod">PaymentMethod that is being added</param>
        /// <returns>Added PaymentMethod</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(PaymentMethod), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethodAdd paymentMethod)
        {
            var bllPaymentMethod = PaymentMethodMapper.MapToBll(paymentMethod);
            
            var addedPaymentMethod = _bll.PaymentMethods.Add(bllPaymentMethod);
            await _bll.SaveChangesAsync();

            var returnPaymentMethod = _paymentMethodMapper.Map(addedPaymentMethod)!;

            return CreatedAtAction("GetPaymentMethod", 
                new
                {
                    id = returnPaymentMethod.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnPaymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        /// <summary>
        /// Delete an existing PaymentMethod based on id
        /// </summary>
        /// <param name="id">Id of the PaymentMethod</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _bll.PaymentMethods.Remove(paymentMethod);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PaymentMethodExists(Guid id)
        {
            return await _bll.PaymentMethods.ExistsAsync(id);
        }
    }
}
