using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data.Repositories
{
    public class PaymentDeductionRepository
    {
        public static List<PaymentDeduction> GetPaymentDeductionsByPayment(Payment payment)
        {
            using (var dbContext = new AppDBContext())
            {
                List<PaymentDeduction> paymentDeductions = dbContext.PaymentDeductions.Where(pd => pd.PaymentID == payment.ID).ToList();

                return paymentDeductions;
            }
        }

        public static void RemovePaymentDeductionsByPayment(Payment payment)
        {
            using (var dbContext = new AppDBContext())
            {
                
                dbContext.SaveChanges();
            }
        }
    }
}
