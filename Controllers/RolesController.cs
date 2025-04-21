using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly IRolesService _rolesService;

    // Inject the service interface instead of using RoleManager directly
    public RolesController(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }

    /// <summary>
    /// Creates a new role.
    /// Only accessible by users with the Admin role.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        return await _rolesService.CreateRoleAsync(roleName);
    }

    /// <summary>
    /// Assigns an existing role to a specific user.
    /// Only accessible by Admins.
    /// </summary>
    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
    {
        return await _rolesService.AssignRoleAsync(model);
    }
}