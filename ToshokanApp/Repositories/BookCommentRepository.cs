using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;

namespace ToshokanApp.Repositories;

public class BookCommentRepository : IBookCommentRepository
{
    private readonly string connectionString;
    private readonly IDbConnection dbConnection;

    public BookCommentRepository(IDbConnection dbConnection, IOptionsSnapshot<string> connectionStrings) {
        this.dbConnection = dbConnection;   
        this.connectionString = connectionStrings.Value; 
    }

    public async Task<IEnumerable<BookComment>?> GetAllAsync()
    {
        string query = "SELECT * FROM MyEntity";
        return await dbConnection.QueryAsync<BookComment>(query);
    }

    public async Task AddAsync(BookComment newBookComment)
    {
        string query = "INSERT INTO MyEntity (Name, Description) VALUES (@Name, @Description)";
        await dbConnection.ExecuteAsync(query, newBookComment);
    }

    public async Task DeleteAsync(int bookCommentId)
    {
        string query = "DELETE FROM MyEntity WHERE Id = @Id";
        await dbConnection.ExecuteAsync(query, new { Id = bookCommentId });
    }
}