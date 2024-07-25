namespace ParserCombinators.Server.DataLayer;

public sealed class UserRoleEntity
{
    public int ID { get; set; }

    public string? Name { get; set; }
    
    public HashSet<UserEntity> Users { get; } = [];
    
    public HashSet<UserToUserRoleEntity> UserToUserRole { get; } = [];
    
    public const int AdminRoleID = 1;
    public const int CustomerRoleID = 2;
    public const int ManagerRoleID = 3;
}