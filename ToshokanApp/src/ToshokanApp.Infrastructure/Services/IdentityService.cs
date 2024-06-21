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
    public async Task BanAsync(Guid id)
    {
        await this.identityRepository.BanAsync(id);
    }
    public async Task PromoteAdminAsync(Guid id)
    {
        await this.identityRepository.PromoteAdminAsync(id);
    }
    public async Task<Guid> Registration(RegistrationDto registrationDto)
    {
        return await identityRepository.Registration(registrationDto);
    }
    public async Task<string> GetRole(Guid userId){
        return await identityRepository.GetRole(userId);
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await identityRepository.GetAllAsync();
    }

    public async Task BuyBook(Guid userId, Guid bookId){
        await identityRepository.BuyBook(userId, bookId);
    }

    public async Task AddtoWishlistBook(Guid userId, Guid bookId){
        await identityRepository.AddtoWishlistBook(userId, bookId);
    }
    public async Task<User> MyAccount(Guid userId){
        return await identityRepository.MyAccount(userId);
    }
}