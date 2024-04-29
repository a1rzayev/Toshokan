namespace ToshokanApp.Repositories;
using ToshokanApp.Repositories.Base;
using ToshokanApp.Models;
public interface IBookCommentRepository : IGetAllAsync<BookComment>, ICreateAsync<BookComment>, IDeleteAsync<BookComment> { }