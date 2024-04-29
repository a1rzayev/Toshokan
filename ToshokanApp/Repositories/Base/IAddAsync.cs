namespace ToshokanApp.Repositories.Base;
public interface IAddAsync<TEntity>
{
    Task AddAsync(TEntity entity);
}
