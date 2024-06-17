using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class BookCommentService : IBookCommentService
{
    private readonly IBookCommentRepository bookCommentRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public BookCommentService(IBookCommentRepository bookCommentRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.bookCommentRepository = bookCommentRepository;
    }
    public async Task AddAsync(BookComment comment)
    {
        await this.bookCommentRepository.AddAsync(comment);
    }

    public async Task DeleteAsync(Guid commentId)
    {
        await this.bookCommentRepository.DeleteAsync(commentId);
    }

    public async Task<IEnumerable<BookComment>?> GetAllAsync()
    {
        return await this.bookCommentRepository.GetAllAsync();
    }

    public async Task<IEnumerable<BookComment>?> GetByIdAsync(Guid bookId){
        return await this.bookCommentRepository.GetByIdAsync(bookId);
    }
}