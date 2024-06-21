using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface ILogService
{
    Task AddAsync(Log newLog);
}
