using SecurityManager_Fun.Data.Repositories;

namespace SecurityManager_Fun.Model
{
    public class Payment
    {
        public int ID { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfLatestModification { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public decimal DefaultValue { get; set; }
        public decimal DeductionsValue { get; set; }
        public decimal FinalValue { get; set; }
        public StatusType Status { get; set; }
        public int EmployeeID { get; set; }

        public enum StatusType
        {
            Created = 0, Commited = 1, Done = 2, Canceled = 3
        }

        // Navigation property
        public Employee Employee { get; set; }
        public ICollection<PaymentDeduction> PaymentDeductions { get; set; }

        //public decimal DeductionsValue { get { return DeductionRepository.GetDeductionAmount(this); } }

        public StatusType IncreaseStatus()
        {
            if (Status == StatusType.Canceled) return Status;
            return Status++;
        }

        public StatusType DecreaseStatus()
        {
            if (Status == StatusType.Created) return Status;
            return Status--;
        }

        public void Cancel()
        {
            Status = StatusType.Canceled;
        }
    }
}
