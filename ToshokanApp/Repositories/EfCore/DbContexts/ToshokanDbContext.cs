using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToshokanApp.Models;
namespace ToshokanApp.Repositories.EfCore.DbContexts;


public class ToshokanDbContext : DbContext
{
    public DbSet<BookComment> BookComments { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Log> Logs { get; set; }

    public ToshokanDbContext(DbContextOptions<ToshokanDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Book>()
        .HasKey(g => g.Id);
        
        modelBuilder.Entity<Book>()
        .Property(g => g.Name)
        .HasMaxLength(100)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.Author)
        .HasMaxLength(100)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.PublicationYear)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.Genre)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.Language)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.Description)
        .HasMaxLength(1000)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.AddedBy)
        .IsRequired();

        modelBuilder.Entity<Book>()
        .Property(g => g.AddedDate)
        .IsRequired();


        modelBuilder.Entity<BookComment>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<BookComment>()
        .Property(c => c.BookId)
        .IsRequired();

        modelBuilder.Entity<BookComment>()
        .Property(c => c.SenderId)
        .IsRequired();

        modelBuilder.Entity<BookComment>()
        .Property(c => c.Comment)
        .HasMaxLength(1000)
        .IsRequired();



        modelBuilder.Entity<Log>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<Log>()
        .Property(c => c.RequestBody)
        .IsRequired();

        modelBuilder.Entity<Log>()
        .Property(e => e.Url)
        .IsRequired();

        modelBuilder.Entity<Log>()
        .Property(e => e.RequestBody)
        .IsRequired();
        
        modelBuilder.Entity<Log>()
        .Property(e => e.ResponseBody)
        .IsRequired();
        
        modelBuilder.Entity<Log>()
        .Property(e => e.CreationDate)
        .IsRequired();
        
        modelBuilder.Entity<Log>()
        .Property(e => e.EndDate)
        .IsRequired();
        
        modelBuilder.Entity<Log>()
        .Property(e => e.StatusCode)
        .IsRequired();
        
        modelBuilder.Entity<Log>()
        .Property(e => e.HttpMethod)
        .IsRequired();

    }
}