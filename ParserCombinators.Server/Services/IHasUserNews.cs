using ParserCombinators.Model;

namespace ParserCombinators.Server.Services;

public interface IHasUserNews
{
    Task<IEnumerable<PieceOfNews>> Get(int userID);
}