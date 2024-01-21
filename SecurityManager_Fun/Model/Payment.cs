using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Model
{
    public class Payment
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public StatusType Status { get; set; }
        public int EmployeeID { get; set; }

        public enum StatusType
        {
            Created = 0, Commited = 1, Done = 2
        }

        // Navigation property
        public Employee Employee { get; set; }
        public ICollection<PaymentDeduction> PaymentDeductions { get; set; }
    }
}
