namespace ParserCombinators.Server.DataLayer.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> Get();

    Task<UserEntity?> Get(int id);
}