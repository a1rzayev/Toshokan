namespace ToshokanApp.Repositories.Base.Functions;
public interface IGetByNameAsync<TEntity>
{
    Task<IEnumerable<TEntity>?> GetByNameAsync(string name);
}
