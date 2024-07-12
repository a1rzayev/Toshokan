using ToshokanApp.Models;
using ToshokanApp.Repositories.Base.Functions;

namespace ToshokanApp.Repositories.Base;
public interface IBookCommentRepository : IGetAllAsync<BookComment>, IAddAsync<BookComment>, IDeleteAsync<BookComment> { 
    
}