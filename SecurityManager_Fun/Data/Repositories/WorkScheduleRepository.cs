using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data.Repositories
{
    public class WorkScheduleRepository
    {
        private readonly AppDBContext dbContext;

        public WorkScheduleRepository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
