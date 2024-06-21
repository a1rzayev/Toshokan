using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Models;
using ToshokanApp.Infrastructure.Repositories.EfCore;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using Xunit;

namespace ToshokanApp.test.ToshokanApp.Infrastructure.UnitTests.Repositories;
public class BookEfCoreRepositoryUnitTests
{
    private readonly DbContextOptions<ToshokanDbContext> _options;

    public BookEfCoreRepositoryUnitTests()
    {
        _options = new DbContextOptionsBuilder<ToshokanDbContext>()
            .UseInMemoryDatabase(databaseName: "ToshokanDb")
            .Options;
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewBook()
    {
        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new BookEfCoreRepository(context);
            var newBook = new Book { Id = Guid.NewGuid(), Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Test Book" };

            await repository.AddAsync(newBook);
        }

        using (var context = new ToshokanDbContext(_options))
        {
            Assert.Equal("Test Book", context.Books.First().Name);
        }
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllBooks()
    {
        using (var context = new ToshokanDbContext(_options))
        {
            context.Books.Add(new Book { Id = Guid.NewGuid(), Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Book 1" });
            context.Books.Add(new Book { Id = Guid.NewGuid(), Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Book 2" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new BookEfCoreRepository(context);
            var books = await repository.GetAllAsync();

            Assert.NotNull(books);
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnBookWithGivenId()
    {
        var bookId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Books.Add(new Book { Id = bookId, Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Test Book" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new BookEfCoreRepository(context);
            var book = await repository.GetByIdAsync(bookId);

            Assert.NotNull(book);
            Assert.Equal(bookId, book.Id);
        }
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnBooksWithGivenName()
    {
        using (var context = new ToshokanDbContext(_options))
        {
            context.Books.Add(new Book { Id = Guid.NewGuid(), Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Test Book" });
            context.Books.Add(new Book { Id = Guid.NewGuid(), Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Another Book" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new BookEfCoreRepository(context);
            var books = await repository.GetByNameAsync("Test Book");

            Assert.NotNull(books);
            Assert.Equal("Test Book", books.First().Name);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveBookWithGivenId()
    {
        var bookId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Books.Add(new Book { Id = bookId, Author = "Author", Description = "Description", Genre = "Genre", Language = "En", PublicationYear = 1999, Name = "Test Book" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new BookEfCoreRepository(context);
            await repository.DeleteAsync(bookId);
        }

        using (var context = new ToshokanDbContext(_options))
        {
            Assert.Null(await context.Books.FindAsync(bookId));
        }
    }
    

    [Fact]
    public async Task GetComments_ShouldReturnCommentsForGivenBookId()
    {
        var bookId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Comments.Add(new Comment { Id = Guid.NewGuid(), BookId = bookId, Text = "Great book!" });
            context.Comments.Add(new Comment { Id = Guid.NewGuid(), BookId = bookId, Text = "Interesting read." });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new BookEfCoreRepository(context);
            var comments = await repository.GetComments(bookId);

            Assert.NotNull(comments);
        }
    }
}
