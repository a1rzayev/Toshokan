using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Repositories;
public interface IIdentityRepository : IDeleteAsync<User>, IGetAllAsync<User> { 
    Task<User?> Login(LoginDto loginDto);
    Task<Guid?> Registration(RegistrationDto registrationDto);

    Task<string> GetRole(Guid Id);
    
    Task BanAsync(Guid id);
    Task PromoteAdminAsync(Guid id);
    Task BuyBook(Guid userId, Guid bookId);
    Task AddtoWishlistBook(Guid userId, Guid bookId);
    
    Task SendUserRequest(UserRequest userRequest);
    Task RemovefromWishlistBook(Guid userId, Guid bookId);
    
    Task<User?> GetByIdAsync(Guid userId);

    Task<bool> HasPendingRequest(Guid userId);
    Task UpdateAvatarUrlAsync(Guid userId, string avatarUrl);

    //Task<bool> ConfirmEmailAsync(User user, string code);
}