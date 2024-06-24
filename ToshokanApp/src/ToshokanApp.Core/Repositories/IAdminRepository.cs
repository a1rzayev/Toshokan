using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;

namespace ToshokanApp.Core.Repositories;
public interface IAdminRepository : IDeleteAsync<Comment>, IDeleteAsync<Book>, IDeleteAsync<User> { 
    
    
    Task<IEnumerable<UserRequest>?> GetAllUserRequestsAsync();
    Task AcceptUserRequest(Guid requestId);
    Task RejectUserRequest(Guid requestId);
}