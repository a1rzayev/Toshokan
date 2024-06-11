using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface IIdentityService
{
    User? Login(LoginDto loginDto);
    Task Registration(RegistrationDto registrationDto);
}
