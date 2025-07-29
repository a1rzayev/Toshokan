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
    public Task<User?> Login(LoginDto loginDto)
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
    public async Task<Guid?> Registration(RegistrationDto registrationDto)
    {
        return await this.identityRepository.Registration(registrationDto);
    }
    public async Task<string> GetRole(Guid userId){
        return await this.identityRepository.GetRole(userId);
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await this.identityRepository.GetAllAsync();
    }

    public async Task BuyBook(Guid userId, Guid bookId){
        await this.identityRepository.BuyBook(userId, bookId);
    }

    public async Task AddtoWishlistBook(Guid userId, Guid bookId){
        await this.identityRepository.AddtoWishlistBook(userId, bookId);
    }
    public async Task SendUserRequest(UserRequest userRequest){
        await this.identityRepository.SendUserRequest(userRequest);
    }

    public async Task RemovefromWishlistBook(Guid userId, Guid bookId){
        await this.identityRepository.RemovefromWishlistBook(userId, bookId);
    }
    public async Task<User?> GetByIdAsync(Guid userId){
        return await this.identityRepository.GetByIdAsync(userId);
    }

    public async Task<bool> HasPendingRequest(Guid userId)
    {
        return await this.identityRepository.HasPendingRequest(userId);
    }

    public async Task UpdateAvatarUrlAsync(Guid userId, string avatarUrl)
    {
        await this.identityRepository.UpdateAvatarUrlAsync(userId, avatarUrl);
    }

    // public async Task<bool> ConfirmEmailAsync(Guid userId, string code)
    // {
    //     var user = await identityRepository.GetByIdAsync(userId);
    //     if (user == null)
    //     {
    //         return false;
    //     }
    //     return await identityRepository.ConfirmEmailAsync(user, code);
    // }
}