using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic.Filters
{
    public class PaymentFilter
    {
        public static List<Payment> FilterPayments(DateTime? startDate, DateTime? endDate, Country country, Department department, Payment.StatusType? status, Employee employee)
        {
            return PaymentRepository.GetActivePayments()
                .Where(pay =>
                (department == null || pay.Employee.Department?.Equals(department) == true) &&
                (country == null || pay.Employee.Department?.Country.Equals(country) == true) &&
                (employee == null || pay.Employee.Equals(employee) == true) &&
                (status == null || pay.Status == status) &&
                (startDate == null || pay.DateOfCreation.Date.CompareTo(((DateTime)startDate).Date) >= 0) &&
                (endDate == null || pay.DateOfCreation.Date.CompareTo(((DateTime)endDate).Date) <= 0))
                .ToList();
        }

        public static List<Payment> FilterArchivisedPayments(DateTime? startDate, DateTime? endDate, Country country, Department department, Payment.StatusType? status, Employee employee)
        {
            return PaymentRepository.GetInactivePayments()
                .Where(pay =>
                (department == null || pay.Employee.Department?.Equals(department) == true) &&
                (country == null || pay.Employee.Department?.Country.Equals(country) == true) &&
                (employee == null || pay.Employee.Equals(employee) == true) &&
                (status == null || pay.Status == status) &&
                (startDate == null || ((DateTime)pay.DateOfEnd).Date.CompareTo(((DateTime)startDate).Date) >= 0) &&
                (endDate == null || ((DateTime)pay.DateOfEnd).Date.CompareTo(((DateTime)endDate).Date) <= 0))
                .ToList();
        }
    }
}
