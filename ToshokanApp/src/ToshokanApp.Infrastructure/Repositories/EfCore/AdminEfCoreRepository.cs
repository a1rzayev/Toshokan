using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class AdminEfCoreRepository : IAdminRepository
{
    private readonly ToshokanDbContext dbContext;

    public AdminEfCoreRepository()
    {
    }

    public AdminEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task DeleteAsync(Guid entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>?> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}