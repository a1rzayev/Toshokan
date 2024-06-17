using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Services;
public interface IIdentityService
{
    User? Login(LoginDto loginDto);
    Task<Guid> Registration(RegistrationDto registrationDto);
    Task<string> GetRole(Guid userId);
}
