using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class RolesService : IRolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RolesService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Creates a new role if it doesn't already exist.
    /// </summary>
    public async Task<IActionResult> CreateRoleAsync(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
        {
            return new BadRequestObjectResult("Role already exists.");
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        return result.Succeeded ? new OkObjectResult("Role created.") : new BadRequestObjectResult(result.Errors);
    }

    /// <summary>
    /// Assigns an existing role to a user.
    /// </summary>
    public async Task<IActionResult> AssignRoleAsync(AssignRoleDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return new NotFoundObjectResult("User not found.");
        }

        var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
        if (!roleExists)
        {
            return new NotFoundObjectResult("Role not found.");
        }

        var result = await _userManager.AddToRoleAsync(user, model.RoleName);
        return result.Succeeded ? new OkObjectResult("Role assigned.") : new BadRequestObjectResult(result.Errors);
    }
}