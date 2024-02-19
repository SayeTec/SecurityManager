using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data
{
    public class DefaultValuesGenerator
    {
        public static string GenerateDefaultEmployeeLogin(string firstName, string lastName, Employee employee)
        {
            string generatedLogin = string.Format("{0}.{1}", firstName.ToLower(), lastName.ToLower());

            while(!ValuesValidation.ValidateLoginIsUnique(generatedLogin, employee))
            {
                generatedLogin = $"{Regex.Replace(generatedLogin, @"\d*$", "")}{GenerateNumericSuffix(generatedLogin)}";
            }

            return generatedLogin;
        }

        public static string GenerateDefaultEmployeePassword(string hash, byte[] salt)
        {
            return string.Format("{0}:{1}", hash, Convert.ToHexString(salt));
        }

        public static string GenerateDefaultEmployeeEmail(string login)
        {
            return string.Format("{0}@gmail.com", login);
        }

        public static decimal GenerateDefaultEmployeeGrossRate()
        {
            return ApplicationConstants.DEFAULT_GROSS_RATE;
        }

        static string GenerateNumericSuffix(string str)
        {
            string numericSuffix = Regex.Match(str, @"\d*$").Value;
            int newNumericSuffix = string.IsNullOrEmpty(numericSuffix) ? 1 : int.Parse(numericSuffix) + 1;
            return newNumericSuffix.ToString();
        }
    }
}
