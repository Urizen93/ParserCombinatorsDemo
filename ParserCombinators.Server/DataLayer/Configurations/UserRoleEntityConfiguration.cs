using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static ParserCombinators.Server.DataLayer.UserRoleEntity;

namespace ParserCombinators.Server.DataLayer.Configurations;

public sealed class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.HasKey(x => x.ID);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles)
            .UsingEntity<UserToUserRoleEntity>(x => x
                .HasOne(manyToMany => manyToMany.UserRole)
                .WithMany(manyToMany => manyToMany.UserToUserRole)
                .HasForeignKey(manyToMany => manyToMany.UserRoleID));

        builder.HasData(
            new UserRoleEntity { ID = AdminRoleID, Name = "Admin" },
            new UserRoleEntity { ID = CustomerRoleID, Name = "Customer" },
            new UserRoleEntity { ID = ManagerRoleID, Name = "Manager" });
    }
}