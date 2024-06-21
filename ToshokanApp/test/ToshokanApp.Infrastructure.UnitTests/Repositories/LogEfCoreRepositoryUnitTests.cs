using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Models;
using ToshokanApp.Infrastructure.Repositories.EfCore;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using Xunit;

namespace ToshokanApp.test.ToshokanApp.Infrastructure.UnitTests.Repositories;


public class LogEfCoreRepositoryUnitTests
    {
        private readonly DbContextOptions<ToshokanDbContext> _options;

        public LogEfCoreRepositoryUnitTests()
        {
            _options = new DbContextOptionsBuilder<ToshokanDbContext>()
                .UseInMemoryDatabase(databaseName: "ToshokanDb")
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewLog()
        {
            using (var context = new ToshokanDbContext(_options))
            {
                var repository = new LogEfCoreRepository(context);
                var newLog = new Log
                {
                    Url = "http://example.com",
                    RequestBody = "Request Body",
                    ResponseBody = "Response Body",
                    CreationDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMinutes(1),
                    StatusCode = 200,
                    HttpMethod = "GET"
                };

                await repository.AddAsync(newLog);
            }

            using (var context = new ToshokanDbContext(_options))
            {
                Assert.Equal(1, await context.Logs.CountAsync());
                var log = await context.Logs.FirstAsync();
                Assert.Equal("http://example.com", log.Url);
                Assert.Equal("Request Body", log.RequestBody);
                Assert.Equal("Response Body", log.ResponseBody);
                Assert.Equal(200, log.StatusCode);
                Assert.Equal("GET", log.HttpMethod);
            }
        }
    }