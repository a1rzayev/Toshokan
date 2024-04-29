using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;

namespace ToshokanApp.Repositories;

public class BookCommentJsonRepository : IBookCommentRepository
{
    private readonly string path = "Resources/booksComments.json";
    private readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
    private string? bookCommentsJson = string.Empty;
    private List<BookComment> bookComments = Enumerable.Empty<BookComment>().ToList();

    public BookCommentJsonRepository() { }

    public async Task<IEnumerable<BookComment>?> GetAllAsync()
    {
        try
        {
            bookCommentsJson = await File.ReadAllTextAsync(path);
            bookComments = JsonSerializer.Deserialize<List<BookComment>>(bookCommentsJson, options) ?? Enumerable.Empty<BookComment>().ToList();
        }
        catch (Exception)
        {
            throw new Exception();
        }

        return bookComments;
    }

    public async Task AddAsync(BookComment newBookComment)
    {
        try
        {
            bookCommentsJson = await File.ReadAllTextAsync(path);
            bookComments = JsonSerializer.Deserialize<List<BookComment>>(bookCommentsJson, options) ?? Enumerable.Empty<BookComment>().ToList();
        }
        catch (Exception)
        {
            throw new Exception();
        }

        bookComments.Add(newBookComment);

        var resultBookCommentsJson = JsonSerializer.Serialize<List<BookComment>>(bookComments, options);
        await File.WriteAllTextAsync(path, resultBookCommentsJson);
    }

    public async Task DeleteAsync(int bookCommentId)
    {
        try
        {
            bookCommentsJson = await File.ReadAllTextAsync(path);
            bookComments = JsonSerializer.Deserialize<List<BookComment>?>(bookCommentsJson, options) ?? Enumerable.Empty<BookComment>().ToList();
        }
        catch (Exception)
        {
            throw new Exception();
        }

        bookComments.RemoveAt(bookCommentId);

        var resultBookCommentsJson = JsonSerializer.Serialize<List<BookComment>?>(bookComments, options);
        await File.WriteAllTextAsync(path, resultBookCommentsJson);
    }
}