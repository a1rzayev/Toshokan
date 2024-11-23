namespace ToshokanApp.Core.Repositories.Functions;

public interface IDeleteAsync<TEntity>
{
    Task DeleteAsync(Guid entity);
}
