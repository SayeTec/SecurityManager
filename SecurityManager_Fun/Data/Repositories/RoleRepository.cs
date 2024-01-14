using SecurityManager_Fun.Model;
using static SecurityManager_Fun.Model.Role;

namespace SecurityManager_Fun.Data.Repositories
{
    public class RoleRepository
    {
        public static List<Role> GetRolesUnderEmployeePriority(Employee employee)
        {
            using (var context = new AppDBContext())
            {
                return GetRolesUnderPriority(context.Roles.Find(employee.RoleID).Priority);
            }
        }

        private static List<Role> GetRolesUnderPriority(PriorityType priority)
        {
            using (var context = new AppDBContext())
            {
                return context.Roles.Where(role => (int)role.Priority > (int)priority).ToList();
            }
        }

        public static List<Role> GetRolesUnderOrEqualsEmployeePriority(Employee employee)
        {
            using (var context = new AppDBContext())
            {
                return GetRolesUnderOrEqualsPriority(context.Roles.Find(employee.RoleID).Priority);
            }
        }

        private static List<Role> GetRolesUnderOrEqualsPriority(PriorityType priority)
        {
            using (var context = new AppDBContext())
            {
                return context.Roles.Where(role => (int)role.Priority >= (int)priority).ToList();
            }
        }
    }
}
