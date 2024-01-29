using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic.Filters
{
    public class EmployeeFilter
    {
        public static List<Employee> FilterEmployees(string name, string surname, Role role, Country country, Department department)
        {
            return EmployeeRepository.GetAllEmployees()
                .Where(emp =>
                (string.IsNullOrEmpty(name) || emp.Name?.ToLower().Contains(name.ToLower()) == true) &&
                (string.IsNullOrEmpty(surname) || emp.Surname?.ToLower().Contains(surname.ToLower()) == true) &&
                (department == null || emp.Department?.Equals(department) == true) &&
                (country == null || emp.Department?.Country?.Equals(country) == true) &&
                (role == null || emp.Role?.Equals(role) == true))
                .ToList();
        }

        public static List<Employee> FilterEmployeesForPaymentCreation(string name, string surname, Role role, Country country, Department department)
        {
            return EmployeeRepository.GetUnpaidEmployees()
                .Where(emp =>
                (string.IsNullOrEmpty(name) || emp.Name?.ToLower().Contains(name.ToLower()) == true) &&
                (string.IsNullOrEmpty(surname) || emp.Surname?.ToLower().Contains(surname.ToLower()) == true) &&
                (department == null || emp.Department?.Equals(department) == true) &&
                (country == null || emp.Department?.Country?.Equals(country) == true) &&
                (role == null || emp.Role?.Equals(role) == true))
                .ToList();
        }

        public static List<Employee> FilterEmployeesUnderPriority(int priority, string name, string surname, Role role, Country country, Department department)
        {
            return EmployeeRepository.GetEmployeesUnderPriority(priority)
                .Where(emp =>
                (string.IsNullOrEmpty(name) || emp.Name?.ToLower().Contains(name.ToLower()) == true) &&
                (string.IsNullOrEmpty(surname) || emp.Surname?.ToLower().Contains(surname.ToLower()) == true) &&
                (department == null || emp.Department?.Equals(department) == true) &&
                (country == null || emp.Department?.Country?.Equals(country) == true) &&
                (role == null || emp.Role?.Equals(role) == true))
                .ToList();

        }
    }
}
