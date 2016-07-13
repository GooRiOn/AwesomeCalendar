using System;

namespace AwesomeCalendar.Infrastructure.Interfaces.ReadSide
{
    public interface IRepository<in TEntity> where TEntity : class, IInternalEntity
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void SoftDelete(TEntity entity);

        void SoftDelete(Guid id);
    }
}