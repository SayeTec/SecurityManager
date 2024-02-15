using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic.Filters
{
    public class DepartmentFilter
    {
        public static List<Department> FilterDepartments(string address, int capacity, Country country)
        {
            return DepartmentRepository.GetAllDepartments()
                .Where(depart =>
                (string.IsNullOrEmpty(address) || depart.Address.ToLower().Contains(address.ToLower())) &&
                (capacity == 0 || depart.Capacity == capacity) &&
                (country == null || depart.Country.Equals(country)))
                .ToList();
        }

    }
}
