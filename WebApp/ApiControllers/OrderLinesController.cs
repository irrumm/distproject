using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Extensions.Base;
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
    /// API controller for OrderLines
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderLinesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private OrderLineMapper _orderLineMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public OrderLinesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _orderLineMapper = new OrderLineMapper(Mapper);
        }

        // GET: api/OrderLines
        /// <summary>
        /// Get all OrderLines
        /// </summary>
        /// <returns>List of OrderLines</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<OrderLine>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderLine>>> GetOrderLines()
        {
            var bllOrderLines = await _bll.OrderLines.GetAllApiAsync();

            List<OrderLine> returnOrderLines = bllOrderLines.Select(orderLine => _orderLineMapper.Map(orderLine)!).ToList();
            return Ok(returnOrderLines);
        }
        
        // GET: api/OrderLines/Order/5
        /// <summary>
        /// Get all OrderLines by Order Id
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>List of OrderLines</returns>
        [HttpGet("Order/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<OrderLine>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderLine>>> GetOrderLinesByOrder(Guid id)
        {
            IEnumerable<BLL.App.DTO.OrderLine> bllOrderLines;
            
            if (!User.IsInRole("Admin"))
            {
                bllOrderLines = await _bll.OrderLines.GetAllByOrderApiAsync(id, User.GetUserId()!.Value);
            }
            else
            {
                 bllOrderLines = await _bll.OrderLines.GetAllByOrderApiAsync(id, default);
            }

            List<OrderLine> returnOrderLines = bllOrderLines.Select(orderLine => _orderLineMapper.Map(orderLine)!).ToList();
            return Ok(returnOrderLines);
        }

        // GET: api/OrderLines/5
        /// <summary>
        /// Get one OrderLine based on id
        /// </summary>
        /// <param name="id">Id of the OrderLine</param>
        /// <returns>OrderLine</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(OrderLine), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<OrderLine>> GetOrderLine(Guid id)
        {
            var orderLine = await _bll.OrderLines.FirstOrDefaultApiAsync(id);

            if (orderLine == null)
            {
                return NotFound();
            }

            var returnOrderLine = _orderLineMapper.Map(orderLine)!;

            return returnOrderLine;
        }

        // DELETE: api/OrderLines/5
        /// <summary>
        /// Delete an existing OrderLine based on id
        /// </summary>
        /// <param name="id">Id of the OrderLine</param>
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
        public async Task<IActionResult> DeleteOrderLine(Guid id)
        {
            var orderLine = await _bll.OrderLines.FirstOrDefaultAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            _bll.OrderLines.Remove(orderLine);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> OrderLineExists(Guid id)
        {
            return await _bll.OrderLines.ExistsAsync(id);
        }
    }
}
