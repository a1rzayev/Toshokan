using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Resources;

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

    public async Task<Guid> Registration(RegistrationDto registrationDto)
    {
        var userId = new Guid();
        var user = new User
        {
            Id = userId,
            Name = registrationDto.Name,
            Surname = registrationDto.Surname,
            Email = registrationDto.Email,
            Password = registrationDto.Password
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.UserRoles.AddAsync(new UserRole
        {
            UserId = user.Id,
            Role = "User"
        });
        await dbContext.SaveChangesAsync();
        return userId;
    }

    public async Task<string> GetRole(Guid userId){
        return dbContext.UserRoles.FirstOrDefault(x => x.UserId == userId).Role;
    }
}