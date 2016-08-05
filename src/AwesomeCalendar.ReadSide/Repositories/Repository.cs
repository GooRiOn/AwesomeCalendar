using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.ReadSide;

namespace AwesomeCalendar.ReadSide.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IInternalEntity
    {
        protected IQueryable<TEntity> Query => Context.Set<TEntity>().AsNoTracking();

        ReadSideContext Context { get; }

        protected Repository(ReadSideContext readSideContext)
        {
            Context = readSideContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TEntity entity)
        {
            var softDeletableEntity = entity as ISoftDeletable;

            if (softDeletableEntity == null)
                throw new InvalidOperationException("Entity is not soft deletable");

            softDeletableEntity.SoftDelete();
            await Context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = Query.FirstOrDefault(e => e.Id == id);

            if(entity == null)
                throw new ArgumentException("Entity does not exist");

            await SoftDeleteAsync(entity);
        }
    }
}