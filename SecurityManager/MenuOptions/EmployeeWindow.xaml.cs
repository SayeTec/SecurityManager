using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions;
using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace SecurityManager_GUI
{
    /// <summary>
    /// Interaction logic for EmoloyeeWindow.xaml
    /// </summary>
    /// 

    //Employee list with options to add, edit and delete employees
    //First is loading all hours from all objects and if object is selected it shows only hours from this object
    //Data is loaded for month after 11th day of month (if it is March 7th it will show hours from February, [IMPORTANTE KOMANDORE BOMBARDIERO])
    //If date is selected it will show employees with their data only from this date

    public partial class EmployeeWindow : Window
    {
        private static EmployeeWindow _instance = new EmployeeWindow();
        public static EmployeeWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmployeeWindow();
                }
                return _instance;
            }
        }

        public EmployeeWindow()
        {
            InitializeComponent();
            DataGridEmployees.Language = XmlLanguage.GetLanguage("pl-PL");
            LoadEmployeesFromDB();
        }

        public void LoadEmployeesFromDB()
        {
            DataGridEmployees.ItemsSource = EmployeeRepository.GetAllEmployees();
            DataGridEmployees.Items.Refresh();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
            DataGridEmployees.SelectedItem = null;
            LoadEmployeesFromDB();
        }

        private void ButtonEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee? employeeToEdit = DataGridEmployees.SelectedItem as Employee;

            if (employeeToEdit == null)
            {
                MessageBox.Show(DisplayMessages.Error.EMPLOYEE_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(employeeToEdit);
            editEmployeeWindow.ShowDialog();
            DataGridEmployees.SelectedItem = null;
            LoadEmployeesFromDB();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void ButtonDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee? employeeToDelete = DataGridEmployees.SelectedItem as Employee;

            if (employeeToDelete == null)
            {
                MessageBox.Show(DisplayMessages.Error.EMPLOYEE_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.EMPLOYEE_DELETE_CONFIRMATION, employeeToDelete.GetFullName()), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PasswordConfirmation passwordConfirmation = new PasswordConfirmation(employeeToDelete, "deletion", null);
                passwordConfirmation.ShowDialog();
                DataGridEmployees.SelectedItem = null;
                LoadEmployeesFromDB();
            }
        }
    }
}
