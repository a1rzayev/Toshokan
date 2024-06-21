using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Resources;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace ToshokanApp.test.ToshokanApp.Infrastructure.UnitTests.Repositories;

public class IdentityEfCoreRepositoryUnitTests
{
    private readonly DbContextOptions<ToshokanDbContext> _options;

    public IdentityEfCoreRepositoryUnitTests()
    {
        _options = new DbContextOptionsBuilder<ToshokanDbContext>()
            .UseInMemoryDatabase(databaseName: "ToshokanDb")
            .Options;
    }

    [Fact]
    public async Task GetRole_ShouldReturnUserRole_WhenUserIdIsValid()
    {
        var userId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Users.Add(new User { Id = userId, Name = "Name", Surname = "Surname", Email = "test@gmail.com", Password = "password" });
            context.UserRoles.Add(new UserRole { UserId = userId, Role = "User" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            IIdentityRepository repository = new IdentityEfCoreRepository(context);
            var role = await repository.GetRole(userId);
            Assert.Equal("User", role);
        }
    }
    [Fact]
    public async Task PromoteAdminAsync_ShouldUpdateUserRoleToAdmin_WhenUserIdIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Users.Add(new User { Id = userId, Name = "Name", Surname = "Surname", Email = "test@gmail.com", Password = "password" });
            context.UserRoles.Add(new UserRole { UserId = userId, Role = "User" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            IIdentityRepository repository = new IdentityEfCoreRepository(context);

            // Act
            await repository.PromoteAdminAsync(userId);

            // Assert
            var updatedUserRole = await context.UserRoles.FindAsync(userId);
            Assert.Equal("Admin", updatedUserRole.Role);
        }
    }

    [Fact]
    public async Task Login_ShouldReturnUser_WhenCredentialsAreCorrect()
    {
        var userId = Guid.NewGuid();
        var email = "test@gmail.com";
        var password = "password";

        using (var context = new ToshokanDbContext(_options))
        {
            context.Users.Add(new User { Id = userId, Name = "Name", Surname = "Surname", Email = email, Password = password });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            IIdentityRepository repository = new IdentityEfCoreRepository(context);

            var loginDto = new LoginDto { Email = email, Password = password };
            var user = repository.Login(loginDto); // Ensure Login method is async

            Assert.NotNull(user);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveUser_WhenUserIdIsValid()
    {
        var userId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Users.Add(new User { Id = userId, Name = "Name", Surname = "Surname", Email = "test@gmail.com", Password = "password" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            IIdentityRepository repository = new IdentityEfCoreRepository(context);

            await repository.DeleteAsync(userId);

            var deletedUser = await context.Users.FindAsync(userId);
            Assert.Null(deletedUser);
        }
    }

    [Fact]
    public async Task BanAsync_ShouldUpdateUserRoleToBanned_WhenUserIdIsValid()
    {
        var userId = Guid.NewGuid();
        using (var context = new ToshokanDbContext(_options))
        {
            context.Users.Add(new User { Id = userId, Name = "Name", Surname = "Surname", Email = "test@gmail.com", Password = "password" });
            context.UserRoles.Add(new UserRole { UserId = userId, Role = "User" });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            IIdentityRepository repository = new IdentityEfCoreRepository(context);

            await repository.BanAsync(userId);

            var updatedUserRole = await context.UserRoles.FindAsync(userId);
            Assert.Equal("Banned", updatedUserRole.Role);
        }
    }

    [Fact]
    public async Task Registration_ShouldAddUserAndUserRole()
    {
        var registrationDto = new RegistrationDto
        {
            Name = "Name",
            Surname = "Surname",
            Email = "test@gmail.com",
            Password = "password"
        };

        using (var context = new ToshokanDbContext(_options))
        {
            IIdentityRepository repository = new IdentityEfCoreRepository(context);

            var userId = await repository.Registration(registrationDto);

            var user = await context.Users.FindAsync(userId);
            var userRole = await context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);

            Assert.NotNull(user);
            Assert.Equal(registrationDto.Name, user.Name);
            Assert.Equal(registrationDto.Surname, user.Surname);
            Assert.Equal(registrationDto.Email, user.Email);
            Assert.Equal(registrationDto.Password, user.Password);

            Assert.NotNull(userRole);
            Assert.Equal(userId, userRole.UserId);
            Assert.Equal("User", userRole.Role);
        }
    }
}