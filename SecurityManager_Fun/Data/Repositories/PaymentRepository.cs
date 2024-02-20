using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data.Repositories
{
    public class PaymentRepository
    {
        public static List<Payment> GetAllPayments()
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Payments
                    .Include(p => p.Employee)
                        .ThenInclude(e => e.Department)
                            .ThenInclude(d => d.Country)
                    .Include(p => p.PaymentDeductions)
                    .ToList();
            }
        }

        public static List<Payment> GetPaymentThisMonthByEmployee(Employee employee)
        {
            return GetAllPayments()
                .Where(payment => payment.EmployeeID == employee.ID &&
                                payment.DateOfCreation.Month == DateTime.Now.Month &&
                                payment.DateOfCreation.Year == DateTime.Now.Year)
                .ToList();
        }

        public static List<Payment> GetActivePayments()
        {
            return GetAllPayments().Where(payment => (int)payment.Status <= 1).ToList();
        }

        public static List<Payment> GetInactivePayments()
        {
            return GetAllPayments().Where(payment => (int)payment.Status >= 2).ToList();
        }

        public static List<Payment> GetInactivePaymentsForEmployee(Employee employee)
        {
            return GetInactivePayments().Where(payment => payment.EmployeeID == employee.ID).ToList();
        }

        public static void AddNewPayment(Payment payment, List<Deduction> associatedDeductions)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Payments.Add(payment);

                List<PaymentDeduction> paymentDeductions = associatedDeductions.Select(deduction => new PaymentDeduction
                {
                    Payment = payment,
                    DeductionID = deduction.ID
                })
                .ToList();

                dbContext.PaymentDeductions.AddRange(paymentDeductions);

                dbContext.SaveChanges();
            }
        }

        public static void UpdatePayment(Payment payment)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Payments.Update(payment);
                dbContext.SaveChanges();
            }
        }

        public static void UpdatePaymentWithDeductions(Payment payment, List<Deduction> associatedDeductions)
        {
            using (var dbContext = new AppDBContext())
            {
                List<PaymentDeduction> paymentDeductionsToRemove = PaymentDeductionRepository.GetPaymentDeductionsByPayment(payment);

                dbContext.PaymentDeductions.RemoveRange(paymentDeductionsToRemove);

                List<PaymentDeduction> paymentDeductions = associatedDeductions.Select(deduction => new PaymentDeduction
                {
                    PaymentID = payment.ID,
                    DeductionID = deduction.ID
                })
                .ToList();

                dbContext.PaymentDeductions.AddRange(paymentDeductions);
                payment.PaymentDeductions = paymentDeductions;

                dbContext.Payments.Update(payment);
                dbContext.SaveChanges();
            }
        }
    }
}
