namespace ToshokanApp.Core.Services;

public interface IEmailTemplateService
{
    string GenerateVerificationEmailTemplate(string verificationCode, string userName);
    string GenerateWelcomeEmailTemplate(string userName);
} 