using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic.Filters
{
    public class EmployeeFilter
    {
        public static List<Employee> FilterEmployees(string name, string surname, Role role, Department department)
        {
            return EmployeeRepository.GetAllEmployees()
                .Where(emp => 
                (string.IsNullOrEmpty(name) || emp.Name.Contains(name)) &&
                (string.IsNullOrEmpty(surname) || emp.Surname.Contains(surname)) &&
                (department == null || emp.Department.Equals(department)) &&
                (role == null || emp.Role.Equals(role)))
                .ToList();
        }
    }
}
