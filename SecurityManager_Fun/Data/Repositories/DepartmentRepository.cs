using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data.Repositories
{
    public class DepartmentRepository
    {
        public static List<Department> GetAllDepartments()
        {
            using(var dbContext = new AppDBContext())
            {
                return dbContext.Departments.Include(d => d.Country).ToList();
            }
        } 

        public static List<Department> GetDepartmentsByCountry(Country country)
        {
            return GetAllDepartments().Where(d => d.Country.Equals(country)).ToList();
        }

        public static void AddDepartment(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Departments.Add(department);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateDepartment(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Departments.Update(department);
                dbContext.SaveChanges();
            }
        }

        public static void DeleteDepartment(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Departments.Remove(department);
                dbContext.SaveChanges();
            }
        }

        public static bool CheckIfDepartmentHasEmployees(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Employees.Where(employee => employee.DepartmentID == department.ID).Count() > 0;
            }
        }

        public static bool CheckIfDepartmentIsUnique(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                return !dbContext.Departments.Any(d => d.Equals(department));
            }
        }

        public static bool CheckIfDepartmentHavePlaceForEmployee(Department department)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Departments.Find(department.ID).Capacity > dbContext.Employees.Where(employee => employee.DepartmentID != null && employee.DepartmentID == department.ID).Count();
            }
        }
    }
}
