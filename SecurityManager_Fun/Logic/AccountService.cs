using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System.Security.Cryptography;
using System.Text;

namespace SecurityManager_Fun.Logic
{
    public class AccountService
    {
        private static int ITERATIONS = 350000;
        private static int KEY_SIZE = 64;
        private static HashAlgorithmName HASH_ALGORITHM = HashAlgorithmName.SHA512;

        public static Employee LogIn(string login, string password)
        {
            Employee employee;

            employee = EmployeeRepository.GetEmployeeByLogin(login);
            if (employee == null) return null;

            string[] passwordParts = employee.Password.Split(":");

            return (VerifyPassword(password, passwordParts[0], Convert.FromHexString(passwordParts[1])) ? employee : null);
        }

        public static void RegisterNewEmployee(string firstName, string lastName, string phoneNumber, int RoleID) 
        {
            byte[] salt;
            string defaultLogin = DefaultValuesGenerator.GenerateDefaultEmployeeLogin(firstName, lastName);
            string defaultEmail = DefaultValuesGenerator.GenerateDefaultEmployeeEmail(defaultLogin);
            string defaultPassword = DefaultValuesGenerator.GenerateDefaultEmployeePassword(HashPassword(defaultLogin + "_SM", out salt), salt);
            decimal defaultGrossRate = DefaultValuesGenerator.GenerateDefaultEmployeeGrossRate();

            //TODO: Add verification of provided data in this class

            if (EmployeeRepository.CheckIfEmployeeAlreadyExistsByLogin(defaultLogin)) return;

            EmployeeRepository.AddNewEmployee(
                new Employee
                {
                    Name = firstName,
                    Surname = lastName,
                    Phone = phoneNumber,
                    RoleID = RoleID,
                    DepartmentID = null,
                    Login = defaultLogin,
                    Email = defaultEmail,
                    Password = defaultPassword,
                    GrossRate = defaultGrossRate
                });

        }

        //salt with out here is used to generate salt with hash and then write the whole password(with pattern: hash:salt) to the DB
        public static string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(KEY_SIZE);

            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, ITERATIONS, HASH_ALGORITHM, KEY_SIZE);
            return Convert.ToHexString(hash);
        }


        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, HASH_ALGORITHM, KEY_SIZE);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
