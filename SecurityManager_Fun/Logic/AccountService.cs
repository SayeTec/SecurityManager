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

            return (VerifyPassword(password, employee.Password) ? employee : null);
        }

        public static void RegisterNewEmployee(string firstName, string lastName, string phoneNumber, int roleID) 
        {
            byte[] salt;
            string defaultLogin = DefaultValuesGenerator.GenerateDefaultEmployeeLogin(firstName, lastName);
            string defaultEmail = DefaultValuesGenerator.GenerateDefaultEmployeeEmail(defaultLogin);
            string defaultPassword = DefaultValuesGenerator.GenerateDefaultEmployeePassword(HashPassword(GenerateRandomPassword(defaultLogin), out salt), salt);
            decimal defaultGrossRate = DefaultValuesGenerator.GenerateDefaultEmployeeGrossRate();

            //TODO: Add verification of provided data in this class

            if (EmployeeRepository.CheckIfEmployeeAlreadyExistsByLogin(defaultLogin)) return;

            EmployeeRepository.AddNewEmployee(
                new Employee
                {
                    Name = firstName,
                    Surname = lastName,
                    Phone = phoneNumber,
                    RoleID = roleID,
                    DepartmentID = null,
                    Login = defaultLogin,
                    Email = defaultEmail,
                    Password = defaultPassword,
                    GrossRate = defaultGrossRate
                });

        }

        public static void RegisterNewEmployee(Employee newEmployee)
        {
            byte[] salt;
            newEmployee.Login = DefaultValuesGenerator.GenerateDefaultEmployeeLogin(newEmployee.Name, newEmployee.Surname);
            newEmployee.Email = string.IsNullOrEmpty(newEmployee.Email) ? DefaultValuesGenerator.GenerateDefaultEmployeeEmail(newEmployee.Login) : newEmployee.Email;
            newEmployee.Password = DefaultValuesGenerator.GenerateDefaultEmployeePassword(HashPassword(GenerateRandomPassword(newEmployee.Login), out salt), salt);
            newEmployee.GrossRate = DefaultValuesGenerator.GenerateDefaultEmployeeGrossRate();

            //TODO: Add verification of provided data in this class

            if (EmployeeRepository.CheckIfEmployeeAlreadyExistsByLogin(newEmployee.Login)) return;

            EmployeeRepository.AddNewEmployee(newEmployee);
        }

        public static string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(KEY_SIZE);

            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, ITERATIONS, HASH_ALGORITHM, KEY_SIZE);
            return Convert.ToHexString(hash);
        }

        public static bool VerifyPasswordWithHash(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, HASH_ALGORITHM, KEY_SIZE);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        public static bool VerifyPassword(string passwordToVerify, string hashedPassword)
        {
            string[] passwordParts = hashedPassword.Split(":");

            return VerifyPasswordWithHash(passwordToVerify, passwordParts[0], Convert.FromHexString(passwordParts[1]));
        }

        private static string GenerateRandomPassword(string login)
        {
            int min = 0000;
            int max = 9999;

            Random random = new Random();
            string password = "u" + login + random.Next(min, max).ToString();
            return password;
        }
    }
}
