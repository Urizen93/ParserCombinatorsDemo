using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static ParserCombinators.Server.DataLayer.UserEntity;

namespace ParserCombinators.Server.DataLayer.Configurations;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<UserToUserRoleEntity>(x => x
                .HasOne(manyToMany => manyToMany.User)
                .WithMany(manyToMany => manyToMany.UserToUserRole)
                .HasForeignKey(manyToMany => manyToMany.UserID));
        
        builder.HasData(
            new UserEntity { ID = AdminID, Name = "Almighty Admin", IsPremium = false, Birthday = DateOnly.MinValue },
            new UserEntity { ID = ManagerID, Name = "Some Manager", IsPremium = false, Birthday = new DateOnly(1990, 1, 1) },
            new UserEntity { ID = MultiRoleID, Name = "All the roles", IsPremium = false, Birthday = new DateOnly(2000, 1, 1) },
            new UserEntity { ID = UnderAgedID, Name = "Young User", IsPremium = false, Birthday = new DateOnly(2010, 1, 1) },
            new UserEntity { ID = ElderlyID, Name = "Old User", IsPremium = false, Birthday = new DateOnly(1950, 1, 1) },
            new UserEntity { ID = PremiumID, Name = "Premium User", IsPremium = true, Birthday = new DateOnly(2000, 1, 1) });
    }
}