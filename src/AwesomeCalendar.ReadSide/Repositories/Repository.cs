using System;
using System.Data.Entity;
using System.Linq;
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

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void SoftDelete(TEntity entity)
        {
            var softDeletableEntity = entity as ISoftDeletable;

            if (softDeletableEntity == null)
                throw new InvalidOperationException("Entity is not soft deletable");

            softDeletableEntity.SoftDelete();
            Context.SaveChanges();
        }

        public void SoftDelete(Guid id)
        {
            var entity = Query.FirstOrDefault(e => e.Id == id);

            if(entity == null)
                throw new ArgumentException("Entity does not exist");

            SoftDelete(entity);
        }
    }
}