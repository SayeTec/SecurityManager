using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic.Filters;
using SecurityManager_Fun.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using static SecurityManager_Fun.Model.Deduction;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.DeductionManagement
{
    /// <summary>
    /// Interaction logic for DeductionsWindow.xaml
    /// </summary>
    public partial class DeductionsWindow : Window
    {
        public DeductionsWindow()
        {
            InitializeComponent();

            ButtonEdit.Visibility = Visibility.Collapsed;
            ButtonDelete.Visibility = Visibility.Collapsed;

            LoadDataFromDB();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }

        private void LoadDataFromDB()
        {
            ClearFilter();
            LoadDataFromDBDeductions();
            LoadFilterComboBoxesFromDB();
        }

        private void LoadDataFromDBDeductions()
        {
            DataGridDeductions.ItemsSource = DeductionRepository.GetAllDeductions();
        }

        private void LoadFilterComboBoxesFromDB()
        {
            ComboBoxCountrySelection.ItemsSource = CountryRepository.GetAllCountries();
            ComboBoxTypeSelection.ItemsSource = new List<DeductionType>() { DeductionType.Tax, DeductionType.Bonus };
        }

        private void ClearFilter()
        {
            TextBoxNamePattern.Text = string.Empty;
            TextBoxValuePattern.Text = string.Empty;
            ComboBoxCountrySelection.SelectedItem = null;
            ComboBoxTypeSelection.SelectedItem = null;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditDeductionWindow addOrEditDeduction = new AddOrEditDeductionWindow();
            addOrEditDeduction.ShowDialog();
            LoadDataFromDB();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Deduction? deduction = DataGridDeductions.SelectedItem as Deduction;

            if (deduction == null)
            {
                MessageBox.Show(DisplayMessages.Error.DEDUCTION_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddOrEditDeductionWindow addOrEditDeduction = new AddOrEditDeductionWindow(deduction);
            addOrEditDeduction.ShowDialog();
            LoadDataFromDB();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Deduction? deductionToDelete = DataGridDeductions.SelectedItem as Deduction;

            if (deductionToDelete == null)
            {
                MessageBox.Show(DisplayMessages.Error.DEDUCTION_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            if (DeductionRepository.CheckIfDeductionHasAttachedToPayments(deductionToDelete))
                errorMessage += $"{DisplayMessages.Error.DEDUCTION_ATTACHED_TO_PAYMENT}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"{DisplayMessages.Error.OPERATION_CANNOT_BE_APPLIED}\n" + errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.DEDUCTION_DELETE_CONFIRMATION, deductionToDelete.Name), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PasswordConfirmation passwordConfirmation = new PasswordConfirmation(deductionToDelete, "delete-deduction", null);
                passwordConfirmation.ShowDialog();
                DataGridDeductions.SelectedItem = null;
                LoadDataFromDB();
            }
        }

        private void DataGridDeductions_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                if (DataGridDeductions.SelectedItem == clickedRow.DataContext)
                {
                    DataGridDeductions.SelectedItem = null;
                    e.Handled = true;
                }
            }
        }

        private void DataGridDeductions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridDeductions.SelectedItem != null)
            {
                ButtonEdit.Visibility = Visibility.Visible;
                ButtonDelete.Visibility = Visibility.Visible;
                return;
            }

            ButtonEdit.Visibility = Visibility.Collapsed;
            ButtonDelete.Visibility = Visibility.Collapsed;
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            decimal convertingResult;
            DataGridDeductions.ItemsSource = DeductionFilter.FilterDeductions(
                TextBoxNamePattern.Text, decimal.TryParse(TextBoxValuePattern.Text, out convertingResult) ? convertingResult : 0m,
                (DeductionType?)ComboBoxTypeSelection.SelectedItem, ComboBoxCountrySelection.SelectedItem as Country);
            DataGridDeductions.Items.Refresh();
        }

        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearFilter();
            LoadDataFromDBDeductions();
        }
    }
}
