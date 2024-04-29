namespace ToshokanApp.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using ToshokanApp.Models;
using ToshokanApp.Services.Base;

public class BookCommentService : IBookCommentService
{
    private readonly IBookCommentService bookRepository;
    public BookCommentService(IBookCommentService bookRepository)
    {
        this.bookRepository = bookRepository;
    }
    public async Task AddAsync(BookComment comment)
    {
        await this.bookRepository.AddAsync(comment);
    }

    public async Task DeleteAsync(int commentId)
    {
        await this.bookRepository.DeleteAsync(commentId);
    }

    public async Task<IEnumerable<BookComment>?> GetAllAsync(int bookId)
    {
        return await this.bookRepository.GetAllAsync(bookId);
    }
}