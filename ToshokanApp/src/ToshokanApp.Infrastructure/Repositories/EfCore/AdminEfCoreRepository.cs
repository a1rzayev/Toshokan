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

    public async Task AcceptUserRequest(Guid requestId)
    {
        var userRequest = dbContext.UserRequests.Find(requestId);
        if (userRequest != null)
        {
            var userroles = dbContext.UserRoles.FirstOrDefault(c => c.UserId == requestId);
            if (userroles != null)
            {
                dbContext.UserRoles.FirstOrDefault(c => c.UserId == requestId).Role = userRequest.Role;
                dbContext.UserRequests.Remove(userRequest);
                await dbContext.SaveChangesAsync();
            }
        }

    }
    public async Task RejectUserRequest(Guid requestId)
    {

        var userRequest = dbContext.UserRequests.Find(requestId);
        if (userRequest != null)
        {
            dbContext.UserRequests.Remove(userRequest);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserRequest>?> GetAllUserRequestsAsync()
    {
        return dbContext.UserRequests;
    }
}