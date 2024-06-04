using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using ToshokanApp.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Repositories.EfCore;

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