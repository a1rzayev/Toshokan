using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;

namespace ToshokanApp.Core.Repositories;
public interface IBookCommentRepository : IGetAllAsync<BookComment>, IAddAsync<BookComment>, IDeleteAsync<BookComment> { 
    
}