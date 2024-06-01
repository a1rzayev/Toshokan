using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ToshokanApp.Repositories.EfCore.DbContexts
{
    public class ToshokanDbContextFactory : IDesignTimeDbContextFactory<ToshokanDbContext>
    {
        public ToshokanDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ToshokanDbContext>();
            var connectionString = configuration.GetConnectionString("MsSql");

            builder.UseSqlServer(connectionString);

            return new ToshokanDbContext(builder.Options);
        }
    }
}
