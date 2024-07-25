using ParserCombinators.Model;

namespace ParserCombinators.Server.Services;

public interface IHasUser
{
    Task<User?> Get(int userID);
}