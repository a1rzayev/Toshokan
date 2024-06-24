using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ToshokanApp.Core.Dtos;
using System.Security.Cryptography;
using ToshokanApp.Core.Services;
using System.Text;
using ToshokanApp.Core.Models;
using ToshokanApp.Infrastructure.Services;

namespace ToshokanApp.Infrastructure.Controllers;
public class IdentityController : Controller
{
    private readonly IIdentityService identityService;
    private readonly IBookService bookService;

    private readonly IConfiguration avatarDirConfiguration;

    private readonly IDataProtector dataProtector;
    public IdentityController(IIdentityService identityService, IDataProtectionProvider dataProtectionProvider, IConfiguration avatarDirConfiguration, IBookService bookService)
    {
        this.identityService = identityService;
        this.dataProtector = dataProtectionProvider.CreateProtector("identity");
        this.avatarDirConfiguration = avatarDirConfiguration;
        this.bookService = bookService;
    }

    [Route("/[controller]/[action]", Name = "LoginView")]
    [AllowAnonymous]
    public IActionResult Login(string? ReturnUrl)
    {
        var errorMessage = base.TempData["error"];
        ViewBag.ReturnUrl = ReturnUrl;
        if (errorMessage != null)
        {
            base.ModelState.AddModelError("All", errorMessage.ToString()!);
        }

        return base.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/api/[controller]/[action]", Name = "LoginEndpoint")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        loginDto.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(loginDto.Password));
        var foundUser = await this.identityService.Login(loginDto);
        if (foundUser == null)
        {
            base.TempData["error"] = "Incorrect login or password!";
            return base.RedirectToRoute("LoginView", new
            {
                loginDto.ReturnUrl
            });
        }
        var hashedUserId = this.dataProtector.Protect(foundUser.Id.ToString());

        var claims = new Claim[] {
                new(ClaimTypes.Email, foundUser.Email),
                new(ClaimTypes.Name, foundUser.Name),
                new("Id", hashedUserId),
                new(ClaimTypes.Role, await this.identityService.GetRole(foundUser.Id)),
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        if (string.IsNullOrWhiteSpace(loginDto.ReturnUrl) == false)
        {
            return base.Redirect(loginDto.ReturnUrl);
        }

        base.HttpContext.Response.Cookies.Append("CurrentUserId", foundUser.Id.ToString());
        return base.RedirectToAction(controllerName: "Book", actionName: "Index");
    }

    [HttpGet]
    [Authorize()]
    [Route("/api/[controller]/[action]", Name = "LogoutEndpoint")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {

        base.HttpContext.Response.Cookies.Delete("CurrentUserId");
        await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return base.RedirectToRoute("LoginView", new
        {
            ReturnUrl
        });

    }

    [Route("/[controller]/[action]", Name = "RegistrationView")]
    [AllowAnonymous]
    public IActionResult Registration()
    {
        if (TempData["error"] != null)
        {
            ModelState.AddModelError("All", TempData["error"].ToString());
            System.Console.WriteLine(TempData["error"]);
        }

        return base.View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto, IFormFile avatar)
    {
        try
        {
            registrationDto.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(registrationDto.Password));
            var userId = await this.identityService.Registration(registrationDto);
            if (userId == null) throw new Exception("This email is already using by the other user");

            if (avatar == null)
            {
                var defaultAvatarPath = $"{avatarDirConfiguration["StaticFileRoutes:Assets"]}/Default.jpg";

                var extension = Path.GetExtension(defaultAvatarPath);
                using var defaultAvatarFileStream = System.IO.File.OpenRead(defaultAvatarPath);
                using var newFileStream = System.IO.File.Create($"{avatarDirConfiguration["StaticFileRoutes:Avatars"]}{userId}{extension}");
                await defaultAvatarFileStream.CopyToAsync(newFileStream);

            }
            else
            {
                var extension = Path.GetExtension(avatar.FileName);
                using var newFileStream = System.IO.File.Create($"{avatarDirConfiguration["StaticFileRoutes:Avatars"]}{userId}{extension}");
                await avatar.CopyToAsync(newFileStream);
            }
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return base.RedirectToRoute("RegistrationView");
        }

        return base.RedirectToRoute("LoginView");
    }

    [HttpPost]
    [Authorize()]
    [Route("/[controller]/[action]", Name = "BuyBookEndpoint")]
    public async Task<IActionResult> BuyBook(string? ReturnUrl)
    {
        try
        {
            Guid userId;
            var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];
            Guid.TryParse(hashedSenderId, out userId);

            Guid bookId;
            var hashedBookId = base.HttpContext.Request.Cookies["CurrentBookId"];
            Guid.TryParse(hashedBookId, out bookId);
            await this.identityService.BuyBook(userId, bookId);
            return RedirectToAction("GetById", "Book", new { id = bookId });
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }
        return base.RedirectToAction("Index", "Book");
    }


    [HttpPost]
    [Authorize()]
    [Route("/[controller]/[action]", Name = "AddtoWishlistBookEndpoint")]
    public async Task<IActionResult> AddtoWishlistBook(string? ReturnUrl)
    {
        try
        {
            Guid userId;
            var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];
            Guid.TryParse(hashedSenderId, out userId);

            Guid bookId;
            var hashedBookId = base.HttpContext.Request.Cookies["CurrentBookId"];
            Guid.TryParse(hashedBookId, out bookId);
            await this.identityService.AddtoWishlistBook(userId, bookId);
            return RedirectToAction("GetById", "Book", new { id = bookId });
        }
        
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }
        return base.RedirectToAction("Index", "Book");
    }


    [HttpPost]
    [Authorize()]
    [Route("/[controller]/[action]", Name = "RemovefromWishlistBookEndpoint")]
    public async Task<IActionResult> RemovefromWishlistBook(string? ReturnUrl)
    {
        try
        {
            Guid userId;
            var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];
            Guid.TryParse(hashedSenderId, out userId);

            Guid bookId;
            var hashedBookId = base.HttpContext.Request.Cookies["CurrentBookId"];
            Guid.TryParse(hashedBookId, out bookId);
            await this.identityService.RemovefromWishlistBook(userId, bookId);
            return RedirectToAction("GetById", "Book", new { id = bookId });
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            System.Console.WriteLine(TempData["error"]);
        }
        return base.RedirectToAction("Index", "Book");
    }


    [HttpGet]
    [AllowAnonymous]
    [Route("[controller]/[action]/{id}")]
    [ActionName("GetById")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var user = await identityService.GetByIdAsync(id);
        Guid senderId;
        var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];

        if (string.IsNullOrWhiteSpace(hashedSenderId) == false)
        {

            Guid.TryParse(hashedSenderId, out senderId);
            if(senderId == id){
                ViewBag.IsCurrentAccount = true;
                ViewBag.HasPendingRequest = await identityService.HasPendingRequest(id);
            }
            else ViewBag.IsCurrentAccount = false;
        }
        else {
            ViewBag.IsCurrentAccount = false;
        }
        ViewBag.avatarDirPath = avatarDirConfiguration["StaticFileRoutes:Avatars"];
        ViewBag.avatarPath = ViewBag.avatarDirPath + user.Id;
        System.Console.WriteLine(ViewBag.avatarPath);

        List<Book> purchasedBooks = new List<Book>();
        foreach (var bookId in user.PurchasedBooks)
        {
            purchasedBooks.Add(await this.bookService.GetByIdAsync(bookId));
        }
        List<Book> wishList = new List<Book>();
        foreach (var bookId in user.WishList)
        {
            wishList.Add(await this.bookService.GetByIdAsync(bookId));
        }
        ViewBag.PurchasedBooks = purchasedBooks;
        ViewBag.WishList = wishList;

        return base.View(user);
    }


    [HttpGet]
    [Authorize()]
    [ActionName("RequestWriter")]
    public async Task<IActionResult> RequestWriter(Guid id)
    {
        if (ModelState.IsValid)
        {
            var userRequest = new UserRequest{UserId = id, Role = "Writer"};
            await identityService.SendUserRequest(userRequest);
        }
        return base.RedirectToAction("GetById", new {id = id});
    }
}

