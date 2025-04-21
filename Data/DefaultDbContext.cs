using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class DefaultDbContext : IdentityDbContext<IdentityUser>
{
    //IdentityDbContext inherits from DbContext
    //In Program.Cs, you can register this as DbContextFactory, to have more control
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
    {
    }

    
}