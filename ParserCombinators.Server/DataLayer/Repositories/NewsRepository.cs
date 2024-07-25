using Microsoft.EntityFrameworkCore;

namespace ParserCombinators.Server.DataLayer.Repositories;

public sealed class NewsRepository : INewsRepository
{
    private readonly NewsContext _context;

    public NewsRepository(NewsContext context) => _context = context;

    public async Task<IEnumerable<NewsEntity>> Get() =>
        await _context.News.AsNoTracking().ToArrayAsync();
}