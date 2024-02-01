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
            //Application
            public const string LOGGED_USER_PASSWORD_CONFIRMATION_ERROR = "Proszę podaj swoje poprawne hasło!";
            public const string PASSWORD_REPEAT_CONFIRMATION_ERROR = "Proszę wprowadić i potem powtórzyć nowe hasło!";

            //Password
            public const string PASSWORD_NOT_HAS_UPPERCASE = "Hasło powinno zawierać co najmniej jedną wielką literę.";
            public const string PASSWORD_NOT_HAS_LOWERCASE = "Hasło powinno zawierać co najmniej jedną małą literę.";
            public const string PASSWORD_NOT_HAS_DIGIT = "Hasło powinno zawierać co najmniej jedną cyfrę.";
            public const string PASSWORD_NOT_HAS_SPECIAL_CHAR = "Hasło powinno zawierać co najmniej jeden znak specjalny.";
            public static string PASSWORD_NOT_HAS_MINIMUM_LENGTH = $"Hasło powinno mieć co najmniej {ApplicationConstants.MINIMUM_PASSWORD_LENGTH} znaków.";

            //Employee
            public const string EMPLOYEE_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem operacji trzeba najpierw wybrać pracownika z listy.";
            public const string ATTEMPT_TO_UPDATE_EMPLOYEE_WITH_LOWER_ROLE = "Brak dostępu! Nie ma możliwości zmienić rolę pracownika.";
            public const string ATTEMPT_TO_UPDATE_EMPLOYEE_PASSWORD_WITH_LOWER_ROLE = "Brak dostępu! Nie ma możliwości zmienić hasła pracownika z wyższą rolą.";
            public const string REQUIRED_DATA_NOT_PROVIDED = "Dla dodania nowego pracownika musisz wypęłnić wszystkie wymagane pola: Imię, Nazwisko, Numer Telefonu oraz Rolę (Email jest opcjonalny i przy braku danych zostanie wygenerowany automatycznie).";

            public static string LOGIN_NOT_MATCH_PATTERN = "Podany login {0} musi być zgodny ze wzorem <imię>.<nazwisko> (wszystko małymi literami oraz).";
            public const string LOGIN_NOT_UNIQUE = "Podany login już istnieje!";

            public const string PHONE_NUMBER_NOT_VALID = "Podany numer telefonu jest niepoprawny! Dopuszczalne są tylko znaki: +, liczby i spacje.";

            public const string EMAIL_NOT_VALID = "Podany adres email jest niepoprawny! Musi być zgodny ze wzorem (example@gmail.com).";
            public const string EMAIL_NOT_UNIQUE_IN_DB = "Podany adres email już istnieje! Proszę podać inny adres mail.";

            public const string NAME_NOT_VALID = "Podane imię jest niepoprawne! Musi zawierać tylko litery.";

            public const string SURNAME_NOT_VALID = "Podane nazwisko jest niepoprawne! Może zawierać tylko litery, spacje oraz -.";


            //Payment
            public const string PAYMENT_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem tej operacji trzeba najpierw wybrać płatność z listy.";
            public static string PAYMENT_HAS_INAPPROPRIATE_STATUS = "Płatność posiada nieodpowiedni dla tej operacji status! Potrzebny status: {0}";

            public static string PAYMENT_AMOUNT_IS_OUT_OF_RANGE = $"Wartość płatności przekracza ustalony limit w {ApplicationConstants.RATE_VALUE_DIFFERENCE * 100}% odchylenia od podstawowej pensii!";

            //Deduction
            public const string DEDUCTION_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem tej operacji trzeba najpierw wybrać odliczenie z listy.";
        }

        public static class Confirmation
        {
            public const string EXIT_WITHOUT_SAVE_CONFIRAMTION = "Twoje zmiane nie zostaną zapisane automatycznie, czy napewno chcesz wyjść?";
            public const string EMPLOYEE_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć pracownika: {0}?";
            public static string EMPLOYEE_IS_PAID_IN_THIS_MONTH = "Podane pracownik {0} już posiada aktywne lub rozliczone płatności. Czy napewno chcesz kontynuować?";
        }
    }
}