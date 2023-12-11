using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Model
{
    internal class WorkSchedule
    {
        public int ID { get; set; }
        public DateOnly Day { get; set; }
        public int WorkHours { get; set; } //TODO: Needs to be regulated, is not it just StartTime and EndTime
        public int EmployeeID { get; set; }
        public WorkDayType DayType { get; set; }

        public enum WorkDayType
        {
            None = 0,
            Work = 1,
            Illness = 2,
            Vacation = 3
        }

    }
}
