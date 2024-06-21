using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;

namespace ToshokanApp.Core.Repositories;
public interface ICommentRepository : IGetAllAsync<Comment>, IAddAsync<Comment>, IDeleteAsync<Comment>, IGetByIdAsync<Comment> { 
    
}