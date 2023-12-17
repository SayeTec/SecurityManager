using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data.Repositories
{
    public class SettlementRepository
    {
        private readonly AppDBContext dbContext;

        public SettlementRepository(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
