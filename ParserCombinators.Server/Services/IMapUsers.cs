using ParserCombinators.Model;
using ParserCombinators.Server.DataLayer;

namespace ParserCombinators.Server.Services;

public interface IMapUsers
{
    User Map(UserEntity userEntity);
}