using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Repositories;
public interface IIdentityRepository { 
    User? Login(LoginDto loginDto);
    Task Registration(RegistrationDto registrationDto);
}