using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;
using System.Windows.Controls;

namespace SecurityManager_GUI.MenuOptions.EmployeeOptions
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private Employee employeeToEdit;

        public ChangePassword(Employee employee)
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
                ClearTextBoxes();
                MessageBox.Show(DisplayMessages.Error.LOGGED_USER_PASSWORD_CONFIRMATION_ERROR, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string newPassword = PasswordBoxNewPassword.Password;
            string passwordRepeat = PasswordBoxRepeatPassword.Password;

            if (!newPassword.Equals(passwordRepeat) ||
                string.IsNullOrEmpty(newPassword) ||
                string.IsNullOrEmpty(passwordRepeat))
            {
                ClearTextBoxes();
                MessageBox.Show(DisplayMessages.Error.PASSWORD_REPEAT_CONFIRMATION_ERROR, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            if (!ValuesValidation.ValidatePasswordHasAtLeastOneUppercase(newPassword))
                errorMessage += $"{DisplayMessages.Error.PASSWORD_NOT_HAS_UPPERCASE}\n";

            if (!ValuesValidation.ValidatePasswordHasAtLeastOneLowercase(newPassword))
                errorMessage += $"{DisplayMessages.Error.PASSWORD_NOT_HAS_LOWERCASE}\n";

            if (!ValuesValidation.ValidatePasswordHasAtLeastOneDigit(newPassword))
                errorMessage += $"{DisplayMessages.Error.PASSWORD_NOT_HAS_DIGIT}\n";

            if (!ValuesValidation.ValidatePasswordHasAtLeastOneSpecialChar(newPassword))
                errorMessage += $"{DisplayMessages.Error.PASSWORD_NOT_HAS_SPECIAL_CHAR}\n";

            if (!ValuesValidation.ValidatePasswordMeetsMinimumLength(newPassword))
                errorMessage += $"{DisplayMessages.Error.PASSWORD_NOT_HAS_MINIMUM_LENGTH}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ClearTextBoxes();
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] salt;
            AccountService.HashPassword(newPassword, out salt);
            employeeToEdit.Password = DefaultValuesGenerator.GenerateDefaultEmployeePassword(
                AccountService.HashPassword(newPassword, out salt), salt);

            EmployeeRepository.UpdateEmployee(employeeToEdit);
            Close();
        }

        private void ClearTextBoxes()
        {
            PasswordBoxLoggedUserPassword.Password = string.Empty;
            PasswordBoxNewPassword.Password = string.Empty;
            PasswordBoxRepeatPassword.Password = string.Empty;
        }
    }
}
