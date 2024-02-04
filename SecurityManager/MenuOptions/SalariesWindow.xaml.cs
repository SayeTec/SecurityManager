
using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic.Filters;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions.PaymentOptions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for SalariesWindow.xaml
    /// </summary>
    public partial class SalariesWindow : Window
    {
        public SalariesWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData() 
        {
            LoadDataFromDB();
            ClearFilter();
        }

        private void LoadDataFromDB()
        {
            DataGridPayments.ItemsSource = PaymentRepository.GetActivePayments();
            ComboBoxCountry.ItemsSource = CountryRepository.GetAllCountries();
            ComboBoxCompany.ItemsSource = DepartmentRepository.GetAllDepartments();
            ComboBoxEmployee.ItemsSource = EmployeeRepository.GetAllEmployees();
            ComboBoxPayStatus.ItemsSource = new List<Payment.StatusType> { Payment.StatusType.Created, Payment.StatusType.Commited };
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }

        private void ButtonCommitPayment_Click(object sender, RoutedEventArgs e)
        {
            Payment? payment = DataGridPayments.SelectedItem as Payment;

            if (payment == null)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (payment.Status != Payment.StatusType.Created)
            {
                MessageBox.Show(string.Format(DisplayMessages.Error.PAYMENT_HAS_INAPPROPRIATE_STATUS, Payment.StatusType.Created), "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            payment.IncreaseStatus();
            payment.DateOfLatestModification = DateTime.Now;
            PaymentRepository.UpdatePayment(payment);
            LoadData();
        }

        private void ButtonSolvePayment_Click(object sender, RoutedEventArgs e)
        {
            Payment? payment = DataGridPayments.SelectedItem as Payment;

            if (payment == null)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (payment.Status != Payment.StatusType.Commited)
            {
                MessageBox.Show(string.Format(DisplayMessages.Error.PAYMENT_HAS_INAPPROPRIATE_STATUS, Payment.StatusType.Commited), "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            payment.IncreaseStatus();

            payment.DateOfLatestModification = DateTime.Now;
            payment.DateOfEnd = DateTime.Now;

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(payment, "update-status-payment", null);
            passwordConfirmation.ShowDialog();
            LoadData();
        }

        private void ButtonRevert_Click(object sender, RoutedEventArgs e)
        {
            Payment? payment = DataGridPayments.SelectedItem as Payment;

            if (payment == null)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (payment.Status == Payment.StatusType.Created)
            {
                MessageBox.Show(string.Format(DisplayMessages.Error.PAYMENT_HAS_INAPPROPRIATE_STATUS, Payment.StatusType.Commited), "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            payment.DecreaseStatus();
            payment.DateOfLatestModification = DateTime.Now;
            PaymentRepository.UpdatePayment(payment);
            LoadData();
        }

        private void ButtonDeletePayment_Click(object sender, RoutedEventArgs e)
        {
            Payment? payment = DataGridPayments.SelectedItem as Payment;

            if (payment == null)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (payment.Status != Payment.StatusType.Created)
            {
                MessageBox.Show(string.Format(DisplayMessages.Error.PAYMENT_HAS_INAPPROPRIATE_STATUS, Payment.StatusType.Created), "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            payment.Cancel();

            payment.DateOfLatestModification = DateTime.Now;
            payment.DateOfEnd = DateTime.Now;

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(payment, "delete-payment", null);
            passwordConfirmation.ShowDialog();
            LoadData();
        }

        private void ButtonRegisterPayment_Click(object sender, RoutedEventArgs e)
        {
            RegisterPayment registerPayment = new RegisterPayment();
            registerPayment.ShowDialog();
            LoadData();
        }

        private void ButtonEditPayment_Click(object sender, RoutedEventArgs e)
        {
            Payment? payment = DataGridPayments.SelectedItem as Payment;

            if (payment == null)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (payment.Status != Payment.StatusType.Created)
            {
                MessageBox.Show(string.Format(DisplayMessages.Error.PAYMENT_HAS_INAPPROPRIATE_STATUS, Payment.StatusType.Created), "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RegisterPayment registerPayment = new RegisterPayment(payment);
            registerPayment.ShowDialog();
            LoadData();
        }

        private void ButtonArchivedPayment_Click(object sender, RoutedEventArgs e)
        {
            ArchivedPayments archivedPayments = new ArchivedPayments();
            archivedPayments.ShowDialog();
            LoadData();
        }

        private void ButtonSettleEmployee_Click(object sender, RoutedEventArgs e)
        {
            SelectEmployee selectEmployee = new SelectEmployee(this);
            selectEmployee.ShowDialog();
            LoadData();
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

            DataGridPayments.ItemsSource = PaymentFilter.FilterPayments(startDate, endDate, 
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
