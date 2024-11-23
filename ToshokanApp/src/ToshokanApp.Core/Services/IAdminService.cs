using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface IAdminService
{
    Task<IEnumerable<User>?> GetAllUsersAsync();
    Task DeleteUser(Guid id);
    Task DeleteBook(Guid id);
    Task DeleteComment(Guid id);
    
    Task<IEnumerable<UserRequest>?> GetAllUserRequestsAsync();
    Task AcceptUserRequest(Guid requestId);
    Task RejectUserRequest(Guid requestId);
}
