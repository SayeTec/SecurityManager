using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Data.Repositories
{
    public class CountryRepository
    {
        public static List<Country> GetAllCountries()
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Countries.ToList();
            }
        }

        public static void AddCountry(Country country)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Countries.Add(country);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateCountry(Country country) 
        { 
            using (var dbContext = new AppDBContext())
            {
                dbContext.Countries.Update(country);
                dbContext.SaveChanges();
            }
        }

        public static void DeleteCountry(Country country)
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Countries.Remove(country);
                dbContext.SaveChanges();
            }
        }

        public static bool CheckIfCountryHasDeductions(Country country)
        {
            using (var dbContext = new AppDBContext()) 
            { 
                return dbContext.Deductions.Where(deduction => deduction.CountryID == country.ID).Count() > 0;
            }
        }

        public static bool CheckIfCountryHasDepartments(Country country)
        {
            using (var dbContext = new AppDBContext())
            {
                return dbContext.Departments.Where(department => department.CountryID == country.ID).Count() > 0;
            }
        }

        public static bool CheckIfCountryIsUnique(Country country)
        {
            using (var dbContext = new AppDBContext()) 
            {
                return !dbContext.Countries.Any(c => c.Equals(country)); 
            }
        }
    }
}
