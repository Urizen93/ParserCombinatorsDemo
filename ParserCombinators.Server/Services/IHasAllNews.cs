using ParserCombinators.Model;

namespace ParserCombinators.Server.Services;

public interface IHasAllNews
{
    Task<IEnumerable<PieceOfNews>> GetAll();
}