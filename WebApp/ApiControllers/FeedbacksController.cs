using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Feedbacks
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private FeedbackMapper _feedbackMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public FeedbacksController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _feedbackMapper = new FeedbackMapper(Mapper);
        }

        // GET: api/Feedbacks
        /// <summary>
        /// Get all Feedbacks
        /// </summary>
        /// <returns>List of Feedbacks</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<Feedback>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            IEnumerable<BLL.App.DTO.Feedback> bllFeedbacks;
            
            if (!User.IsInRole("Admin"))
            { 
                bllFeedbacks = await _bll.Feedbacks.GetAllApiAsync(User.GetUserId()!.Value);
            }
            else
            {
                bllFeedbacks = await _bll.Feedbacks.GetAllApiAsync(default);
            }

            List<Feedback> returnFeedbacks = bllFeedbacks.Select(feedback => _feedbackMapper.Map(feedback)!).ToList();
            return Ok(returnFeedbacks);
        }
        
        // GET: api/Feedbacks/User/5
        /// <summary>
        /// Get all Feedbacks by an user
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>List of User's Feedbacks</returns>
        [HttpGet("User/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<Feedback>), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksByUser(Guid id)
        {
            var bllFeedbacks = await _bll.Feedbacks.GetAllByUserApiAsync(id);

            List<Feedback> returnFeedbacks = bllFeedbacks.Select(feedback => _feedbackMapper.Map(feedback)!).ToList();
            return Ok(returnFeedbacks);
        }
        
        // GET: api/Feedbacks/Game/5
        /// <summary>
        /// Get all Feedbacks by GameInfo
        /// </summary>
        /// <param name="id">Id of the GameInfo</param>
        /// <returns>List of Feedbacks</returns>
        [HttpGet("Game/{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<Feedback>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacksByGame(Guid id)
        {
            IEnumerable<BLL.App.DTO.Feedback> bllFeedbacks;
            
            bllFeedbacks = await _bll.Feedbacks.GetAllByGameApiAsync(id);

            List<Feedback> returnFeedbacks = bllFeedbacks.Select(feedback => _feedbackMapper.Map(feedback)!).ToList();
            return Ok(returnFeedbacks);
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Get one Feedback based on id
        /// </summary>
        /// <param name="id">Id of the Feedback</param>
        /// <returns>Feedback</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Feedback>> GetFeedback(Guid id)
        {
            BLL.App.DTO.Feedback? feedback;
            
            if (User.IsInRole("Admin"))
            {
                feedback = await _bll.Feedbacks.FirstOrDefaultApiAsync(id, default);
            }
            else
            {
                feedback = await _bll.Feedbacks.FirstOrDefaultApiAsync(id, User.GetUserId()!.Value);
            }

            if (feedback == null)
            {
                return NotFound();
            }

            var returnFeedback = _feedbackMapper.Map(feedback)!;

            return returnFeedback;
        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Feedback
        /// </summary>
        /// <param name="feedback">Feedback that is being added</param>
        /// <returns>Added Feedback</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Feedback), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Feedback>> PostFeedback(FeedbackAdd feedback)
        {
            var bllFeedback = FeedbackMapper.MapToBll(feedback);
            bllFeedback.AppUserId = User.GetUserId()!.Value;
            
            var addedFeedback = _bll.Feedbacks.Add(bllFeedback);
            await _bll.SaveChangesAsync();

            var returnFeedback = _feedbackMapper.Map(addedFeedback)!;

            return CreatedAtAction("GetFeedback", 
                new
                {
                    id = returnFeedback.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnFeedback);
        }

        // DELETE: api/Feedbacks/5
        /// <summary>
        /// Delete an existing Feedback based on id
        /// </summary>
        /// <param name="id">Id of the Feedback</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteFeedback(Guid id)
        {
            BLL.App.DTO.Feedback? feedback;
            if (User.IsInRole("Admin"))
            {
                feedback = await _bll.Feedbacks.FirstOrDefaultApiAsync(id, default);
            }
            else
            {
                feedback = await _bll.Feedbacks.FirstOrDefaultApiAsync(id, User.GetUserId()!.Value);
            }
            if (feedback == null)
            {
                return NotFound();
            }

            _bll.Feedbacks.Remove(feedback);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> FeedbackExists(Guid id)
        {
            return await _bll.Feedbacks.ExistsAsync(id);
        }
    }
}
