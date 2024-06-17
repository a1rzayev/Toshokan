namespace ToshokanApp.Core.Repositories.Functions;
public interface IGetByIdAsync<TEntity>
{
    Task<TEntity?> GetByIdAsync(Guid id);
}
