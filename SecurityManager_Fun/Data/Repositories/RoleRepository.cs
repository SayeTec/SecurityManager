using SecurityManager_Fun.Model;
using static SecurityManager_Fun.Model.Role;

namespace SecurityManager_Fun.Data.Repositories
{
    public class RoleRepository
    {
        public static List<Role> GetRolesUnderPriority(PriorityType priority)
        {
            using (var context = new AppDBContext())
            {
                return context.Roles.Where(role => (int)role.Priority > (int)priority + 1).ToList();
            }
        }
    }
}
