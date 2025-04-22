
//Inject it in Program.cs like this:
//await DbSeeder.SeedAsync(app.Services);
public class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        // 1. Create Roles
        List<string> roles = new List<string> { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Create a user
        var user = await userManager.FindByEmailAsync("<The_Email>");
        if (user == null)
        {
            user = new User
            {
                UserName = "<The_UserName>",
                Email = "<The_Email>",
                FirstName = "<The_FirstName>",
                LastName = "<The_LastName>",
                DateOfBirth = new DateOnly(1994, 6, 20),
                EmailConfirmed = true // 3. Mark as verified
            };
            await userManager.CreateAsync(user, "<The_Password>");
        }

        if (user != null)
        {
            // 4. Assign roles
            foreach (var role in new List<string> { "Admin", "User" })
            {
                if (!await userManager.IsInRoleAsync(user, role))
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

    }
}