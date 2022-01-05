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
    /// API controller for Orders
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private OrdersMapper _ordersMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public OrdersController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _ordersMapper = new OrdersMapper(Mapper);
        }

        // GET: api/Orders
        /// <summary>
        /// Get all Orders
        /// </summary>
        /// <returns>List of Orders</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<Orders>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            IEnumerable<BLL.App.DTO.Orders> bllOrders;
            
            if (!User.IsInRole("Admin"))
            {
                bllOrders = await _bll.Orders.GetAllApiAsync(User.GetUserId()!.Value);
            }
            else
            {
                bllOrders = await _bll.Orders.GetAllApiAsync(default);
            }
            
            List<Orders> returnOrders = bllOrders.Select(order => _ordersMapper.Map(order)!).ToList();
            return Ok(returnOrders);
        }
        
        // GET: api/Orders/User/5
        /// <summary>
        /// Get all Orders by User
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of Orders</returns>
        [HttpGet("User/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<Orders>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrdersByUser(Guid id)
        {
            var bllOrders = await _bll.Orders.GetAllByUserApiAsync(id);

            List<Orders> returnOrders = bllOrders.Select(order => _ordersMapper.Map(order)!).ToList();
            return Ok(returnOrders);
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get one Order based on id
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>Order</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Orders), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Orders>> GetOrders(Guid id)
        {
            BLL.App.DTO.Orders? orders;
            
            if (User.IsInRole("Admin"))
            {
                orders = await _bll.Orders.FirstOrDefaultApiAsync(id, default);
            }
            else
            {
                orders = await _bll.Orders.FirstOrDefaultApiAsync(id, User.GetUserId()!.Value);
            }
            if (orders == null)
            {
                return NotFound();
            }

            var returnOrder = _ordersMapper.Map(orders)!;

            return returnOrder;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Order
        /// </summary>
        /// <param name="orders">Order that is being added</param>
        /// <returns>Added Order</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Orders), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Orders>> PostOrders(OrdersAdd orders)
        {
            var bllOrder = OrdersMapper.MapToBll(orders);
            bllOrder.AppUserId = User.GetUserId()!.Value;
            
            var addedOrder = _bll.Orders.Add(bllOrder);
            await _bll.SaveChangesAsync();
            var placedOrder = await _bll.Orders.CreateOrder(addedOrder.Id, orders.GameIds);

            var returnOrder = _ordersMapper.Map(placedOrder)!;
            
            return CreatedAtAction("GetOrders", 
                new
                {
                    id = returnOrder.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnOrder);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete an existing Order based on id
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrders(Guid id)
        {
            BLL.App.DTO.Orders? order;
            if (User.IsInRole("Admin"))
            {
                order = await _bll.Orders.FirstOrDefaultApiAsync(id, default);
            }
            else
            {
                order = await _bll.Orders.FirstOrDefaultApiAsync(id, User.GetUserId()!.Value);
            }
            if (order == null)
            {
                return NotFound();
            }

            _bll.Orders.Remove(order);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> OrdersExists(Guid id)
        {
            return await _bll.Orders.ExistsAsync(id);
        }
    }
}
