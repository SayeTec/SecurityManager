using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;
using System.Collections.Immutable;

namespace SecurityManager_Fun.Data.Repositories
{
    public class WorkScheduleRepository
    {
        public static List<WorkSchedule> GetAll()
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.WorkSchedules
                    .Include(w => w.Employee)
                        .ThenInclude(e => e.Department)
                    .ToList();
            }
        }

        public static List<WorkSchedule> GetWorkSchedulesForDate(DateTime date)
        {
            return GetAll().Where(work => work.Day.Date.Equals(date)).ToList();
        }

        public static List<WorkSchedule> GetWorkSchedulesForDateByDepartment(DateTime date, Department department)
        {
            return GetAll().Where(work => work.Day.Date.Equals(date) && (work.Employee.Department == null ? false : work.Employee.Department.Equals(department))).ToList();
        }

        public static List<WorkSchedule> GetWorkSchedulesForEmployeeByMonth(DateTime date, Employee employee)
        {
            return GetAll().Where(work => work.Day.Date.Year == date.Year && work.Day.Date.Month == date.Month && work.EmployeeID == employee.ID).ToList();
        }

        public static List<DateTime> GetWorkDatesForMonth(DateTime date)
        {
            return GetAll().Where(work => work.Day.Year == date.Year && work.Day.Month == date.Month)
                .Select(work => work.Day)
                .DistinctBy(work => work.Day)
                .ToList();
        }

        public static List<DateTime> GetWorkDatesForMonthByDepartment(DateTime date, Department department)
        {
            List<DateTime> dates = GetAll().Where(work => work.Day.Year == date.Year && work.Day.Month == date.Month && work.Employee.Department.Equals(department))
                .Select(work => work.Day)
                .DistinctBy(work => work.Day)
                .ToList();
            dates.Sort();
            return dates;
        }

        public static void AddNewWorkSchedule(WorkSchedule workSchedule)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.WorkSchedules.Add(workSchedule);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateWorkSchedule(WorkSchedule workSchedule)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.WorkSchedules.Update(workSchedule);
                dbContext.SaveChanges();
            }
        }

        public static void DeleteWorkSchedule(WorkSchedule workSchedule)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.WorkSchedules.Remove(workSchedule);
                dbContext.SaveChanges();
            }
        }

        public static bool CheckIfWorkScheduleExistForEmployeeByDate(DateTime date, Employee employee) 
        { 
            return GetAll().Where(work => work.EmployeeID == employee.ID && work.Day.Date.Equals(date)).Any();
        }

        public static WorkSchedule GetWorkScheduleForEmployeeByDate(DateTime date, Employee employee)
        {
            return GetAll().Where(work => work.Day.Date.Equals(date) && work.EmployeeID == employee.ID).FirstOrDefault();
        }

        public static int GetSumDaysInWorkForEmployeeByMonth(DateTime date, Employee employee)
        {
            return GetAll().Where(work => work.Day.Date.Year == date.Year && work.Day.Date.Month == date.Month && work.EmployeeID == employee.ID)
                .Count();
        }

        public static int GetSumHoursInWorkForEmployeeByMonth(DateTime date, Employee employee)
        {
            return GetAll().Where(work => work.Day.Date.Year == date.Year && work.Day.Date.Month == date.Month && work.EmployeeID == employee.ID)
                .Select(work => work.WorkHours)
                .Sum();
        }

        public static int GetSumEmployeesWithWorkForDepartment(DateTime date, Department department)
        {
            return GetAll().Where(work => work.Day.Date.Equals(date) && 
            EmployeeRepository.GetEmployeesByDepartment(department).Contains(work.Employee))
                .Count();
        }

        public static int GetSumHoursInWorkWithWorkForDepartment(DateTime date, Department department)
        {
            return GetAll().Where(work => work.Day.Date.Equals(date) &&
            EmployeeRepository.GetEmployeesByDepartment(department).Contains(work.Employee))
                .Select(work => work.WorkHours)
                .Sum();
        }
    }
}
