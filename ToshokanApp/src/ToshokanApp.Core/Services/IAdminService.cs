using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface IAdminService
{
    Task<IEnumerable<User>?> GetAllUsersAsync();
    Task DeleteUser(Guid id);
    Task DeleteBook(Guid id);
    Task DeleteComment(Guid id);
}
