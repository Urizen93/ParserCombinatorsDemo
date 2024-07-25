namespace ParserCombinators.Server.DataLayer;

public sealed class UserEntity
{
    public int ID { get; set; }

    public string? Name { get; set; }

    public DateOnly Birthday { get; set; }

    public bool IsPremium { get; set; }

    public HashSet<UserRoleEntity> Roles { get; } = [];

    public HashSet<UserToUserRoleEntity> UserToUserRole { get; } = [];

    public const int AdminID = 1;
    public const int ManagerID = 2;
    public const int MultiRoleID = 3;
    public const int UnderAgedID = 4;
    public const int ElderlyID = 5;
    public const int PremiumID = 6;
}