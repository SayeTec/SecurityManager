using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data.Repositories
{
    public class EmployeeRepository
    {
        public static Employee GetEmployeeByLogin(string login)
        {
            using(var dbContext = new AppDBContext())
            {
                return dbContext.Employees.SingleOrDefault(employee => employee.Login == login);
            }
        }

        public static bool CheckIfEmployeeAlreadyExistsByLogin(string login)
        {
            return GetEmployeeByLogin(login) != null;
        }
    }
}
