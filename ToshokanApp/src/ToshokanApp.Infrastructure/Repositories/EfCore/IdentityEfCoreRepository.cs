using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Resources;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


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
            Name = Regex.Replace(registrationDto.Name, @"^\w", m => m.Value.ToUpper()),
            Surname = Regex.Replace(registrationDto.Surname, @"^\w", m => m.Value.ToUpper()),
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
        return user.Id;
    }

    public async Task<string> GetRole(Guid userId)
    {
        return dbContext.UserRoles.FirstOrDefault(x => x.UserId == userId).Role;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(c => c.Id == id);
        if (user != null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return dbContext.Users;
    }

    public async Task BanAsync(Guid id)
    {
        var userroles = await dbContext.UserRoles.FirstOrDefaultAsync(c => c.UserId == id);
        if (userroles != null)
        {
            dbContext.UserRoles.FirstOrDefault(c => c.UserId == id).Role = "Banned";
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task PromoteAdminAsync(Guid id)
    {
        var userroles = await dbContext.UserRoles.FirstOrDefaultAsync(c => c.UserId == id);
        if (userroles != null)
        {
            dbContext.UserRoles.FirstOrDefault(x => x.UserId == id).Role = "Admin";
            await dbContext.SaveChangesAsync();
        }
    }


    public async Task BuyBook(Guid userId, Guid bookId)
    {
        var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
        if (user != null)
        {
            user.PurchasedBooks.Add(bookId);
            await dbContext.SaveChangesAsync();
        }
    }


    public async Task AddtoWishlistBook(Guid userId, Guid bookId)
    {
        var user = dbContext.Users.FirstOrDefault(x => x.Id == userId);
        if (user != null)
        {
            user.WishList.Add(bookId);
            await dbContext.SaveChangesAsync();
        }
    }
    
    public async Task<User?> GetByIdAsync(Guid userId)
    {        
        return dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }
}