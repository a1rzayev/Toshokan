using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Dtos;
using ToshokanApp.Models;

namespace ToshokanApp.Services.Base;
public interface IIdentityService
{
    User? Login(LoginDto loginDto);
    Task Registration(RegistrationDto registrationDto);
}
