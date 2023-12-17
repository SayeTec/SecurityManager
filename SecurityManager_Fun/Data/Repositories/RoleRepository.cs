using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data.Repositories
{
    public class RoleRepository
    {
        private readonly AppDBContext dbContext;

        public RoleRepository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
