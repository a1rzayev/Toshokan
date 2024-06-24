using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository adminRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public AdminService(IAdminRepository adminRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.adminRepository = adminRepository;
    }

    public Task DeleteBook(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteComment(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>?> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task AcceptUserRequest(Guid requestId){
        await adminRepository.AcceptUserRequest(requestId);
    }

    public async Task RejectUserRequest(Guid requestId){
        await adminRepository.RejectUserRequest(requestId);
    }

    public async Task<IEnumerable<UserRequest>?> GetAllUserRequestsAsync(){
        return await adminRepository.GetAllUserRequestsAsync();
    }
}