using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;

namespace ToshokanApp.Repositories;

public class BookCommentRepository : IBookCommentRepository
{
    private readonly IDbConnection dbConnection;

    public BookCommentRepository(IDbConnection dbConnection) {
        this.dbConnection = dbConnection;   
    }

    public async Task<IEnumerable<BookComment>?> GetAllAsync()
    {
        string query = "SELECT * FROM BookComments";
        return await dbConnection.QueryAsync<BookComment>(query);
    }

    public async Task AddAsync(BookComment newBookComment)
    {
        string query = "INSERT INTO BookComments (Id, BookId, SenderId, Comment) VALUES (@Id, @BookId, @SenderId, @Comment)";
        await dbConnection.ExecuteAsync(query, newBookComment);
    }

    public async Task DeleteAsync(int bookCommentId)
    {
        string query = "DELETE FROM BookComments WHERE Id = @Id";
        await dbConnection.ExecuteAsync(query, new { Id = bookCommentId });
    }
}