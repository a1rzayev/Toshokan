using Microsoft.Extensions.Options;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
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

    public async Task Registration(RegistrationDto registrationDto)
    {
        await identityRepository.Registration(registrationDto);
    }
}