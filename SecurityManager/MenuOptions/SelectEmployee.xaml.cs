using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Logic.Filters;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions.PaymentOptions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for SelectEmployee.xaml
    /// </summary>
    public partial class SelectEmployee : Window
    {
        //TODO: Implement "Paid in this month" Column

        private Window previousWindow;

        public SelectEmployee(Window window)
        {
            InitializeComponent();
            previousWindow = window;

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
            if (previousWindow is SalariesWindow)
            {
                DataGridEmployees.ItemsSource = EmployeeRepository/*.GetEmployeesUnderPriority((int)Session.Instance.CurrentEmployee.Role.Priority);*/
                                                              .GetUnpaidEmployees();
                DataGridEmployees.Items.Refresh();

                DataGridEmployees.Columns[5].Visibility = Visibility.Hidden;

                return;
            }

            DataGridEmployees.ItemsSource = EmployeeRepository/*.GetEmployeesUnderPriority((int)Session.Instance.CurrentEmployee.Role.Priority);*/
                                                              .GetAllEmployees();
            DataGridEmployees.Items.Refresh();
        }

        private void LoadFilterComboBoxesFromDB()
        {
            RoleComboBox.ItemsSource = RoleRepository/*.GetRolesUnderEmployeePriority(Session.Instance.CurrentEmployee);*/
                                                     .GetAllRoles();
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

        private void ButtonSelectEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee? employee = DataGridEmployees.SelectedItem as Employee;

            if (employee == null)
            {
                MessageBox.Show("Nie wolno!");
                return;
            }

            //TODO: Add confirmation if Employee was paid for this month

            if (previousWindow is RegisterPayment)
            {
                (previousWindow as RegisterPayment).selectedEmployee = employee;
                (previousWindow as RegisterPayment).selectedDeductions.RemoveAll(d =>
                {
                    if (d.CountryID == null)
                    {
                        return false;
                    }

                    if ((previousWindow as RegisterPayment).selectedEmployee.Department == null)
                    {
                        return true;
                    }

                    return !d.Country.Equals((previousWindow as RegisterPayment).selectedEmployee.Department.Country);
                });
            }
            else if (previousWindow is SalariesWindow)
            {
                RegisterPayment registerPayment = new RegisterPayment(employee);
                Close();
                registerPayment.ShowDialog();
            }

            Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void ButtonFilterEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (previousWindow is RegisterPayment) 
            {
                DataGridEmployees.ItemsSource = EmployeeFilter.FilterEmployees(TextBoxEmployeeNamePattern.Text, TextBoxEmployeeSurnamePattern.Text, RoleComboBox.SelectedItem as Role,
                CountryComboBox.SelectedItem as Country, CompanyComboBox.SelectedItem as Department);
                DataGridEmployees.Items.Refresh();
            }
            else if (previousWindow is SalariesWindow)
            {
                DataGridEmployees.ItemsSource = EmployeeFilter.FilterEmployeesForPaymentCreation(TextBoxEmployeeNamePattern.Text, TextBoxEmployeeSurnamePattern.Text, RoleComboBox.SelectedItem as Role,
                CountryComboBox.SelectedItem as Country, CompanyComboBox.SelectedItem as Department);
                DataGridEmployees.Items.Refresh();
            }
        }

        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromDB();
        }

        private void DataGridEmployees_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
