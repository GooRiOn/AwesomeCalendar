using System;
using System.ComponentModel.DataAnnotations;
using AwesomeCalendar.Infrastructure.Interfaces.ReadSide;

namespace AwesomeCalendar.ReadSide.Entities
{
    public abstract class InternalEntity : IInternalEntity, ISoftDeletable
    {
        [Key]
        public Guid Id { get; set; }

        public bool IsDeleted { get; private set; }

        protected InternalEntity()
        {
            IsDeleted = false;
        }

        void ISoftDeletable.SoftDelete()
        {
            IsDeleted = true;
        }
    }
}
