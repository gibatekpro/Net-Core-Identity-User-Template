using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class DefaultDbContext : IdentityDbContext<User, IdentityRole, string>
{
    //DbSet for each entity
    //public DbSet<YourEntity> YourEntities { get; set; } = null!;

    //Constructor
    //You can add any additional configuration here if needed
    //IdentityDbContext inherits from DbContext
    //In Program.Cs, you can register this as DbContextFactory, to have more control
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
    {
    }

    
}