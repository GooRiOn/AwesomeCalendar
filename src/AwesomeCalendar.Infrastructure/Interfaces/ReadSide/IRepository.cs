using System;
using System.Threading.Tasks;

namespace AwesomeCalendar.Infrastructure.Interfaces.ReadSide
{
    public interface IRepository<in TEntity> where TEntity : class, IInternalEntity
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
        
        Task SoftDeleteAsync(TEntity entity);
        
        Task SoftDeleteAsync(Guid id);
    }
}