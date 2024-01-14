using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data
{
    //TODO: Talk about default values for new Employee
    public class DefaultValuesGenerator
    {
        public static string GenerateDefaultEmployeeLogin(string firstName, string lastName)
        {
            return string.Format("{0}.{1}", firstName.ToLower(), lastName.ToLower());
        }

        public static string GenerateDefaultEmployeePassword(string hash, byte[] salt)
        {
            return string.Format("{0}:{1}", hash, Convert.ToHexString(salt));
        }

        public static string GenerateDefaultEmployeeEmail(string login)
        {
            return string.Format("{0}@gmail.com", login); //TODO: talk about email domain
        }

        public static decimal GenerateDefaultEmployeeGrossRate()
        {
            return ApplicationConstants.DEFAULT_GROSS_RATE;
        }
    }
}
