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


public ActionResult GetMessage()
    {
        ViewBag.Message = "Hello from the controller!";
        return View("Index");
    }   
     public async Task<IActionResult> Index()
    {
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
