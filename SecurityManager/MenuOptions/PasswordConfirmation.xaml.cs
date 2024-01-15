using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for PasswordConfirmation.xaml
    /// </summary>
    public partial class PasswordConfirmation : Window
    {
        private Employee employeeToConfirm;
        private string confirmationPurpose;
        private Window previousWindow;
        public PasswordConfirmation(Employee employee, string purpose, Window previous)
        {
            InitializeComponent();
            employeeToConfirm = employee;
            confirmationPurpose = purpose;
            previousWindow = previous;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (previousWindow != null)
            {
                previousWindow.Show();
            }
            Close();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!AccountService.VerifyPassword(PasswordBoxLoggedUserPassword.Password, Session.Instance.CurrentEmployee.Password))
            {
                MessageBox.Show("Proszę podaj swoje poprawne hasło!", "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (confirmationPurpose)
            {
                case "deletion":
                    EmployeeRepository.DeleteEmployee(employeeToConfirm);
                    break;

                case "add":
                    AccountService.RegisterNewEmployee(employeeToConfirm);
                    break;

                case "update":
                    EmployeeRepository.UpdateEmployee(employeeToConfirm);
                    break;
            }

            if (previousWindow != null)
            {
                previousWindow.Close();
            }

            Close();
        }
    }
}
