using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;
public class CurrentStateService : ICurrentStateService 
{
    public Guid CurrentBookId { get; set; }
    public Guid CurrentUserId { get; set; }
}
