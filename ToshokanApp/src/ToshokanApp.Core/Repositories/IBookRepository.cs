using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;

namespace ToshokanApp.Core.Repositories;
public interface IBookRepository : IGetAllAsync<Book>, IAddAsync<Book>, IGetByNameAsync<Book> , IGetByIdAsync<Book>, IDeleteAsync<Book> {
    IEnumerable<CommentDto>? GetComments(Guid id) ;
    
}