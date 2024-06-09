namespace ToshokanApp.Repositories.Base.Functions;

public interface IDeleteAsync<TEntity>
{
    Task DeleteAsync(Guid entity);
}
