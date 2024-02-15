using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for PasswordConfirmation.xaml
    /// </summary>
    public partial class PasswordConfirmation : Window
    {
        private object objectToConfirm;
        private List<object> associatedObjects;
        private string confirmationPurpose;
        private Window previousWindow;


        public PasswordConfirmation(object objectToConfirm, string purpose, Window previous)
        {
            InitializeComponent();
            this.objectToConfirm = objectToConfirm;
            confirmationPurpose = purpose;
            previousWindow = previous;
        }

        public PasswordConfirmation(object objectToConfirm, List<object> associatedObjects, string purpose, Window previous)
        {
            InitializeComponent();
            this.objectToConfirm = objectToConfirm;
            this.associatedObjects = associatedObjects;
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
                MessageBox.Show(DisplayMessages.Error.LOGGED_USER_PASSWORD_CONFIRMATION_ERROR, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                PasswordBoxLoggedUserPassword.Password = string.Empty;
                return;
            }

            switch (confirmationPurpose)
            {
                case "add-employee":
                    AccountService.RegisterNewEmployee(objectToConfirm as Employee);
                    break;

                case "delete-employee":
                    EmployeeRepository.DeleteEmployee(objectToConfirm as Employee);
                    break;

                case "update-employee":
                    EmployeeRepository.UpdateEmployee(objectToConfirm as Employee);
                    break;

                case "add-payment":
                    PaymentRepository.AddNewPayment(objectToConfirm as Payment, associatedObjects.Cast<Deduction>().ToList());
                    break;

                case "update-payment":
                    PaymentDeductionRepository.RemovePaymentDeductionsByPayment(objectToConfirm as Payment);
                    PaymentRepository.UpdatePaymentWithDeductions(objectToConfirm as Payment, associatedObjects.Cast<Deduction>().ToList());
                    break;

                case "update-status-payment":
                    PaymentRepository.UpdatePayment(objectToConfirm as Payment);
                    break;

                case "delete-payment":
                    PaymentRepository.UpdatePayment(objectToConfirm as Payment);
                    break;

                case "add-country":
                    CountryRepository.AddCountry(objectToConfirm as Country);
                    break;

                case "update-country":
                    CountryRepository.UpdateCountry(objectToConfirm as Country);
                    break;

                case "delete-country":
                    CountryRepository.DeleteCountry(objectToConfirm as Country);
                    break;

                case "add-department":
                    DepartmentRepository.AddDepartment(objectToConfirm as Department);
                    break;

                case "update-department":
                    DepartmentRepository.UpdateDepartment(objectToConfirm as Department);
                    break;

                case "delete-department":
                    DepartmentRepository.DeleteDepartment(objectToConfirm as Department);
                    break;

                case "add-role":
                    RoleRepository.AddNewRole(objectToConfirm as Role);
                    break;

                case "update-role":
                    RoleRepository.UpdateRole(objectToConfirm as Role);
                    break;

                case "delete-role":
                    RoleRepository.DeleteRole(objectToConfirm as Role);
                    break;

                case "add-deduction":
                    DeductionRepository.AddNewDeduction(objectToConfirm as Deduction);
                    break;

                case "update-deduction":
                    DeductionRepository.UpdateDeduction(objectToConfirm as Deduction);
                    break;

                case "delete-deduction":
                    DeductionRepository.DeleteDeduction(objectToConfirm as Deduction);
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
