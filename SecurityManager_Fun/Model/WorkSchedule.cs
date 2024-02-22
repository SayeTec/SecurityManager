namespace SecurityManager_Fun.Model
{
    public class WorkSchedule
    {
        public int ID { get; set; }
        public DateTime Day { get; set; }
        public TimeOnly StartTime { get; set; } 
        public int WorkHours { get; set; } 
        public int EmployeeID { get; set; }

        // Navigation property
        public Employee Employee { get; set; }
    }
}
