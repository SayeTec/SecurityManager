using Microsoft.EntityFrameworkCore;
using SecurityManager_Fun.Model;
using System.Data;
using System.Diagnostics.Metrics;

namespace SecurityManager_Fun.Data.Repositories
{
    public class DeductionRepository
    {
        public static List<Deduction> GetAllDeductions()
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Deductions
                    .Include(d => d.Country)
                    .ToList();
            }
        }

        public static decimal GetDeductionAmount(Payment payment)
        {
            using (var dbContext = new AppDBContext())
            {
                return GetDeductionAmountByTypeForPayment(payment, Deduction.DeductionType.Bonus).Sum() -
                   GetDeductionAmountByTypeForPayment(payment, Deduction.DeductionType.Tax).Sum();
            }
        }

        public static List<decimal> GetDeductionAmountByTypeForPayment(Payment payment, Deduction.DeductionType type)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.PaymentDeductions
                    .Where(pd => pd.PaymentID == payment.ID)
                    .Select(pd => dbContext.Deductions.Where(d => d.ID == pd.ID)
                    .Where(d => d.Type == type)
                    .Sum(d => d.IsPercentage ? d.Value * payment.Employee.GrossRate : d.Value))
                    .ToList();
            }
        }

        public static List<Deduction> GetDeductionsByTypeForPayment(Payment payment, Deduction.DeductionType type)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.PaymentDeductions
                    .Where(pd => pd.PaymentID == payment.ID)
                    .Select(pd => dbContext.Deductions
                    .Where(d => d.ID == pd.ID)
                    .Where(d => d.Type == type))
                    .SelectMany(pd => pd)
                    .ToList();
            }
        }

        public static List<Deduction> GetDeductionsForPayment(Payment payment)
        {
            using (var dbContext = new AppDBContext())
            {
                List<Deduction> deductions = dbContext.PaymentDeductions
                    .Where(pd => pd.PaymentID == payment.ID)
                    .Select(pd => dbContext.Deductions
                    .Where(d => d.ID == pd.DeductionID)
                    .ToList())
                    .SelectMany(pd => pd)
                    .ToList();

                return deductions;
            }
        }

        public static List<Deduction> GetDeductionsByType(Deduction.DeductionType deductionType)
        {
            return GetAllDeductions().Where(d => d.Type == deductionType).ToList();
        }

        public static List<Deduction> GetDeductionsByCountry(Country country)
        {
            return GetAllDeductions().Where(d => country == null ? d.Country == null : d.Country?.Equals(country) == true).ToList();
        }

        public static List<Deduction> GetDeductionsExceptForList(List<Deduction> deductions)
        {
            return GetAllDeductions().Where(d => !deductions.Contains(d)).ToList();
        }

        public static List<Deduction> GetDeductionsForSelection(Country country, Deduction.DeductionType deductionType, List<Deduction> deductionsToExclude)
        {
            return GetAllDeductions()
                .Where(d => d.Country == null || d.Country?.Equals(country) == true)
                .Where(d => d.Type == deductionType)
                .Where(d => !deductionsToExclude.Contains(d))
                .ToList();
        }

        public static void AddNewDeduction(Deduction deduction)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Deductions.Add(deduction);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateDeduction(Deduction deduction)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Deductions.Update(deduction);
                dbContext.SaveChanges();
            }
        }

        public static void DeleteDeduction(Deduction deduction)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Deductions.Remove(deduction);
                dbContext.SaveChanges();
            }
        }

        public static bool CheckIfDeductionHasAttachedToPayments(Deduction deduction)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.PaymentDeductions.Any(pd => pd.DeductionID == deduction.ID);
            }
        }

        public static bool CheckIfDeductionIsUnique(Deduction deduction)
        {
            using (var dbContext = new AppDBContext())
            {
                return !dbContext.Deductions.Any(d => d.Equals(deduction));
            }
        }
    }
}
