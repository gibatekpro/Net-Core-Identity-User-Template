public interface IRolesService
{
    Task<IActionResult> CreateRoleAsync(string roleName);
    Task<IActionResult> AssignRoleAsync(AssignRoleDto model);
}