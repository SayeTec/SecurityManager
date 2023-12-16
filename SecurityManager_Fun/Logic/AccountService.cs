using SecurityManager_Fun.Data;
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

            using (var context = new AppDBContex())
            {
                employee = context.Employees.FirstOrDefault(e => e.Login == login);
                if (employee == null) return null;
            }

            string[] passwordParts = employee.Password.Split(":");

            return (VerifyPassword(password, passwordParts[0], Encoding.UTF8.GetBytes(passwordParts[1])) ? employee : null);
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
