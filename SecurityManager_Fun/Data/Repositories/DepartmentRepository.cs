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
    }
}
