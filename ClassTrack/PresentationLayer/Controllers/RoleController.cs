﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassTrack.PresentationLayer.Controllers
{
    [Route("classtrack/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("create/{roleName}")]
        [Authorize("Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok("Role Created Successfully");
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("getall")]
        [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpDelete("delete/{roleName}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound("Role Not Found");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok("Role Deleted Successfully");
            }
            return BadRequest(result.Errors);
        }
    }
}
