using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data.Repositories
{
    internal class DepartmentRepository
    {
        private readonly AppDBContext dbContext;

        public DepartmentRepository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
