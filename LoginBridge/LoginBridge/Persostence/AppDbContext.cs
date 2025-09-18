using LoginBridge.Entities;
using LoginBridge.Persostence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LoginBridge.Api.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<LoginProvider> LoginProviders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
    }
}
