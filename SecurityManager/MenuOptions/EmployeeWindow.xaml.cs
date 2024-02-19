using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Logic.Filters;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions;
using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;

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

            ButtonEditEmployee.Visibility = Visibility.Collapsed;
            ButtonDeleteEmployee.Visibility = Visibility.Collapsed;

            DataGridEmployees.Language = XmlLanguage.GetLanguage("pl-PL");
            LoadDataFromDB();
        }

        private void LoadDataFromDB() 
        {
            ClearFilter();
            LoadEmployeesFromDB();
            LoadFilterComboBoxesFromDB();
        }

        private void LoadEmployeesFromDB()
        {
            DataGridEmployees.ItemsSource = EmployeeRepository.GetEmployeesUnderPriority(Session.Instance.CurrentEmployee.Role.Priority);
            DataGridEmployees.Items.Refresh();
        }

        private void LoadFilterComboBoxesFromDB()
        {
            RoleComboBox.ItemsSource = RoleRepository.GetRolesUnderEmployeePriority(Session.Instance.CurrentEmployee);
            CountryComboBox.ItemsSource = CountryRepository.GetAllCountries();
            CompanyComboBox.ItemsSource = DepartmentRepository.GetAllDepartments();
        }

        private void ClearFilter()
        {
            TextBoxEmployeeNamePattern.Text = string.Empty;
            TextBoxEmployeeSurnamePattern.Text = string.Empty;
            RoleComboBox.SelectedItem = null;
            CountryComboBox.SelectedItem = null;
            CompanyComboBox.SelectedItem = null;
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
            LoadDataFromDB();
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
            LoadDataFromDB();
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

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.EMPLOYEE_DELETE_CONFIRMATION, employeeToDelete.FullName), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PasswordConfirmation passwordConfirmation = new PasswordConfirmation(employeeToDelete, "delete-employee", null);
                passwordConfirmation.ShowDialog();
                DataGridEmployees.SelectedItem = null;
                LoadDataFromDB();
            }
        }

        private void ButtonFilterEmployees_Click(object sender, RoutedEventArgs e)
        {
            DataGridEmployees.ItemsSource = EmployeeFilter.FilterEmployeesUnderPriority(Session.Instance.CurrentEmployee.Role.Priority,
                TextBoxEmployeeNamePattern.Text, TextBoxEmployeeSurnamePattern.Text, RoleComboBox.SelectedItem as Role,
                CountryComboBox.SelectedItem as Country, CompanyComboBox.SelectedItem as Department);
            DataGridEmployees.Items.Refresh();
        }

        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromDB();
        }

        private void DataGridEmployees_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DependencyObject element = (UIElement)e.OriginalSource;

            while (element != null && !(element is DataGridRow))
            {
                if (element is DataGridColumnHeader)
                {
                    return;
                }

                element = VisualTreeHelper.GetParent(element);
            }

            DataGridRow clickedRow = (DataGridRow)element;

            if (clickedRow != null)
            {
                if (DataGridEmployees.SelectedItem == clickedRow.DataContext)
                {
                    DataGridEmployees.SelectedItem = null;
                    e.Handled = true;
                }
            }
        }

        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CompanyComboBox.ItemsSource = null;

            Country selectedCountry = CountryComboBox.SelectedItem as Country;

            if (selectedCountry != null)
            {
                CompanyComboBox.ItemsSource = DepartmentRepository.GetDepartmentsByCountry(selectedCountry);
                return;
            }

            CompanyComboBox.ItemsSource = DepartmentRepository.GetAllDepartments();
        }

        private void DataGridEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEmployees.SelectedItem != null)
            {
                ButtonEditEmployee.Visibility = Visibility.Visible;
                ButtonDeleteEmployee.Visibility = Visibility.Visible;
                return;
            }

            ButtonEditEmployee.Visibility = Visibility.Collapsed;
            ButtonDeleteEmployee.Visibility = Visibility.Collapsed;
        }
    }
}
