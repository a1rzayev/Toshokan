namespace ToshokanApp.Core.Repositories.Functions;
public interface IGetByIdAsync<TEntity>
{
    Task<IEnumerable<TEntity>?> GetByIdAsync(Guid id);
}
