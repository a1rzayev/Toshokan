namespace ToshokanApp.Repositories.Base;

public interface IDeleteAsync<TEntity>
{
    Task DeleteAsync(int entity);
}
