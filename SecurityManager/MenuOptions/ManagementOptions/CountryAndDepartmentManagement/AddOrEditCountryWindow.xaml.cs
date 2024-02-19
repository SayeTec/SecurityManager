using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.CountryAndDepartmentManagement
{
    /// <summary>
    /// Interaction logic for AddCountryWindow.xaml
    /// </summary>
    public partial class AddOrEditCountryWindow : Window
    {
        //TODO: Improve error messages 

        private Country countryToEdit;
        public AddOrEditCountryWindow()
        {
            InitializeComponent();
        }

        public AddOrEditCountryWindow(Country country)
        {
            InitializeComponent();
            countryToEdit = country;

            LoadDataFromCountry();
        }

        private void LoadDataFromCountry()
        {
            TextBoxSymbol.Text = countryToEdit.Symbol;
            TextBoxName.Text = countryToEdit.Name;
        }

        private Country GetCountryWithData()
        {
            return new Country()
            {
                ID = countryToEdit.ID,
                Name = TextBoxName.Text,
                Symbol = TextBoxSymbol.Text
            };
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text)
                || string.IsNullOrEmpty(TextBoxSymbol.Text))
            {
                MessageBox.Show(DisplayMessages.Error.COUNTRY_REQUIRED_DATA_NOT_PROVIDED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            Country country = new Country()
            {
                Name = TextBoxName.Text,
                Symbol = TextBoxSymbol.Text,
            };

            if (!ValuesValidation.ValidateCountrySymbol(TextBoxSymbol.Text))
                errorMessage += $"{DisplayMessages.Error.COUNTRY_SYMBOL_NOT_VALID}\n";

            if (!ValuesValidation.ValidateCountryName(TextBoxName.Text))
                errorMessage += $"{string.Format(DisplayMessages.Error.COUNTRY_NAME_NOT_VALID)}\n";
            
            if (!CountryRepository.CheckIfCountryIsUnique(country))
                errorMessage += $"{string.Format(DisplayMessages.Error.COUNTRY_IS_NOT_UNIQUE)}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string action = "add-country";

            if (countryToEdit != null)
            {
                action = "update-country";
                country = GetCountryWithData();
            }

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(country, action, this);
            passwordConfirmation.ShowDialog();
            Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (countryToEdit == null)
            {
                Close();
                return;
            }

            if (countryToEdit.Equals(GetCountryWithData()))
            {
                Close();
                return;
            }

            MessageBoxResult result = MessageBox.Show(DisplayMessages.Confirmation.EXIT_WITHOUT_SAVE_CONFIRAMTION, "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
