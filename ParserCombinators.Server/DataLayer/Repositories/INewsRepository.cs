namespace ParserCombinators.Server.DataLayer.Repositories;

public interface INewsRepository
{
    Task<IEnumerable<NewsEntity>> Get();
}