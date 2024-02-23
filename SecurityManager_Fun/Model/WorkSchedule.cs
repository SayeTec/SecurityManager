namespace SecurityManager_Fun.Model
{
    public class WorkSchedule
    {
        public int ID { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan StartTime { get; set; } 
        public int WorkHours { get; set; } 
        public int EmployeeID { get; set; }

        // Navigation property
        public Employee Employee { get; set; }

        public string WorkScheduleViewForEmployee
        {
            get 
            { 
                DateTime date = new DateTime(Day.Year, Day.Month, Day.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds);
                return $"Date: {date.ToString("dd.MM.yy")} Start time: {date.ToString("HH:mm")} Hours: {WorkHours}";
            }
        }

        public override string ToString()
        {
            return $"Start time: {StartTime.ToString(@"hh\:mm")} Hours: {WorkHours} Employee: {Employee.FullName}";
        }
    }
}
