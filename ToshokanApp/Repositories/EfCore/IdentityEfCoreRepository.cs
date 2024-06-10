using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using ToshokanApp.Repositories.EfCore.DbContexts;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Dtos;

namespace ToshokanApp.Repositories.EfCore;

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