using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Logic.Filters
{
    public class RoleFilter
    {
        public static List<Role> FilterRoles(string code, string name)
        {
            return RoleRepository.GetAllRoles()
                .Where(role =>
                (string.IsNullOrEmpty(code) || role.Code.ToLower().Contains(code.ToLower())) &&
                (string.IsNullOrEmpty(name) || role.Name.ToLower().Contains(name.ToLower())))
                .ToList();
        }
    }
}
