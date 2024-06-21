using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class BookEfCoreRepository : IBookRepository
{
    private readonly ToshokanDbContext dbContext;
    public BookEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(Book newBook)
    {
        await dbContext.Books.AddAsync(newBook);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>?> GetAllAsync()
    {
        return dbContext.Books;
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return dbContext.Books.FirstOrDefault(book => book.Id == id);
    }

    public async Task<IEnumerable<Book>?> GetByNameAsync(string name)
    {
        return dbContext.Books.Where(book => book.Name == name);
    }
    
    public async Task DeleteAsync(Guid id)
{
    var book = await dbContext.Books.FirstOrDefaultAsync(c => c.Id == id);
    if (book != null)
    {
        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
    }
}

    public IEnumerable<CommentDto>? GetComments(Guid id){
        var comments = dbContext.Comments.Where(bookComment => bookComment.BookId == id).ToList();
        var commentDtos = new List<CommentDto>();
        foreach (var item in comments)
        {

            commentDtos.Add(new CommentDto{
                Id = item.Id,
                BookId = item.BookId,
                SenderId = item.SenderId,
                Text = item.Text,
                SenderName = dbContext.Users.FirstOrDefault(user => user.Id == item.SenderId)?.Name,
                SenderSurname = dbContext.Users.FirstOrDefault(user => user.Id == item.SenderId)?.Surname,
            });
        }
        return commentDtos;
    }
}