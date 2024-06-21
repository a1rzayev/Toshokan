namespace ToshokanApp.Core.Repositories.Functions;
public interface IGetAllAsync<TEntity>
{
    Task<IEnumerable<TEntity>?> GetAllAsync();
}
