using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories.Functions;

namespace ToshokanApp.Core.Repositories;
public interface IBookRepository : IGetAllAsync<Book>, IAddAsync<Book>, IGetByNameAsync<Book> , IGetByIdAsync<Book> { 
    
}