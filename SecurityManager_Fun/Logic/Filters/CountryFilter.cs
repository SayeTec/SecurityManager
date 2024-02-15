using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;

namespace SecurityManager_Fun.Logic.Filters
{
    public class CountryFilter
    {
        public static List<Country> FilterCountries(string symbol, string name) 
        {
            return CountryRepository.GetAllCountries()
                .Where(country =>
                (string.IsNullOrEmpty(symbol) || country.Symbol.ToLower().Contains(symbol.ToLower())) &&
                (string.IsNullOrEmpty(name) || country.Name.ToLower().Contains(name.ToLower())))
                .ToList();
        }
    }
}
