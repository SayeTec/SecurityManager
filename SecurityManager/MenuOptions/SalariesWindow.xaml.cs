
using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions.PaymentOptions;
using System;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for SalariesWindow.xaml
    /// </summary>
    public partial class SalariesWindow : Window
    {
        /*
         * TODO: Messages to update
         */

        public SalariesWindow()
        {
            InitializeComponent();
            LoadDataFromDB();
        }

        private void LoadDataFromDB()
        {
            DataGridPayments.ItemsSource = PaymentRepository.GetActivePayments();
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
            LoadDataFromDB();
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
            //TODO: Check why data is not changed
            payment.DateOfLatestModification = DateTime.Now;
            payment.DateOfEnd = DateTime.Now;

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(payment, "update-status-payment", null);
            passwordConfirmation.ShowDialog();
            LoadDataFromDB();
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
            LoadDataFromDB();
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
            LoadDataFromDB();
        }

        private void ButtonRegisterPayment_Click(object sender, RoutedEventArgs e)
        {
            RegisterPayment registerPayment = new RegisterPayment();
            registerPayment.ShowDialog();
            LoadDataFromDB();
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
            LoadDataFromDB();
        }

        private void ButtonArchivedPayment_Click(object sender, RoutedEventArgs e)
        {
            ArchivedPayments archivedPayments = new ArchivedPayments();
            archivedPayments.ShowDialog();
            LoadDataFromDB();
        }

        private void ButtonSettleEmployee_Click(object sender, RoutedEventArgs e)
        {
            SelectEmployee selectEmployee = new SelectEmployee(this);
            selectEmployee.ShowDialog();
            LoadDataFromDB();
        }
    }
}
