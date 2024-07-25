using Microsoft.EntityFrameworkCore;

namespace ParserCombinators.Server.DataLayer.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly NewsContext _context;

    public UserRepository(NewsContext context) =>
        _context = context;

    public async Task<IEnumerable<UserEntity>> Get() =>
        await _context.Users
            .AsNoTracking()
            .Include(user => user.Roles)
            .ToArrayAsync();

    public async Task<UserEntity?> Get(int id) =>
        await _context.Users
            .AsNoTracking()
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.ID == id);
}