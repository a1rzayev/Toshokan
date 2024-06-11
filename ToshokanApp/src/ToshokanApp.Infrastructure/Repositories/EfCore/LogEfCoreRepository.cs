using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class LogEfCoreRepository : ILogRepository
{
    private readonly ToshokanDbContext dbContext;
    public LogEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(Log newLog)
    {
        await dbContext.Logs.AddAsync(newLog);
        await dbContext.SaveChangesAsync();
    }
}