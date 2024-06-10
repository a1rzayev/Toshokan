using ToshokanApp.Models;
using ToshokanApp.Repositories.Base.Functions;

namespace ToshokanApp.Repositories.Base;
public interface IBookRepository : IGetAllAsync<Book>, IAddAsync<Book>, IGetByNameAsync<Book> { 
    
}