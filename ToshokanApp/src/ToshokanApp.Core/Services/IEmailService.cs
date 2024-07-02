using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Services;
public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);

}
