﻿// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootyLeague.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using FootyLeague.API.ViewModels.Administration.Users;
    using FootyLeague.Common;
    using FootyLeague.Data.Models;
    using FootyLeague.Services.Data;
    using FootyLeague.Services.Data.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdminService _adminService;

        public UsersController(
                                            UserManager<ApplicationUser> userManager,
                                            IAdminService adminService)
        {
            _userManager = userManager;
            _adminService = adminService;
        }

        // GET: api/users
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var users = await _adminService.GetAllUsersAsync();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET api/users/bc719f0c-ad53-4d35-8bc4-b511dd94dc07
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _adminService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // Patch api/users/bc719f0c-ad53-4d35-8bc4-b511dd94dc07
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Patch(string id, [FromBody] List<string> roles)
        {
            var filteredRoleList = await _adminService.FilterRolesThatExistsAsync(roles);

            if (!filteredRoleList.Any())
            {
                return BadRequest();
            }

            var user = await _userManager.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var existingRoles = await _userManager.GetRolesAsync(user);

            var checkUnique = await _adminService.FilterRolesThatAreNotAlreadySetAsync(filteredRoleList, user);

            if (!checkUnique.Any())
            {
                return BadRequest();
            }

            // Add new roles
            var result = await _userManager.AddToRolesAsync(user, checkUnique);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var userViewModel = await _adminService.GetUserAsync(id);

            return Ok(userViewModel);
        }

        // DELETE api/users/bc719f0c-ad53-4d35-8bc4-b511dd94dc07
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(string id, [FromBody] List<string> roles)
        {
            var filteredRoleList = await _adminService.FilterRolesThatExistsAsync(roles);

            if (!filteredRoleList.Any())
            {
                return BadRequest();
            }

            var user = await _userManager.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var existingRoles = await _userManager.GetRolesAsync(user);

            var rolesToRemove = filteredRoleList.Intersect(existingRoles);

            if (!rolesToRemove.Any())
            {
                return NotFound();
            }

            var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var userViewModel = await _adminService.GetUserAsync(id);

            return Ok(userViewModel);

        }
    }
}
