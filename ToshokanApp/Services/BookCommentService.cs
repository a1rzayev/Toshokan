using Microsoft.Extensions.Options;
using ToshokanApp.Models;
using ToshokanApp.Repositories;
using ToshokanApp.Services.Base;

namespace ToshokanApp.Services;

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
}