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
        HttpContext.Items["CurrentUserId"] = foundUser.Id;
        // ViewData["currentUserId"] = foundUser.Id;

        return base.RedirectToAction(controllerName: "Book", actionName: "Index");
    }

    [HttpGet]
    [Authorize()]
    [Route("/api/[controller]/[action]", Name = "LogoutEndpoint")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //base.HttpContext.Response.Cookies.Delete("Authentication");
        //await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


        ViewData["currentUserId"] = null;
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
            
            //var extension = new FileInfo(avatar.FileName).Extension[1..];
            //using var newFileStream = System.IO.File.Create($"Assets/Avatars/{userId}.{extension}");
            //await avatar.CopyToAsync(newFileStream);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return base.RedirectToRoute("RegistrationView");
        }
        return base.RedirectToRoute("LoginView");
    }

    [HttpDelete]
    [Authorize("RequireAdminAccess")]
    [Route("/api/[controller]/[action]", Name = "DeleteEndpoint")]
    public async Task<IActionResult> Delete(Guid id){
        if (ModelState.IsValid)
        {
            await this.identityService.DeleteAsync(id);
            return base.RedirectToAction("Book", "Index");
        }

        return Forbid();
    }
}

