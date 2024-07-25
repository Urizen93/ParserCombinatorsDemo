using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static ParserCombinators.Server.DataLayer.UserEntity;
using static ParserCombinators.Server.DataLayer.UserRoleEntity;

namespace ParserCombinators.Server.DataLayer.Configurations;

public sealed class UserToUserRoleEntityConfiguration : IEntityTypeConfiguration<UserToUserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserToUserRoleEntity> builder)
    {
        builder.HasKey(x => new { x.UserID, x.UserRoleID });

        builder.HasData(
            new UserToUserRoleEntity { UserID = AdminID, UserRoleID = AdminRoleID },
            new UserToUserRoleEntity { UserID = ManagerID, UserRoleID = ManagerRoleID },
            new UserToUserRoleEntity { UserID = MultiRoleID, UserRoleID = CustomerRoleID },
            new UserToUserRoleEntity { UserID = MultiRoleID, UserRoleID = ManagerRoleID },
            new UserToUserRoleEntity { UserID = MultiRoleID, UserRoleID = AdminRoleID },
            new UserToUserRoleEntity { UserID = UnderAgedID, UserRoleID = CustomerRoleID },
            new UserToUserRoleEntity { UserID = ElderlyID, UserRoleID = CustomerRoleID },
            new UserToUserRoleEntity { UserID = PremiumID, UserRoleID = CustomerRoleID });
    }
}