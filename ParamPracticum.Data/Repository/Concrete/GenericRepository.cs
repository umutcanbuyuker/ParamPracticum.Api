using Microsoft.EntityFrameworkCore;
using ParamPracticum.Data.Context;
using ParamPracticum.Data.Repository.Abstract;

namespace ParamPracticum.Data.Repository.Concrete
{
    public class GenericRepositoryz<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected readonly AppDbContext Context;
        private DbSet<Entity> entities;
        public GenericRepositoryz(AppDbContext dbContext)
        {
            this.Context = dbContext;
            this.entities = Context.Set<Entity>();
        }
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int entityId)
        {
            return await entities.AsNoTracking().Where(entity => EF.Property<int>(entity, "Id").Equals(entityId)).SingleOrDefaultAsync();
        }

        public async Task InsertAsync(Entity entity)
        {
            await entities.AddAsync(entity);
        }

        public void RemoveAsync(Entity entity)
        {
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }

        public void Update(Entity entity)
        {
            entities.Update(entity);
        }
    }
}
