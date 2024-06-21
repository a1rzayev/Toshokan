using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;

namespace ToshokanApp.Core.Repositories;
public interface IAdminRepository : IGetAllAsync<User>, IDeleteAsync<Comment>, IDeleteAsync<Book>, IDeleteAsync<User> { 
    
}