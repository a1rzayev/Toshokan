namespace ToshokanApp.Repositories.Base;

public interface IDeleteAsync<TEntity>
{
    Task DeleteAsync(Guid entity);
}
