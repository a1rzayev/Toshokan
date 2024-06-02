namespace ToshokanApp.Repositories.Base.Functions;
public interface IAddAsync<TEntity>
{
    Task AddAsync(TEntity entity);
}
