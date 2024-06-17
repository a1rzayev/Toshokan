using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace ToshokanApp.Infrastructure.Controllers;

[Authorize(Roles = "Admin")]

public class AdminController : Controller
{

    private readonly string identityConnectionString;
    private readonly IBookService bookService;
    public AdminController(IConfiguration configuration, IBookService bookService)
    {
        this.bookService = bookService;
        identityConnectionString = configuration.GetConnectionString("MsSql") ?? throw new ArgumentNullException("Identity connection string");
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult GetUsers(){
        return base.View();
    }

    public IActionResult GetBooks(){
        return base.View(this.bookService.GetAllAsync());
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
