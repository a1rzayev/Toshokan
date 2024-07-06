using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class EmailEfCoreRepository : IEmailRepository
{
    private readonly ToshokanDbContext dbContext;

    public EmailEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task VerifyEmail(Guid userId)
    {
        var user = await dbContext.Users.FindAsync(userId);
        if (user != null)
        {
            dbContext.Users.FirstOrDefault(c => c.Id == userId).EmailVerified = true;
            await dbContext.SaveChangesAsync();
        }
    }
}