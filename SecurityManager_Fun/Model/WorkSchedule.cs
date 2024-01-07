namespace SecurityManager_Fun.Model
{
    public class WorkSchedule
    {
        public int ID { get; set; }
        public DateTime Day { get; set; }
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

        // Navigation property
        public Employee Employee { get; set; }
    }
}
