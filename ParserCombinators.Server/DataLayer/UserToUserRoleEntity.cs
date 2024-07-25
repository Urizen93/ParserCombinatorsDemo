namespace ParserCombinators.Server.DataLayer;

public sealed class UserToUserRoleEntity
{
    public int UserID { get; set; }

    public int UserRoleID { get; set; }

    public UserEntity? User { get; set; }

    public UserRoleEntity? UserRole { get; set; }
}