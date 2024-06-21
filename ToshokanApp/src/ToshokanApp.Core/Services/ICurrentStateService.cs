namespace ToshokanApp.Core.Services;
public interface ICurrentStateService 
{
    Guid CurrentBookId { get; set; }
    Guid CurrentUserId { get; set; }
}