using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager_Fun.Data
{
    public static class DisplayMessages
    {
        public static class Error
        {
            public const string PASSWORD_NOT_HAS_UPPERCASE = "Hasło powinno zawierać co najmniej jedną wielką literę.";
            public const string PASSWORD_NOT_HAS_LOWERCASE = "Hasło powinno zawierać co najmniej jedną małą literę.";
            public const string PASSWORD_NOT_HAS_DIGIT = "Hasło powinno zawierać co najmniej jedną cyfrę.";
            public const string PASSWORD_NOT_HAS_SPECIAL_CHAR = "Hasło powinno zawierać co najmniej jeden znak specjalny.";
            public static string PASSWORD_NOT_HAS_MINIMUM_LENGTH = $"Hasło powinno mieć co najmniej {ApplicationConstants.MINIMUM_PASSWORD_LENGTH} znaków.";
            
            public const string EMPLOYEE_FROM_LIST_MUST_BE_SELECTED = "Przed edytowaniem pracownika trzeba go najpierw wybrać z listy.";
            public const string ATTEMPT_TO_UPDATE_EMPLOYEE_WITH_LOWER_ROLE = "Brak dostępu! Nie ma możliwości zmienić rolę pracownika.";
            public const string ATTEMPT_TO_UPDATE_EMPLOYEE_PASSWORD_WITH_LOWER_ROLE = "Brak dostępu! Nie ma możliwości zmienić hasła pracownika z wyższą rolą.";
            public const string REQUIRED_DATA_NOT_PROVIDED = "Dla dodania nowego pracownika musisz wypęłnić wszystkie wymagane pola: Imię, Nazwisko, Numer Telefonu oraz Rolę (Email jest opcjonalny i przy braku danych zostanie wygenerowany automatycznie).";

            public static string LOGIN_NOT_MATCH_PATTERN = "Podany login {0} musi być zgodny ze wzorem <imię>.<nazwisko> (wszystko małymi literami oraz).";
            public static string LOGIN_NOT_UNIQUE = "Podany login już istnieje!";

            public static string PHONE_NUMBER_NOT_VALID = "Podany numer telefonu jest niepoprawny! Dopuszczalne są tylko znaki: +, liczby i spacje.";

            public static string EMAIL_NOT_VALID = "Podany adres email jest niepoprawny! Musi być zgodny ze wzorem (example@gmail.com).";
        } 

        public static class Confirmation
        {
            public const string EXIT_WITHOUT_SAVE_CONFIRAMTION = "Twoje zmiane nie zostaną zapisane automatycznie, czy napewno chcesz wyjść?";
            public const string EMPLOYEE_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć pracownika: {0}?";
        }
    }
}
