using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System.Text.RegularExpressions;

namespace SecurityManager_Fun.Logic
{
    public class ValuesValidation
    {
        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            
            return ValidatePasswordHasAtLeastOneUppercase(password) &&
                ValidatePasswordHasAtLeastOneLowercase(password) &&
                ValidatePasswordHasAtLeastOneDigit(password) &&
                ValidatePasswordHasAtLeastOneSpecialChar(password) &&
                ValidatePasswordMeetsMinimumLength(password);
        }

        public static bool ValidatePasswordHasAtLeastOneUppercase(string password)
        {
            Regex uppercaseRegex = new Regex(@"[A-Z]");
            return uppercaseRegex.IsMatch(password);
        }

        public static bool ValidatePasswordHasAtLeastOneLowercase(string password)
        {
            Regex lowercaseRegex = new Regex(@"[a-z]");
            return lowercaseRegex.IsMatch(password);
        }

        public static bool ValidatePasswordHasAtLeastOneDigit(string password)
        {
            Regex digitRegex = new Regex(@"[0-9]");
            return digitRegex.IsMatch(password);
        }

        public static bool ValidatePasswordHasAtLeastOneSpecialChar(string password)
        {
            Regex specialCharRegex = new Regex(@"[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]");
            return specialCharRegex.IsMatch(password);
        }

        public static bool ValidatePasswordMeetsMinimumLength(string password)
        {
            const int MinimumLength = 8;
            return password.Length >= MinimumLength;
        }

        public static bool ValidateLogin(string login, Employee employee)
        {
            if (string.IsNullOrEmpty(login) || employee == null) return false;

            return ValidateLoginMatchesPattern(login, employee)
                && ValidateLoginIsUnique(login, employee);
        }

        public static bool ValidateLoginMatchesPattern(string login, Employee employee)
        {
            string possibleLogin = $"{employee.Name.ToLower()}.{employee.Surname.ToLower()}";
            Regex loginRegex = new Regex(@"^" + possibleLogin + @"([a-z]*\d*)$");
            return loginRegex.IsMatch(login);
        }

        public static bool ValidateLoginIsUnique(string login, Employee employee)
        {
            return EmployeeRepository.CheckIfLoginIsUnique(employee, login);
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            Regex phoneRegex = new Regex(@"^(\+\d)?[\d\s]*$");
            return phoneRegex.IsMatch(phoneNumber);
        }

        public static bool ValidateEmail(string email)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }

        public static bool ValidateEmailIsUnique(string login, Employee employee)
        {
            return EmployeeRepository.CheckIfEmailIsUnique(employee, login);

        }

        public static bool ValidateName(string name)
        {
            Regex nameRegex = new Regex(@"^[a-zA-Z]+$");
            return nameRegex.IsMatch(name);
        }

        public static bool ValidateSurname(string surname)
        {
            Regex surnameRegex = new Regex(@"^[a-zA-Z\s]+(-[a-zA-Z]+)?$");
            return surnameRegex.IsMatch(surname);

        }

        public static bool ValidateCountrySymbol(string symbol)
        {
            Regex symbolRegex = new Regex(@"^[A-Z]+$");
            return symbolRegex.IsMatch(symbol);
        }

        public static bool ValidateCountryName(string name)
        {
            Regex nameRegex = new Regex(@"^[A-Za-z]+$");
            return nameRegex.IsMatch(name);
        }

        public static bool ValidateDepartmentAddress(string address)
        {
            Regex addressRegex = new Regex(@"^[a-zA-Z0-9\s.,/-]*$");
            return addressRegex.IsMatch(address);
        }

        public static bool ValidateDepartmentCapacity(string capacity)
        {
            Regex capacityRegex = new Regex(@"^[0-9]+$");
            return capacityRegex.IsMatch(capacity);
        }

        public static bool ValidateRoleCode(string code)
        {
            Regex codeRegex = new Regex(@"^[A-Z]+$");
            return codeRegex.IsMatch(code);
        }

        public static bool ValidateRoleName(string name)
        {
            Regex nameRegex = new Regex(@"^[a-zA-Z\s]+(-[a-zA-Z]+)?$");
            return nameRegex.IsMatch(name);
        }

        public static bool ValidateRolePriority(string priority)
        {
            Regex codeRegex = new Regex(@"^[0-9]+$");
            return codeRegex.IsMatch(priority);
        }

        public static bool ValidateDeductionName(string name)
        {
            Regex nameRegex = new Regex(@"^[A-Za-z0-9\s\/\-\\]+$");
            return nameRegex.IsMatch(name);
        }

        public static bool ValidateDeductionValue(string value)
        {
            Regex valueRegex = new Regex(@"^\d+(\,\d{0,10})?$");
            return valueRegex.IsMatch(value);
        }

        public static bool ValidateGrossRateValue(string value)
        {
            Regex valueRegex = new Regex(@"^\d+(\,\d{0,10})?$");
            return valueRegex.IsMatch(value);
        }
    }
}
