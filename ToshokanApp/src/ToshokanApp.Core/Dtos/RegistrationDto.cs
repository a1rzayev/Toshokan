#pragma warning disable CS8618

namespace ToshokanApp.Core.Dtos;

public class RegistrationDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}