using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;

namespace ToshokanApp.Infrastructure.Controllers;

public class HomeController : Controller
{

    private readonly IDataProtector dataProtector;
    private readonly string identityConnectionString;
    public HomeController(IConfiguration configuration, IDataProtectionProvider dataProtectionProvider)
    {
        identityConnectionString = configuration.GetConnectionString("MsSql") ?? throw new ArgumentNullException("Identity connection string");
        this.dataProtector = dataProtectionProvider.CreateProtector("identity");
    }

    public async Task<IActionResult> Index()
    {
        var authenticationHashedValue = base.HttpContext.Request.Cookies["Authentication"];

        if(string.IsNullOrWhiteSpace(authenticationHashedValue) == false) {
            var authenticationValue = this.dataProtector.Unprotect(authenticationHashedValue);

            if(Guid.TryParse(authenticationValue, out Guid userId)) {
                var connection = new SqlConnection(identityConnectionString);

                var foundUser = await connection.QueryFirstOrDefaultAsync<User>(
                    sql: "select * from Users where [Id] = @Id",
                    param: new {
                        Id = userId
                    }
                );

                return View(foundUser);
            }
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
