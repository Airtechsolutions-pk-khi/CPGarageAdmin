using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Repositories
{
    public static class DateTimeUTC
    {
        public static DateTime Now { get { return DateTime.UtcNow.AddMinutes(180); } }
    }
}
