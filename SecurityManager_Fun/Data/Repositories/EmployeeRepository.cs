using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;
using System.Net.NetworkInformation;

namespace SecurityManager_Fun.Data.Repositories
{
    public class EmployeeRepository
    {
        public static List<Employee> GetAllEmployees()
        {
            using(var dbContext = new AppDBContext())
            {
                return dbContext.Employees
                    .Include(e => e.Role)
                    .Include(e => e.Department)
                        .ThenInclude(d => d.Country)
                    .ToList();
            }
        }

        public static Employee GetEmployeeByLogin(string login)
        {
            using (var dbContext = new AppDBContext())
            {
                return GetAllEmployees().SingleOrDefault(employee => employee.Login == login);
            }
        }

        public static bool CheckIfEmployeeAlreadyExistsByLogin(string login)
        {
            return GetEmployeeByLogin(login) != null;
        }

        public static void AddNewEmployee(Employee employee)
        {
            using (var context = new AppDBContext())
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }
    }
}
