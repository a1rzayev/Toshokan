using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Dtos;
using ToshokanApp.Models;
using ToshokanApp.Repositories.Base.Functions;

namespace ToshokanApp.Repositories.Base;
public interface IIdentityRepository { 
    User? Login(LoginDto loginDto);
    Task Registration(RegistrationDto registrationDto);
}