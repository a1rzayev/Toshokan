using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Dtos;
using ToshokanApp.Models;
using ToshokanApp.Services.Base;

namespace ToshokanApp.Controllers
{
    public class IdentityController : Controller
    {        
        private readonly IIdentityService identityService;
        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [Route("/[controller]/[action]", Name = "LoginView")]
        public IActionResult Login() {
            var errorMessage = base.TempData["error"];
            if(errorMessage != null) {
                base.ModelState.AddModelError("All", errorMessage.ToString()!);
            }

            return base.View();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "LoginEndpoint")]
        public async Task<IActionResult> Login([FromForm]LoginDto loginDto) {
            
            var foundUser = this.identityService.Login(loginDto);
            if (foundUser == null) {
                base.TempData["error"] = "Incorrect login or password!";
                return base.RedirectToRoute("LoginView");
            }
            base.HttpContext.Response.Cookies.Append("Authentication", foundUser.Id.ToString());
            return base.RedirectToAction(controllerName: "Home", actionName: "Index");
        }

        [Route("/[controller]/[action]", Name = "RegistrationView")]
        public IActionResult Registration() {
            if(TempData["error"] != null) {
                ModelState.AddModelError("All", "Something went wrong. Please try again");
                System.Console.WriteLine(TempData["error"]);
            }

            return base.View();
        }

        [HttpPost]
        [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
        public async Task<IActionResult> Registration([FromForm]RegistrationDto registrationDto) {
            try {
                await this.identityService.Registration(registrationDto);
            }
            catch(Exception ex) {
                TempData["error"] = ex.Message;
                return base.RedirectToRoute("RegistrationView");
            }

            return base.RedirectToRoute("LoginView");
        }
    }
}