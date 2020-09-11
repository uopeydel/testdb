using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCodeDb.Service
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime ThNow()
        {
            return DateTime.UtcNow.AddHours(7);
        }
    }

    public interface IDateTimeService
    {
        DateTime ThNow();
    }
}
