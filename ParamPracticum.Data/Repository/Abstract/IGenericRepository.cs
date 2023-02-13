namespace ParamPracticum.Data.Repository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int entityId);
        Task InsertAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
