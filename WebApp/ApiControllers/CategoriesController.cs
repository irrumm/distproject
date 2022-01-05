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
    /// API controller for Categories
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private CategoryMapper _categoryMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public CategoriesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _categoryMapper = new CategoryMapper(Mapper);
        }

        // GET: api/Categories
        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>List of Categories</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var bllCategories = await _bll.Categories.GetAllAsync();

            List<Category> returnCategories = bllCategories.Select(category => _categoryMapper.Map(category)!).ToList();
            return Ok(returnCategories);
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get one Category based on id
        /// </summary>
        /// <param name="id">Id of the Category</param>
        /// <returns>Category</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var returnCategory = _categoryMapper.Map(category)!;

            return returnCategory;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing Category based on id
        /// </summary>
        /// <param name="id">Id of the Category</param>
        /// <param name="category">Updated Category</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCategory(Guid id, CategoryAdd category)
        {
            var bllCategory = CategoryMapper.MapToBll(category);

            _bll.Categories.Update(bllCategory);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Category
        /// </summary>
        /// <param name="category">Category that is being added</param>
        /// <returns>Added Category</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Category>> PostCategory(CategoryAdd category)
        {
            
            var bllCategory = CategoryMapper.MapToBll(category);
            
            var addedCategory = _bll.Categories.Add(bllCategory);
            await _bll.SaveChangesAsync();

            var returnCategory = _categoryMapper.Map(addedCategory);

            return CreatedAtAction("GetCategory", 
                new
                {
                    id = returnCategory!.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnCategory);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete an existing Category based on id
        /// </summary>
        /// <param name="id">Id of the Category</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _bll.Categories.Remove(category);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CategoryExists(Guid id)
        {
            return await _bll.Categories.ExistsAsync(id);
        }
    }
}
