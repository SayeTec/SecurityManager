using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SecurityManager_GUI.MenuOptions.PaymentOptions
{
    /// <summary>
    /// Interaction logic for RegisterPayment.xaml
    /// </summary>
    public partial class RegisterPayment : Window
    {
        public Employee selectedEmployee;
        public List<Deduction> selectedDeductions = new List<Deduction>();
        public Payment paymentToEdit;

        public RegisterPayment()
        {
            InitializeComponent();
        }

        public RegisterPayment(Employee employee)
        {
            InitializeComponent();
            
            selectedEmployee = employee;

            TextBlockSelectedEmployee.Text = selectedEmployee.FullName;
            TextBlockSelectedEmployee.Foreground = Brushes.Green;
        }

        public RegisterPayment(Payment payment)
        {
            InitializeComponent();

            selectedEmployee = payment.Employee;
            selectedDeductions = DeductionRepository.GetDeductionsForPayment(payment);
            paymentToEdit = payment;

            TextBlockSelectedEmployee.Text = selectedEmployee.FullName;
            TextBlockSelectedEmployee.Foreground = Brushes.Green;

            TextBlockSelectedTaxValue.Text = ShowDeductions(Deduction.DeductionType.Tax);
            TextBlockSelectedBonusValue.Text = ShowDeductions(Deduction.DeductionType.Bonus);
            TextBlockSumValue.Text = ShowSum();

            ButtonCreate.Content = "Edytuj";
            ButtonCreate.Click -= ButtonCreate_Click;
            ButtonCreate.Click += ButtonUpdate_Click;
        }

        public RegisterPayment(Payment payment, Window previousWindow)
        {
            InitializeComponent();

            if (previousWindow is ArchivedPayments) 
            {
                selectedEmployee = payment.Employee;
                selectedDeductions = DeductionRepository.GetDeductionsForPayment(payment);
                paymentToEdit = payment;

                TextBlockSelectedEmployee.Text = selectedEmployee.FullName;
                TextBlockSelectedEmployee.Foreground = Brushes.Green;

                TextBlockSelectedTaxValue.Text = ShowDeductions(Deduction.DeductionType.Tax);
                TextBlockSelectedBonusValue.Text = ShowDeductions(Deduction.DeductionType.Bonus);
                TextBlockSumValue.Text = ShowSum();
            }
        }

        private void ButtonSelectEmployee_Click(object sender, RoutedEventArgs e)
        {
            SelectEmployee selectEmployee = new SelectEmployee(this);
            selectEmployee.ShowDialog();

            if (selectedEmployee != null)
            {
                TextBlockSelectedEmployee.Text = selectedEmployee.FullName;
                TextBlockSelectedEmployee.Foreground = Brushes.Green;

                TextBlockSelectedTaxValue.Text = ShowDeductions(Deduction.DeductionType.Tax);
                TextBlockSelectedBonusValue.Text = ShowDeductions(Deduction.DeductionType.Bonus);
                TextBlockSumValue.Text = ShowSum();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null && selectedDeductions.Count == 0) 
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

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show(DisplayMessages.Error.EMPLOYEE_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CalculateSum() < 0)
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_AMOUNT_IS_OUT_OF_RANGE, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Payment payment = new Payment()
            {
                DateOfCreation = DateTime.Now,
                Status = 0,
                DefaultValue = selectedEmployee.GrossRate * CalculateFactor(),
                DeductionsValue = -CalculateDeductions(Deduction.DeductionType.Tax) + CalculateDeductions(Deduction.DeductionType.Bonus),
                FinalValue = CalculateSum(),
                EmployeeID = selectedEmployee.ID
            };

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(payment, selectedDeductions.Cast<object>().ToList(), "add-payment", this);
            passwordConfirmation.ShowDialog();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee == null)
            {
                MessageBox.Show(DisplayMessages.Error.EMPLOYEE_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CalculateSum() < selectedEmployee.GrossRate * (1 - ApplicationConstants.RATE_VALUE_DIFFERENCE) || CalculateSum() > selectedEmployee.GrossRate * (1 + ApplicationConstants.RATE_VALUE_DIFFERENCE))
            {
                MessageBox.Show(DisplayMessages.Error.PAYMENT_AMOUNT_IS_OUT_OF_RANGE, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            paymentToEdit.Employee = selectedEmployee;
            paymentToEdit.DefaultValue = selectedEmployee.GrossRate * CalculateFactor();
            paymentToEdit.DeductionsValue = -CalculateDeductions(Deduction.DeductionType.Tax) + CalculateDeductions(Deduction.DeductionType.Bonus);
            paymentToEdit.FinalValue = CalculateSum();

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(paymentToEdit, selectedDeductions.Cast<object>().ToList(), "update-payment", this);
            passwordConfirmation.ShowDialog();
        }

        private void ButtonSelectTax_Click(object sender, RoutedEventArgs e)
        {
            SelectDeductions selectDeductions = new SelectDeductions(this, Deduction.DeductionType.Tax);
            selectDeductions.ShowDialog();

            if (selectedEmployee != null)
            {
                TextBlockSelectedTaxValue.Text = ShowDeductions(Deduction.DeductionType.Tax);
                TextBlockSumValue.Text = ShowSum();
            }
        }

        private void ButtonSelectBonus_Click(object sender, RoutedEventArgs e)
        {
            SelectDeductions selectDeductions = new SelectDeductions(this, Deduction.DeductionType.Bonus);
            selectDeductions.ShowDialog();

            if (selectedEmployee != null)
            {
                TextBlockSelectedBonusValue.Text = ShowDeductions(Deduction.DeductionType.Bonus);
                TextBlockSumValue.Text = ShowSum();
            }
        }

        private string ShowDeductions(Deduction.DeductionType type)
        {
            return $"({selectedDeductions
                .Where(d => d.Type == type)
                .ToList().Count}) " +
                (type == Deduction.DeductionType.Tax ? "-" : "") +
                $"{(selectedDeductions
                .Where(d => d.Type == type)
                .Sum(d => d.IsPercentage ? d.Value * selectedEmployee.GrossRate : d.Value)).ToString("F2")}$";
        }

        private decimal CalculateDeductions(Deduction.DeductionType type)
        {
            return selectedDeductions
                .Where(d => d.Type == type)
                .Sum(d => d.IsPercentage ? d.Value * selectedEmployee.GrossRate : d.Value);
        }

        private decimal CalculateFactor()
        {
            bool editionOfPayment = paymentToEdit != null;
            return WorkScheduleRepository.GetSumHoursInWorkForEmployeeByMonth(
                editionOfPayment ? paymentToEdit.DateOfCreation : DateTime.Now,
                selectedEmployee);
        }

        private decimal CalculateSum()
        {
            return selectedEmployee.GrossRate *
                CalculateFactor() - 
                CalculateDeductions(Deduction.DeductionType.Tax) + 
                CalculateDeductions(Deduction.DeductionType.Bonus);
        }

        private string ShowSum()
        {
            return $"{CalculateSum().ToString("F2")}$";
        }
    }
}
