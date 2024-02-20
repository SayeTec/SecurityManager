using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data.Repositories
{
    public class EmployeeRepository
    {
        public static List<Employee> GetAllEmployees()
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Employees
                    .Include(e => e.Role)
                    .Include(e => e.Department)
                        .ThenInclude(d => d.Country)
                    .ToList();
            }
        }

        public static List<Employee> GetUnpaidEmployees()
        {
            using (var dbContext = new AppDBContext())
            {
                DateTime now = DateTime.Now;
                DateTime startDateOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1);

                return GetAllEmployees().Where(e =>
                    !dbContext.Payments.Any(p =>
                        p.EmployeeID == e.ID &&
                        p.DateOfCreation >= startDateOfMonth &&
                        p.DateOfCreation <= endDateOfMonth &&
                        (p.Status != Payment.StatusType.Canceled ||
                        p.Status == Payment.StatusType.Done)))
                    .ToList();

            }
        }

        public static List<Employee> GetEmployeesUnderPriority(int priority) 
        {
            return GetAllEmployees().Where(employee => employee.Role.Priority > priority).ToList();
        }

        public static Employee GetEmployeeByLogin(string login)
        {
            using (var dbContext = new AppDBContext())
            {
                return GetAllEmployees().SingleOrDefault(employee => employee.Login == login);
            }
        }

        public static List<Employee> GetEmployeesByDepartment(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                return GetAllEmployees().Where(employee => employee.Department?.Equals(department) == true).ToList();
            }
        }

        public static List<Employee> GetEmployeesByCountry(Country country)
        {
            using (var dbContext = new AppDBContext())
            {
                return GetAllEmployees().Where(employee => employee.Department?.Country.Equals(country) == true).ToList();
            }
        }

        public static bool CheckIfEmployeeAlreadyExistsByLogin(string login)
        {
            return GetEmployeeByLogin(login) != null;
        }

        public static void AddNewEmployee(Employee employee)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateEmployee(Employee employee)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Employees.Update(employee);
                dbContext.SaveChanges();
            }
        }

        public static void DeleteEmployee(Employee employee)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
            }
        }

        public static bool CheckIfLoginIsUnique(Employee employee, string login)
        {
            using (var dbContext = new AppDBContext())
            {
                return !dbContext.Employees.Any(e => e.Login == login && !e.Equals(employee));
            }
        }

        public static bool CheckIfEmailIsUnique(Employee employee, string email)
        {
            using (var dbContext = new AppDBContext())
            {
                return !dbContext.Employees.Any(e => e.Email == email && !e.Equals(employee));
            }
        }
    }
}
