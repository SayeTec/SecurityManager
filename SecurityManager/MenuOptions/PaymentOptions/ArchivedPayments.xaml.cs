using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic.Filters;
using SecurityManager_Fun.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions.PaymentOptions
{
    /// <summary>
    /// Interaction logic for ArchivedPayments.xaml
    /// </summary>
    public partial class ArchivedPayments : Window
    {
        private SalariesWindow previousWindow;

        public ArchivedPayments()
        {
            InitializeComponent();

            LoadData();
        }

        public ArchivedPayments(SalariesWindow salaries)
        {
            InitializeComponent();

            previousWindow = salaries;

            LoadData();
        }

        private void LoadData()
        {
            LoadDataForPayments();
            ClearFilter();
        }

        private void LoadDataForPayments()
        {
            DataGridPayments.ItemsSource = PaymentRepository.GetInactivePayments();
            ComboBoxCountry.ItemsSource = CountryRepository.GetAllCountries();
            ComboBoxCompany.ItemsSource = DepartmentRepository.GetAllDepartments();
            ComboBoxEmployee.ItemsSource = EmployeeRepository.GetAllEmployees();
            ComboBoxPayStatus.ItemsSource = new List<Payment.StatusType> { Payment.StatusType.Done, Payment.StatusType.Canceled };
        }

        private void ButtonRepeatPayment_Click(object sender, RoutedEventArgs e)
        {
            Payment? payment = DataGridPayments.SelectedItem as Payment;

            if (payment == null)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RegisterPayment registerPayment = new RegisterPayment(payment, this);
            registerPayment.ShowDialog();
            Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonFilterEmployees_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (CalendarDateFilter.SelectedDates.Count != 0)
            {
                startDate = CalendarDateFilter.SelectedDates[0];
                endDate = CalendarDateFilter.SelectedDates[CalendarDateFilter.SelectedDates.Count - 1];
            }

            DataGridPayments.ItemsSource = PaymentFilter.FilterArchivisedPayments(startDate, endDate,
                ComboBoxCountry.SelectedItem as Country, ComboBoxCompany.SelectedItem as Department,
                (Payment.StatusType?)ComboBoxPayStatus.SelectedItem, ComboBoxEmployee.SelectedItem as Employee);
            DataGridPayments.Items.Refresh();
        }

        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ClearFilter()
        {
            ComboBoxCountry.SelectedItem = null;
            ComboBoxCompany.SelectedItem = null;
            ComboBoxEmployee.SelectedItem = null;
            ComboBoxPayStatus.SelectedItem = null;
            CalendarDateFilter.SelectedDates.Clear();
        }

        private void ComboBoxCountry_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxCompany.ItemsSource = null;
            ComboBoxEmployee.ItemsSource = null;

            Country selectedCountry = ComboBoxCountry.SelectedItem as Country;

            if (selectedCountry != null)
            {
                ComboBoxCompany.ItemsSource = DepartmentRepository.GetDepartmentsByCountry(selectedCountry);
                ComboBoxEmployee.ItemsSource = EmployeeRepository.GetEmployeesByCountry(selectedCountry);
                return;
            }

            ComboBoxCompany.ItemsSource = DepartmentRepository.GetAllDepartments();
            ComboBoxEmployee.ItemsSource = EmployeeRepository.GetAllEmployees();
        }

        private void ComboBoxCompany_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxEmployee.ItemsSource = null;

            Department selectedDepartment = ComboBoxCompany.SelectedItem as Department;

            if (selectedDepartment != null)
            {
                ComboBoxEmployee.ItemsSource = EmployeeRepository.GetEmployeesByDepartment(selectedDepartment);
                return;
            }

            ComboBoxEmployee.ItemsSource = EmployeeRepository.GetAllEmployees();
        }
    }
}
