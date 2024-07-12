using ToshokanApp.Models;

namespace ToshokanAp.Models;

public class UserBook
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; }
}