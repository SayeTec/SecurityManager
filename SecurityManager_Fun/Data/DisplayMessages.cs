namespace SecurityManager_Fun.Data
{
    public static class DisplayMessages
    {
        public static class Error
        {
            //Application
            public const string LOGGED_USER_PASSWORD_CONFIRMATION_ERROR = "Wpisz poprawne hasło!";
            public const string PASSWORD_REPEAT_CONFIRMATION_ERROR = "Wprowadź, a następnie powtórz nowe hasło!";
            public const string LOGGING_IN_ERROR = "Podano nieprawidłowy login lub hasło!";
            public const string OPERATION_CANNOT_BE_APPLIED = "Nie można wykonać operacji:";
            public const string GROSSRATE_VALUE_NOT_VALID = "Podana wartość obliczenia jest niepoprawna! Dopuszczalne są tylko liczby oraz ,.";
            public const string GROSSRATE_VALUE_DATA_NOT_PROVIDED = "Aby zmianić pensje pracownika trzeba podać nową wartość!";


            //Password
            public const string PASSWORD_NOT_HAS_UPPERCASE = "Hasło powinno zawierać co najmniej jedną wielką literę.";
            public const string PASSWORD_NOT_HAS_LOWERCASE = "Hasło powinno zawierać co najmniej jedną małą literę.";
            public const string PASSWORD_NOT_HAS_DIGIT = "Hasło powinno zawierać co najmniej jedną cyfrę.";
            public const string PASSWORD_NOT_HAS_SPECIAL_CHAR = "Hasło powinno zawierać co najmniej jeden znak specjalny.";
            public static string PASSWORD_NOT_HAS_MINIMUM_LENGTH = $"Hasło powinno liczyć co najmniej {ApplicationConstants.MINIMUM_PASSWORD_LENGTH} znaków.";

            //Employee
            public const string EMPLOYEE_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem operacji trzeba najpierw wybrać pracownika z listy.";
            public const string ATTEMPT_TO_UPDATE_EMPLOYEE_WITH_LOWER_ROLE = "Brak dostępu! Brak możliwości zmiany roli pracownika.";
            public const string ATTEMPT_TO_UPDATE_EMPLOYEE_PASSWORD_WITH_LOWER_ROLE = "Brak dostępu! Brak możliwości zmiany hasła pracownika z wyższą rolą.";
            public const string EMPLOYEE_REQUIRED_DATA_NOT_PROVIDED = "Aby dodać nowego pracownika musisz wypełnić wszystkie wymagane pola: Imię, Nazwisko, Numer Telefonu oraz Rolę (Email jest opcjonalny i przy braku danych zostanie wygenerowany automatycznie).";

            public static string LOGIN_NOT_MATCH_PATTERN = "Podany login {0} musi być zgodny ze wzorcem <imię>.<nazwisko> (wszystko małymi literami).";
            public const string LOGIN_NOT_UNIQUE = "Podany login już istnieje!";

            public const string PHONE_NUMBER_NOT_VALID = "Podany numer telefonu jest niepoprawny! Dopuszczalne są tylko znaki: +, liczby i spacje.";

            public const string EMAIL_NOT_VALID = "Podany adres email jest niepoprawny! Musi być zgodny ze wzorem (example@example.com).";
            public const string EMAIL_NOT_UNIQUE_IN_DB = "Podany adres email jest przypisany do innego użytkownika! Proszę podać inny adres email.";

            public const string NAME_NOT_VALID = "Podane imię jest niepoprawne! Moźe zawierać tylko litery.";

            public const string SURNAME_NOT_VALID = "Podane nazwisko jest niepoprawne! Może zawierać tylko litery, spacje oraz -.";

            //Payment
            public const string PAYMENT_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem tej operacji trzeba najpierw wybrać płatność z listy.";
            public static string PAYMENT_HAS_INAPPROPRIATE_STATUS = "Płatność posiada nieodpowiedni dla tej operacji status! Potrzebny status: {0}";

            public static string PAYMENT_AMOUNT_IS_OUT_OF_RANGE = $"Wartość płatności przekracza ustalony limit i nie może być ujemna!";

            //Country
            public const string COUNTRY_HAS_DEPARTMENTS = "Kraj ma przypisane biura!";
            public const string COUNTRY_HAS_DEDUCTIONS = "Kraj ma przypisane należności!";

            public const string COUNTRY_SYMBOL_NOT_VALID = "Podany symbol kraju jest niepoprawny! Dopuszczalne są tylko wielkie litery.";
            public const string COUNTRY_NAME_NOT_VALID = "Podana nazwa kraju jest niepoprawna! Dopuszczalne są tylko litery.";

            public const string COUNTRY_REQUIRED_DATA_NOT_PROVIDED = "Dla dodania nowego kraju trzeba wypełnić wszystkie wymagane pola: Symbol oraz Nazwę kraju.";

            public const string COUNTRY_IS_NOT_UNIQUE = "Kraj z podanymi danymi już istnieje!";

            public const string COUNTRY_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem operacji trzeba najpierw wybrać kraj z listy.";

            //Department
            public const string DEPARTMENT_HAS_EMPLOYEES = "Biuro ma przypisanych pracowników!";

            public const string DEPARTMENT_ADDRESS_NOT_VALID = "Podany adres biura jest niepoprawny! Dopuszczalne są tylko litery, liczby, spacje oraz . , / -.";
            public const string DEPARTMENT_CAPACITY_NOT_VALID = "Podana pojemność biura jest niepoprawna! Dopuszczalne są tylko liczby.";

            public const string DEPARTMENT_REQUIRED_DATA_NOT_PROVIDED = "Dla dodania nowego biura trzeba wypełnić wszystkie wymagane pola: Adres, Pojemność oraz Kraj.";

            public const string DEPARTMENT_IS_NOT_UNIQUE = "Biuro z podanymi danymi już istnieje!";

            public const string DEPARTMENT_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem operacji trzeba najpierw wybrać biuro z listy.";

            public const string DEPARTMENT_HAVE_NOT_CAPACITY_FOR_EMPLOYEE = "Ilość pracowników w biurze osiągnęła maksimum. Wybierz inne biuro!";

            //Role
            public const string ROLE_HAS_EMPLOYEES = "Rola ma przypisanych pracowników!";

            public const string ROLE_CODE_NOT_VALID = "Podany kod roli jest niepoprawny! Dopuszczalne są tylko wielkie litery.";
            public const string ROLE_NAME_NOT_VALID = "Podana nazwa roli jest niepoprawna! Dopuszczalne są tylko litery, spacje oraz -.";
            public const string ROLE_PRIORITY_NOT_VALID = "Podany priorytet roli jest niepoprawny! Dopuszczalne są tylko liczby.";

            public const string ROLE_REQUIRED_DATA_NOT_PROVIDED = "Aby dodać nową rolę trzeba wypełnić wszystkie wymagane pola: Kod, Nazwa oraz Priorytet roli.";

            public const string ROLE_PRIORITY_IS_NOT_UNIQUE = "Podany priorytet roli jest już wykorzystywany!";

            public const string ROLE_IS_NOT_UNIQUE = "Rola z podanymi danymi już istnieje!";

            public const string ROLE_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem operacji trzeba najpierw wybrać rolę z listy.";

            //Deduction
            public const string DEDUCTION_ATTACHED_TO_PAYMENT = "Obliczenie jest przypisane do Płatności!";

            public const string DEDUCTION_TYPE_MUST_BE_SELECTED = "Typ obliczenia nie jest wybrany!";
            public const string DEDUCTION_NAME_NOT_VALID = "Podana nazwa obliczenia jest niepoprawna! Dopuszczalne są tylko litery, liczby, spacje, \\, / oraz -.";
            public const string DEDUCTION_VALUE_NOT_VALID = "Podana wartość obliczenia jest niepoprawna! Dopuszczalne są tylko liczby oraz ,.";
            public const string DEDUCTION_PERCENTAGE_VALUE_NOT_VALID = "Podana procentowa wartość obliczenia jest niepoprawna! Procentowe obliczenie nie może przekraczać 100%";

            public const string DEDUCTION_FROM_LIST_MUST_BE_SELECTED = "Przed wykonaniem tej operacji trzeba najpierw wybrać obliczenie z listy.";

            public const string DEDUCTION_REQUIRED_DATA_NOT_PROVIDED = "Dla dodania nowego obliczenia trzeba wypełnić wszystkie wymagane pola: Nazwa, Wartość oraz Typ obliczenia.";

            public const string DEDUCTION_IS_NOT_UNIQUE = "Obliczenie z podanymi danymi już istnieje!";

            //WorkSchedule
            public const string WORK_HOURS_NOT_VALID = "Podany priorytet roli jest niepoprawny! Dopuszczalne są tylko liczby.";
            
            public const string WORK_SCHEDULE_DATA_NOT_PROVIDED = "Dla dodania nowego planu pracy trzeba wypełnić wszystkie wymagane pola: Godzina rozpoczęcia oraz Ilość godzin w pracy.";           
            
            public const string WORK_SCHEDULE_HAS_ACTIVE_PAYMENTS = "Dla danego planu pracy istnieją aktywne płatności. Najpierw trzeba ich rozliczyć!";           
        }

        public static class Confirmation
        {
            public const string EXIT_WITHOUT_SAVE_CONFIRAMTION = "Twoje zmiany nie zostaną zapisane automatycznie, czy napewno chcesz wyjść?";
            public const string EMPLOYEE_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć pracownika: {0}?";
            public const string COUNTRY_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć kraj o nazwie: {0}?";
            public const string DEPARTMENT_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć biuro pod adresem: {0}?";
            public const string ROLE_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć rolę: {0}?";
            public const string DEDUCTION_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć obliczenia: {0}?";
            public static string EMPLOYEE_IS_PAID_IN_THIS_MONTH = "Podany pracownik {0} posiada już aktywne lub rozliczone płatności. Czy napewno chcesz kontynuować?";
            public const string WORK_SCHEDULE_EDIT_CONFIRMATION = "Czy napewno chcesz wprowadzić plan pracy w terminie: {0}?";
            public const string WORK_SCHEDULE_DELETE_CONFIRMATION = "Czy napewno chcesz usunąć plan pracy w terminie: {0}?";
        }
    }
}