using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using ToshokanApp.Core.Dtos;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class IdentityEfCoreRepository : IIdentityRepository
{
    private readonly ToshokanDbContext dbContext;
    public IdentityEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public User? Login(LoginDto loginDto)
    {
        return dbContext.Users.FirstOrDefault(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
    }

    public async Task Registration(RegistrationDto registrationDto)
    {
        await dbContext.Users.AddAsync(new User{ Id = new Guid(),
                                      Name = registrationDto.Name,
                                      Surname = registrationDto.Surname,
                                      Email = registrationDto.Email,
                                      Password = registrationDto.Password});
        await dbContext.SaveChangesAsync();
    }
}