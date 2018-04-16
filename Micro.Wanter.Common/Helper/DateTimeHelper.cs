using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Wanter.Common.Helper
{
    public class DateTimeHelper
    {
        public static bool DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            double d = (ts2 - ts1).TotalMinutes;
            int ts = int.Parse(Math.Floor(d).ToString());
            if (ts > 30)
                return true;
            else
                return false;
        }
    }
}
