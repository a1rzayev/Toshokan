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

    private readonly IBookService bookService;
    private readonly IIdentityService identityService;
    private readonly IAdminService adminService;
    public AdminController(IConfiguration configuration, IBookService bookService, IIdentityService identityService, IAdminService adminService)
    {
        this.bookService = bookService;
        this.identityService = identityService;
        this.adminService = adminService;
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
        Guid bookId;
        var hashedBookId = base.HttpContext.Request.Cookies["CurrentBookId"];

        if (string.IsNullOrWhiteSpace(hashedBookId) == false)
        {
            Guid.TryParse(hashedBookId, out bookId);
            ViewBag.CurrentUserId = bookId;
        }
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


    [HttpGet]
    [ActionName("BanUser")]
    public async Task<IActionResult> BanUser(Guid id)
    {
        await this.identityService.BanAsync(id);
        return base.RedirectToAction("GetUsers");

    }


    [HttpGet]
    [ActionName("PromoteAdmin")]
    public async Task<IActionResult> PromoteAdmin(Guid id)
    {
        await this.identityService.PromoteAdminAsync(id);
        return base.RedirectToAction("GetUsers");

    }

    [HttpGet]
    [ActionName("DeleteUser")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await this.identityService.DeleteAsync(id);
        return base.RedirectToAction("GetUsers");
    }

    [HttpGet]
    [ActionName("AcceptRequest")]
    public async Task<IActionResult> AcceptRequest(Guid id)
    {
        await this.adminService.AcceptUserRequest(id);
        return base.RedirectToAction("GetRequests");
    }

    [HttpGet]
    [ActionName("RejectRequest")]
    public async Task<IActionResult> RejectRequest(Guid id)
    {
        await this.adminService.RejectUserRequest(id);
        return base.RedirectToAction("GetRequests");
    }

    [HttpGet]
    [ActionName("GetRequests")]
    public async Task<IActionResult> GetRequests()
    {
        return base.View(await this.adminService.GetAllUserRequestsAsync());
    }
}
