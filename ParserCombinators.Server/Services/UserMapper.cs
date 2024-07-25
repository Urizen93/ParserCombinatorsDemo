using Microsoft.FSharp.Collections;
using ParserCombinators.Model;
using ParserCombinators.Server.DataLayer;

namespace ParserCombinators.Server.Services;

public sealed class UserMapper : IMapUsers
{
    public User Map(UserEntity userEntity) => new(
        userEntity.Name ?? throw new ArgumentException("Must have a name!", nameof(userEntity)),
        userEntity.IsPremium,
        GetAge(userEntity.Birthday, DateTime.Today),
        SetModule.OfSeq(userEntity.Roles.Select(role => role.Name)));

    private static int GetAge(DateOnly birthday, DateTime byDate)
    {
        var age = byDate.Year - birthday.Year;
    
        return birthday.ToDateTime(TimeOnly.MinValue) > byDate.AddYears(-age)
            ? age - 1
            : age;
    }
}