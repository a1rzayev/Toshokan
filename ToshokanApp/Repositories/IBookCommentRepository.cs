
using ToshokanApp.Repositories.Base;
using ToshokanApp.Models;

namespace ToshokanApp.Repositories;
public interface IBookCommentRepository : IGetAllAsync<BookComment>, IAddAsync<BookComment>, IDeleteAsync<BookComment> { 

}