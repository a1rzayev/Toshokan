using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public IActionResult Login()
    {
        var errorMessage = base.TempData["error"];
        if (errorMessage != null)
        {
            base.ModelState.AddModelError("All", errorMessage.ToString()!);
        }

        return base.View();
    }

    [HttpPost]
    [Route("/api/[controller]/[action]", Name = "LoginEndpoint")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {

        var foundUser = this.identityService.Login(loginDto);
        if (foundUser == null)
        {
            base.TempData["error"] = "Incorrect login or password!";
            return base.RedirectToRoute("LoginView");
        }
        var hashedUserId = this.dataProtector.Protect(foundUser.Id.ToString());

        base.HttpContext.Response.Cookies.Append("Authentication", hashedUserId);

        var claims = new Claim[] {
                new(ClaimTypes.Email, foundUser.Email),
                new(ClaimTypes.Name, foundUser.Name),
                new("id", hashedUserId),
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return base.RedirectToAction(controllerName: "Home", actionName: "Index");
    }

    [HttpGet]
    [Route("/api/[controller]/[action]", Name = "LogoutEndpoint")]
    public async Task<IActionResult> Logout()
    {

        base.HttpContext.Response.Cookies.Delete("Authentication");

        return base.RedirectToRoute("LoginView");
    }

    [Route("/[controller]/[action]", Name = "RegistrationView")]
    public IActionResult Registration()
    {
        if (TempData["error"] != null)
        {
            ModelState.AddModelError("All", "Something went wrong. Please try again");
            System.Console.WriteLine(TempData["error"]);
        }

        return base.View();
    }

    [HttpPost]
    [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto)
    {
        try
        {
            await this.identityService.Registration(registrationDto);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return base.RedirectToRoute("RegistrationView");
        }

        return base.RedirectToRoute("LoginView");
    }
}