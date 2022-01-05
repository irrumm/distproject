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
    /// API controller for Languages
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LanguagesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        private LanguageMapper _languageMapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="mapper"></param>
        public LanguagesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
            _languageMapper = new LanguageMapper(Mapper);
        }

        // GET: api/Languages
        /// <summary>
        /// Get all Languages
        /// </summary>
        /// <returns>List of Languages</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(IEnumerable<Language>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            var bllLanguages = await _bll.Languages.GetAllAsync();

            List<Language> returnLanguages = bllLanguages.Select(language => _languageMapper.Map(language)!).ToList();
            return Ok(returnLanguages);
        }

        // GET: api/Languages/5
        /// <summary>
        /// Get one Language based on id
        /// </summary>
        /// <param name="id">Id of the Language</param>
        /// <returns>Language</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Language), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Language>> GetLanguage(Guid id)
        {
            var language = await _bll.Languages.FirstOrDefaultAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            var returnLanguage = _languageMapper.Map(language)!;

            return returnLanguage;
        }

        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update an existing Language based on id
        /// </summary>
        /// <param name="id">Id of the Language</param>
        /// <param name="language">Updated Language</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutLanguage(Guid id, LanguageAdd language)
        {
            var bllLanguage = LanguageMapper.MapToBll(language);
            bllLanguage.Id = id;

            _bll.Languages.Update(bllLanguage);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LanguageExists(id))
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

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Language
        /// </summary>
        /// <param name="language">Language that is being added</param>
        /// <returns>Added Language</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Language), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Language>> PostLanguage(LanguageAdd language)
        {
            var bllLanguage = LanguageMapper.MapToBll(language);
            
            var addedLanguage = _bll.Languages.Add(bllLanguage);
            await _bll.SaveChangesAsync();

            var returnLanguage = _languageMapper.Map(addedLanguage)!;

            return CreatedAtAction("GetLanguage", 
                new
                {
                    id = returnLanguage.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnLanguage);
        }

        // DELETE: api/Languages/5
        /// <summary>
        /// Delete an existing Language based on id
        /// </summary>
        /// <param name="id">Id of the Language</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLanguage(Guid id)
        {
            var language = await _bll.Languages.FirstOrDefaultAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            _bll.Languages.Remove(language);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> LanguageExists(Guid id)
        {
            return await _bll.Languages.ExistsAsync(id);
        }
    }
}
