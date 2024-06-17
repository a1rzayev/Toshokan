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
    private readonly IIdentityService identityService;
    public AdminController(IConfiguration configuration, IBookService bookService, IIdentityService identityService)
    {
        this.bookService = bookService;
        this.identityService = identityService;
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

    public async Task<IActionResult> GetUsers()
    {
        var users = await this.identityService.GetAllAsync();
        return base.View(users);
    }

    public IActionResult GetBooks()
    {
        return base.View(this.bookService.GetAllAsync());
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost]
    [Route("[controller]/[action]/{id}", Name = "BanUserEndpoint")]
    public async Task<IActionResult> BanUser(Guid id)
    {
        if (ModelState.IsValid)
        {
            await this.identityService.BanAsync(id);
            return base.RedirectToAction("GetUsers");
        }

        return Forbid();
    }


    [HttpPost]
    [Route("[controller]/[action]/{id}", Name = "PromoteAdminEndpoint")]
    public async Task<IActionResult> PromoteAdmin(Guid id)
    {
        if (ModelState.IsValid)
        {
            await this.identityService.PromoteAdminAsync(id);
            return base.RedirectToAction("GetUsers");
        }

        return Forbid();
    }

    [HttpDelete]
    [Route("[controller]/[action]", Name = "DeleteUserEndpoint")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await this.identityService.DeleteAsync(id);
        return base.RedirectToAction("GetUsers");
    }

}
