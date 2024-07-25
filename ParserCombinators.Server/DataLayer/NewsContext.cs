using Microsoft.EntityFrameworkCore;
using ParserCombinators.Server.DataLayer.Configurations;

namespace ParserCombinators.Server.DataLayer;

public sealed class NewsContext : DbContext
{
    public DbSet<NewsEntity> News => Set<NewsEntity>();
    
    public DbSet<UserEntity> Users => Set<UserEntity>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseInMemoryDatabase("News");

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder
        .ApplyConfiguration(new NewsEntityConfiguration())
        .ApplyConfiguration(new UserEntityConfiguration())
        .ApplyConfiguration(new UserRoleEntityConfiguration())
        .ApplyConfiguration(new UserToUserRoleEntityConfiguration());
}