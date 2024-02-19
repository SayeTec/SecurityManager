using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Collections.Generic;
using System.Windows;
using static SecurityManager_Fun.Model.Deduction;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.DeductionManagement
{
    /// <summary>
    /// Interaction logic for AddOrEditDeductionWindow.xaml
    /// </summary>
    public partial class AddOrEditDeductionWindow : Window
    {
        private Deduction deductionToEdit;
        public AddOrEditDeductionWindow()
        {
            InitializeComponent();

            LoadDataFromDB();
        }

        public AddOrEditDeductionWindow(Deduction deduction)
        {
            InitializeComponent();
            deductionToEdit = deduction;

            LoadDataFromDeduction();
        }

        private void LoadDataFromDeduction()
        {
            TextBoxName.Text = deductionToEdit.Name;
            TextBoxValue.Text = deductionToEdit.Value.ToString();
            CheckBoxIsPercentage.IsChecked = deductionToEdit.IsPercentage;
            TextBoxDescription.Text = deductionToEdit.Description;
            LoadDataFromDB();
            ComboboxCountry.SelectedItem = deductionToEdit.Country;
            ComboboxType.SelectedItem = deductionToEdit.Type;
        }

        private void LoadDataFromDB()
        {
            List<Country> countries = CountryRepository.GetAllCountries();
            countries.Insert(0, null);
            ComboboxCountry.ItemsSource = countries;
            ComboboxType.ItemsSource = new List<DeductionType>() { DeductionType.Tax, DeductionType.Bonus };
        }

        private Deduction GetDeductionWithData()
        {
            bool isPercentage = CheckBoxIsPercentage.IsChecked == true;
            decimal convertingResult;

            return new Deduction()
            {
                ID = deductionToEdit.ID,
                Name = TextBoxName.Text,
                Type = (DeductionType)ComboboxType.SelectedItem,
                Value = decimal.TryParse(TextBoxValue.Text, out convertingResult) ? convertingResult : 0m,
                IsPercentage = isPercentage,
                Description = TextBoxDescription.Text,
                CountryID = (ComboboxCountry.SelectedItem as Country)?.ID
            };
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text) || string.IsNullOrEmpty(TextBoxValue.Text)
                || ComboboxType.SelectedItem == null)
            {
                MessageBox.Show(DisplayMessages.Error.DEDUCTION_REQUIRED_DATA_NOT_PROVIDED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";
            bool isPercentage = CheckBoxIsPercentage.IsChecked == true;
            decimal convertingResult;

            Deduction deduction = new Deduction()
            {
                Name = TextBoxName.Text,
                Type = (DeductionType)ComboboxType.SelectedItem,
                Value = decimal.TryParse(TextBoxValue.Text, out convertingResult) ? convertingResult : 0m, 
                IsPercentage = isPercentage,
                Description = TextBoxDescription.Text,
                CountryID = (ComboboxCountry.SelectedItem as Country)?.ID
            };

            if (!ValuesValidation.ValidateDeductionName(TextBoxName.Text))
                errorMessage += $"{DisplayMessages.Error.DEDUCTION_NAME_NOT_VALID}\n";

            if (!ValuesValidation.ValidateDeductionValue(TextBoxValue.Text))
                errorMessage += $"{DisplayMessages.Error.DEDUCTION_VALUE_NOT_VALID}\n";

            if (CheckBoxIsPercentage.IsChecked == true && convertingResult > ApplicationConstants.MAX_PERCENTAGE_DEDUCTION_VALUE)
                errorMessage += $"{DisplayMessages.Error.DEDUCTION_PERCENTAGE_VALUE_NOT_VALID}\n";
            
            if (!DeductionRepository.CheckIfDeductionIsUnique(deduction))
                errorMessage += $"{DisplayMessages.Error.DEDUCTION_IS_NOT_UNIQUE}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string action = "add-deduction";

            if (deductionToEdit != null)
            {
                action = "update-deduction";
                deduction = GetDeductionWithData();
            }

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(deduction, action, this);
            passwordConfirmation.ShowDialog();
            Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (deductionToEdit == null)
            {
                Close();
                return;
            }

            if (deductionToEdit.Equals(GetDeductionWithData()))
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
