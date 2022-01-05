using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using PublicApi.DTO.v1.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// API controller for role management
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Admin/[action]")]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : Controller
    {
        private readonly RoleManager<Domain.App.Identity.AppRole> _roleManager;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// 
        /// </summary>
        public AdminController(RoleManager<Domain.App.Identity.AppRole> roleManager, UserManager<Domain.App.Identity.AppUser> userManager, ILogger<AccountController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: api/Admin/Roles
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AppRole>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Roles()
        {
            List<AppRole> returnRoles = new();
            var domainRoles = await _roleManager.Roles.ToListAsync();

            foreach (var role in domainRoles)
            {
                returnRoles.Add(new AppRole()
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            return Ok(returnRoles);
        }
        
        // GET: api/Admin/Users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Users()
        {
            List<AppUser> returnUsers = new();
            var domainUsers = await _userManager.Users.ToListAsync();

            foreach (var user in domainUsers)
            {
                returnUsers.Add(new AppUser()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.Firstname,
                    LastName = user.Lastname
                });
            }
            return Ok(returnUsers);
        }
        
        // GET: api/Admin/UserRoles
        /// <summary>
        /// Get users in role
        /// </summary>
        /// <param name="role">Role name</param>
        /// <returns>List of user's in role</returns>
        [HttpGet("{role}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AppUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UserRoles(string role)
        {
            List<AppUser> returnUsers = new();
            
            var users = await _userManager.GetUsersInRoleAsync(role);

            foreach (var user in users)
            {
                returnUsers.Add(new AppUser()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.Firstname,
                    LastName = user.Lastname
                });
            }
            return Ok(returnUsers);
        }
        
        // POST: api/Admin/Roles
        /// <summary>
        /// Add a new role
        /// </summary>
        /// <param name="role">Role that is being added</param>
        /// <returns>Added Role</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Roles(AppRoleAdd role)
        {
            var domainRole = new Domain.App.Identity.AppRole()
            {
                Name = role.Name
            };

            await _roleManager.CreateAsync(domainRole);
            _logger.LogInformation("Role created");
            
            return Ok(role.Name);
        }
        
        // DELETE: api/Admin/DeleteRole/abc
        /// <summary>
        /// Delete an existing Role based on name
        /// </summary>
        /// <param name="name">Name of the Role</param>
        /// <returns>No content</returns>
        [HttpDelete("{name}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteRole(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role == null)
            {
                _logger.LogWarning("Role not found");
                return NotFound(new Message($"{name} not Found!"));
            }

            await _roleManager.DeleteAsync(role);
            _logger.LogInformation("Role deleted");

            return Ok(name);
        }

        // POST: api/Admin/UserRoles/abc
        /// <summary>
        /// Add user to a role
        /// </summary>
        /// <param name="userRole">User email and role name</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UserRoles(UserRole userRole)
        {
            var appRole = await _roleManager.FindByNameAsync(userRole.RoleName);

            if (appRole == null)
            {
                _logger.LogWarning("Role not found");
                return NotFound(new Message($"{userRole.RoleName} not Found!"));
            }
            
            _logger.LogInformation("Role was found");

            var user = await _userManager.FindByEmailAsync(userRole.UserEmail);
            _logger.LogInformation("Found user");

            await _userManager.AddToRoleAsync(user, appRole.Name);

            return Ok(userRole.UserEmail);
        }
        
        // DELETE: api/DeleteUserRole/abc
        /// <summary>
        /// Delete User from role
        /// </summary>
        /// <param name="userRole">User's email and role's name</param>
        /// <returns>No content</returns>
        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUserRole(UserRole userRole)
        {
            var user = await _userManager.FindByEmailAsync(userRole.UserEmail);
            if (user == null)
            {
                _logger.LogWarning("User not found");
                return NotFound(new Message($"{userRole.UserEmail} not Found!"));
            }
            _logger.LogInformation("Found user");
            
            await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
            _logger.LogInformation("User removed from role");

            return NoContent();
        }
    }
}