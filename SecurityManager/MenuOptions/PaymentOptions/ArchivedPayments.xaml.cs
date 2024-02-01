using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
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

            LoadDataForPayments();
        }

        public ArchivedPayments(SalariesWindow salaries)
        {
            InitializeComponent();

            previousWindow = salaries;

            LoadDataForPayments();
        }

        private void LoadDataForPayments()
        {
            DataGridPayments.ItemsSource = PaymentRepository.GetInactivePayments();
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
    }
}
