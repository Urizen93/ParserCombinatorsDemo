using ParserCombinators.Model;

namespace ParserCombinators.Frontend.Services;

public interface IHasNews
{
    Task<IReadOnlyCollection<PieceOfNews>> Get();
    
    Task<IReadOnlyCollection<PieceOfNews>> GetForUser(int userID);
}