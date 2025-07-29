using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

public class ToshokanDbContextFactory : IDesignTimeDbContextFactory<ToshokanDbContext>
{
    public ToshokanDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ToshokanDbContext>();
        var connectionString = configuration.GetConnectionString("Postgres");

        builder.UseNpgsql(connectionString);

        return new ToshokanDbContext(builder.Options);
    }
}
