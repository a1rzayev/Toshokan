using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IBookRepository adminRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public AdminService(IAdminRepository bookRepository, IOptionsSnapshot<string> connectionString)
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
}