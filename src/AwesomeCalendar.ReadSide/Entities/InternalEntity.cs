using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeCalendar.Infrastructure.Interfaces.ReadSide;

namespace AwesomeCalendar.ReadSide.Entities
{
    public class InternalEntity : IInternalEntity, ISoftDeletable
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; }
        public DateTime UpdateDate { get; private set; }
        public bool IsDeleted { get; private set; }

        public InternalEntity()
        {
            IsDeleted = false;
            CreatedDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }

        void ISoftDeletable.Delete()
        {
            IsDeleted = true;
        }

        void ISoftDeletable.SetUpdatedDate(DateTime updatedDate)
        {
            UpdateDate = updatedDate;
        }
    }
}
