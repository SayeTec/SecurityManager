using SecurityManager_Fun.Model;
using static SecurityManager_Fun.Model.Role;

namespace SecurityManager_Fun.Data.Repositories
{
    public class RoleRepository
    {
        public static List<Role> GetAllRoles()
        {
            using(var dbContext = new AppDBContext())
            {
                return dbContext.Roles.ToList();
            }
        }

        public static List<Role> GetRolesUnderEmployeePriority(Employee employee)
        {
            using (var context = new AppDBContext())
            {
                return GetRolesUnderPriority(context.Roles.Find(employee.RoleID).Priority);
            }
        }

        private static List<Role> GetRolesUnderPriority(int priority)
        {
            using (var context = new AppDBContext())
            {
                return context.Roles.Where(role => role.Priority > priority).ToList();
            }
        }

        public static List<Role> GetRolesUnderOrEqualsEmployeePriority(Employee employee)
        {
            using (var context = new AppDBContext())
            {
                return GetRolesUnderOrEqualsPriority(context.Roles.Find(employee.RoleID).Priority);
            }
        }

        private static List<Role> GetRolesUnderOrEqualsPriority(int priority)
        {
            using (var context = new AppDBContext())
            {
                return context.Roles.Where(role => role.Priority >= priority).ToList();
            }
        }

        public static void AddNewRole(Role role)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateRole(Role role)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Roles.Update(role);
                dbContext.SaveChanges();
            }
        }

        public static void DeleteRole(Role role)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Roles.Remove(role);
                dbContext.SaveChanges();
            }
        }

        public static bool CheckIfRoleHasEmployees(Role role)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Employees.Any(emp => emp.RoleID == role.ID);
            }
        }

        public static bool CheckIfRoleWithPriorityExistsInDB(int priority)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Roles.Any(role => role.Priority == priority);
            }
        }

        public static bool CheckIfRoleIsUnique(Role role)
        {
            using (var dbContext = new AppDBContext())
            {
                return !dbContext.Roles.Any(r => r.Code == role.Code && r.Name == role.Name);
            }
        }
    }
}
