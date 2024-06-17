using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Repositories;
public interface IIdentityRepository { 
    User? Login(LoginDto loginDto);
    Task<Guid> Registration(RegistrationDto registrationDto);

    Task<string> GetRole(Guid userId);
}