using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using ToshokanApp.Core.Services;
using ToshokanApp.Core.Repositories;

public class EmailService : IEmailService
{
    private readonly IEmailRepository emailRepository;
    private readonly IConfiguration configuration;

    public EmailService(IEmailRepository emailRepository, IConfiguration configuration)
    {
        this.emailRepository = emailRepository;
        this.configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var smtpClient = new SmtpClient(configuration["Smtp:Host"])
        {
            Port = int.Parse(configuration["Smtp:Port"]),
            Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(configuration["Smtp:Username"]),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
    public string GenerateVerificationCode()
    {
        Random random = new Random();
        int randomNumber = random.Next(100000, 1000000);
        return randomNumber.ToString();

    }

    public async Task VerifyEmail(Guid userId){
        await this.emailRepository.VerifyEmail(userId);
    }

}
