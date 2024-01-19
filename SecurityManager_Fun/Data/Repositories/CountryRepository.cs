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
    }
}
