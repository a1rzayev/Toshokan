using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Controllers;
public class IdentityController : Controller
{
    private readonly IIdentityService identityService;

    private readonly IDataProtector dataProtector;
    public IdentityController(IIdentityService identityService, IDataProtectionProvider dataProtectionProvider)
    {
        this.identityService = identityService;
        this.dataProtector = dataProtectionProvider.CreateProtector("identity");
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

        var foundUser = this.identityService.Login(loginDto);
        if (foundUser == null)
        {
            base.TempData["error"] = "Incorrect login or password!";
            return base.RedirectToRoute("LoginView", new
            {
                loginDto.ReturnUrl
            });
        }
        var hashedUserId = this.dataProtector.Protect(foundUser.Id.ToString());

        //base.HttpContext.Response.Cookies.Append("Authentication", hashedUserId);

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
        //HttpContext.Items["CurrentUserId"] = foundUser.Id;

        base.HttpContext.Response.Cookies.Append("CurrentUserId", foundUser.Id.ToString());
        // ViewData["currentUserId"] = foundUser.Id;

        return base.RedirectToAction(controllerName: "Book", actionName: "Index");
    }

    [HttpGet]
    [Authorize()]
    [Route("/api/[controller]/[action]", Name = "LogoutEndpoint")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {

        base.HttpContext.Response.Cookies.Delete("CurrentUserId");
        await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //base.HttpContext.Response.Cookies.Delete("Authentication");
        //await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return base.RedirectToRoute("LoginView", new
        {
            ReturnUrl
        });

        //return base.RedirectToRoute("LoginView");
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
            var userId = await this.identityService.Registration(registrationDto);

            if (avatar == null){}
            var extension = new FileInfo(avatar.FileName).Extension[1..];
            using var newFileStream = System.IO.File.Create($"Assets/Avatars/{userId}.{extension}");
            await avatar.CopyToAsync(newFileStream);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return base.RedirectToRoute("RegistrationView");
        }
        return base.RedirectToRoute("LoginView");
    }


    // [HttpPost]
    // [AllowAnonymous]
    // public IActionResult RequestTobeWriter(){
    //     var rtbw = new RequestTobeWriter{
    //         MotivationalLetter = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
    //         ExperienceYears = 3,
    //         Portfolio = "https://example.com/your-portfolio"
    //     };
    //     return base.View(rtbw);
    // }


    [HttpPost]
    [Route("/[controller]/[action]", Name = "BuyBookEndpoint")]
    [Authorize()]
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

            //var extension = new FileInfo(avatar.FileName).Extension[1..];
            //using var newFileStream = System.IO.File.Create($"Assets/Avatars/{userId}.{extension}");
            //await avatar.CopyToAsync(newFileStream);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }
        return base.RedirectToAction("Index", "Book", new
        {
            ReturnUrl
        });
    }


    [HttpPost]
    [Route("/[controller]/[action]", Name = "AddtoWishlistBookEndpoint")]
    [Authorize("UserAccess")]
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

            //var extension = new FileInfo(avatar.FileName).Extension[1..];
            //using var newFileStream = System.IO.File.Create($"Assets/Avatars/{userId}.{extension}");
            //await avatar.CopyToAsync(newFileStream);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }
        return base.RedirectToAction("Index", "Book", new
        {
            ReturnUrl
        });
    }
    [HttpGet]
    [Route("/[controller]/[action]")]
    [Authorize()]
    public async Task<ActionResult> MyAccount()
    {
       
        Guid userId;
        var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];
        Guid.TryParse(hashedSenderId, out userId);
        var user = await identityService.MyAccount(userId);

        return View(user);
    }
}

