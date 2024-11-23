namespace ToshokanApp.Core.Repositories;
public interface IEmailRepository { 
    Task VerifyEmail(Guid userId);
}