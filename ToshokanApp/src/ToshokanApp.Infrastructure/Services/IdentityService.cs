using Microsoft.Extensions.Options;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Resources;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IIdentityRepository identityRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public IdentityService(IIdentityRepository identityRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.identityRepository = identityRepository;
    }
    public User? Login(LoginDto loginDto)
    {
        return identityRepository.Login(loginDto);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.identityRepository.DeleteAsync(id);
    }
    public async Task<Guid> Registration(RegistrationDto registrationDto)
    {
        return await identityRepository.Registration(registrationDto);
    }
    public async Task<string> GetRole(Guid userId){
        return await identityRepository.GetRole(userId);
    }
}