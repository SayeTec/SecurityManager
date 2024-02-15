using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SecurityManager_Fun.Model.Deduction;

namespace SecurityManager_Fun.Logic.Filters
{
    public class DeductionFilter
    {
        public static List<Deduction> FilterDeductions(string name, decimal value, DeductionType? type, Country country)
        {
            return DeductionRepository.GetAllDeductions()
                .Where(deduction =>
                (string.IsNullOrEmpty(name) || deduction.Name.ToLower().Contains(name.ToLower())) &&
                (value == 0m || deduction.Value == value) &&
                (country == null || deduction.Country?.Equals(country) == true) &&
                (type == null || deduction.Type == type))
                .ToList();
        }
    }
}
