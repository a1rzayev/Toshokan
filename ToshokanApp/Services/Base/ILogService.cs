using ToshokanApp.Models;

namespace ToshokanApp.Services.Base;
public interface ILogService
{
    Task AddAsync(Log newLog);
}
