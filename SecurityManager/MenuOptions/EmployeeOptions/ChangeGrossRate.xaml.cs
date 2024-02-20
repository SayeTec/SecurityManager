using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions.EmployeeOptions
{
    /// <summary>
    /// Interaction logic for ChangeGrossRate.xaml
    /// </summary>
    public partial class ChangeGrossRate : Window
    {
        private Employee employeeToEdit;

        public ChangeGrossRate(Employee employee)
        {
            InitializeComponent();
            employeeToEdit = employee;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!AccountService.VerifyPassword(PasswordBoxLoggedUserPassword.Password, Session.Instance.CurrentEmployee.Password))
            {
                PasswordBoxLoggedUserPassword.Password = string.Empty;
                MessageBox.Show(DisplayMessages.Error.LOGGED_USER_PASSWORD_CONFIRMATION_ERROR, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TextBoxNewGrossRate.Text))
            {
                PasswordBoxLoggedUserPassword.Password = string.Empty;
                MessageBox.Show(DisplayMessages.Error.GROSSRATE_VALUE_DATA_NOT_PROVIDED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            if (!ValuesValidation.ValidateGrossRateValue(TextBoxNewGrossRate.Text))
                errorMessage += $"{DisplayMessages.Error.GROSSRATE_VALUE_NOT_VALID}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                PasswordBoxLoggedUserPassword.Password = string.Empty;
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal convertingResult;
            employeeToEdit.GrossRate = decimal.TryParse(TextBoxNewGrossRate.Text, out convertingResult) ? convertingResult : 0m;
            EmployeeRepository.UpdateEmployee(employeeToEdit);
            Close();
        }
    }
}
