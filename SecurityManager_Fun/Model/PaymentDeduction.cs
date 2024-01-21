using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Model
{
    public class PaymentDeduction
    {
        public int ID { get; set; }
        public int PaymentID { get; set; }
        public int DeductionID { get; set; }

        // Navigation properties
        public Payment Payment { get; set; }
        public Deduction Deduction { get; set; }
    }
}
