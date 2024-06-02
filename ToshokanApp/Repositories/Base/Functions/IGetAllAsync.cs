namespace ToshokanApp.Repositories.Base.Functions;
public interface IGetAllAsync<TEntity>
{
    Task<IEnumerable<TEntity>?> GetAllAsync();
}
