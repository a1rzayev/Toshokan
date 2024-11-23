using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class LogService : ILogService
{
    private readonly ILogRepository logRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public LogService(ILogRepository logRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.logRepository = logRepository;
    }

    public async Task AddAsync(Log newLog)
    {
        await this.logRepository.AddAsync(newLog);
    }
}