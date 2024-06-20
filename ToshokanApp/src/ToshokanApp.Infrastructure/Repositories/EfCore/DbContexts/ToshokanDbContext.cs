using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToshokanApp.Core.Models;

namespace ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

public class ToshokanDbContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

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





        modelBuilder.Entity<Comment>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<Comment>()
        .Property(c => c.BookId)
        .IsRequired();

        modelBuilder.Entity<Comment>()
        .Property(c => c.SenderId)
        .IsRequired();

        modelBuilder.Entity<Comment>()
        .Property(c => c.Text)
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



        
        modelBuilder.Entity<User>()
        .HasKey(c => c.Id);

        modelBuilder.Entity<User>()
        .Property(c => c.Name)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<User>()
        .Property(c => c.Surname)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<User>()
        .Property(c => c.Email)
        .HasMaxLength(100)
        .IsRequired();

        modelBuilder.Entity<User>()
        .Property(c => c.Password)
        .HasMaxLength(100)
        .IsRequired();

        modelBuilder.Entity<User>()
        .Property(c => c.AvatarPath);

        modelBuilder.Entity<User>()
        .Property(c => c.PurchasedBooks);

        modelBuilder.Entity<User>()
        .Property(c => c.WishList);





        modelBuilder.Entity<UserRole>()
        .HasKey(c => c.UserId);

        modelBuilder.Entity<UserRole>()
        .Property(c => c.Role)
        .IsRequired();
    }
}