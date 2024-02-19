using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using SecurityManager_Fun.Data;
using SecurityManager_Fun.Logic.Filters;
using System;
using SecurityManager_Fun.Logic;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.CountryAndDepartmentManagement
{
    /// <summary>
    /// Interaction logic for CountryWindow.xaml
    /// </summary>
    public partial class CountryWindow : Window
    {
        //TODO: Improve error messages 

        public CountryWindow()
        {
            InitializeComponent();

            ButtonEditCountry.Visibility = Visibility.Collapsed;
            ButtonDeleteCountry.Visibility = Visibility.Collapsed;

            ButtonEditDepartment.Visibility = Visibility.Collapsed;
            ButtonDeleteDepartment.Visibility = Visibility.Collapsed;

            LoadDataFromDB();
        }

        private void LoadDataFromDB()
        {
            ClearCountryFilter();
            ClearDepartmentFilter();
            LoadDataFromDBCountries();
            LoadDataFromDBDepartments();
            LoadFilterComboBoxesFromDB();
        }

        private void LoadDataFromDBCountries()
        {
            DataGridCountries.ItemsSource = CountryRepository.GetAllCountries();
        }

        private void LoadDataFromDBDepartments()
        {
            DataGridDepartments.ItemsSource = DepartmentRepository.GetAllDepartments();
        }

        private void LoadFilterComboBoxesFromDB()
        {
            ComboBoxCountrySelection.ItemsSource = CountryRepository.GetAllCountries();
        }

        private void ClearCountryFilter()
        {
            TextBoxCountrySymbolPattern.Text = string.Empty;
            TextBoxCountryNamePattern.Text = string.Empty;
        }

        private void ClearDepartmentFilter()
        {
            TextBoxDepartmentAddressPattern.Text = string.Empty;
            TextBoxDepartmentCapacityPattern.Text = string.Empty;
            ComboBoxCountrySelection.SelectedItem = null;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }

        private void ButtonAddCountry_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditCountryWindow addOrEditCountry = new AddOrEditCountryWindow();
            addOrEditCountry.ShowDialog();
            LoadDataFromDB();
        }

        private void ButtonEditCountry_Click(object sender, RoutedEventArgs e)
        {
            Country? country = DataGridCountries.SelectedItem as Country;
            
            if (country == null)
            {
                MessageBox.Show(DisplayMessages.Error.COUNTRY_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddOrEditCountryWindow addOrEditCountry = new AddOrEditCountryWindow(country);
            addOrEditCountry.ShowDialog();
            DataGridCountries.SelectedItem = null;
            LoadDataFromDB();
        }

        private void ButtonAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditDepartmentWindow addOrEditDepartment = new AddOrEditDepartmentWindow();
            addOrEditDepartment.ShowDialog();
            LoadDataFromDB();
        }

        private void ButtonEditDepartment_Click(object sender, RoutedEventArgs e)
        {
            Department? department = DataGridDepartments.SelectedItem as Department;

            if (department == null)
            {
                MessageBox.Show(DisplayMessages.Error.DEPARTMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddOrEditDepartmentWindow addOrEditDepartment = new AddOrEditDepartmentWindow(department);
            addOrEditDepartment.ShowDialog();
            DataGridDepartments.SelectedItem = null;
            LoadDataFromDB();
        }

        private void DataGridCountries_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                if (DataGridCountries.SelectedItem == clickedRow.DataContext)
                {
                    DataGridCountries.SelectedItem = null;
                    e.Handled = true;
                }
            }
        }

        private void DataGridDepartments_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
                if (DataGridDepartments.SelectedItem == clickedRow.DataContext)
                {
                    DataGridDepartments.SelectedItem = null;
                    e.Handled = true;
                }
            }
        }

        private void ButtonDeleteCountry_Click(object sender, RoutedEventArgs e)
        {            
            Country? countryToDelete = DataGridCountries.SelectedItem as Country;

            if (countryToDelete == null)
            {
                MessageBox.Show(DisplayMessages.Error.COUNTRY_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            if (CountryRepository.CheckIfCountryHasDepartments(countryToDelete))
                errorMessage += $"{DisplayMessages.Error.COUNTRY_HAS_DEPARTMENTS}\n";

            if (CountryRepository.CheckIfCountryHasDeductions(countryToDelete))
                errorMessage += $"{DisplayMessages.Error.COUNTRY_HAS_DEDUCTIONS}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"{DisplayMessages.Error.OPERATION_CANNOT_BE_APPLIED}\n" + errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.COUNTRY_DELETE_CONFIRMATION, countryToDelete.Name), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PasswordConfirmation passwordConfirmation = new PasswordConfirmation(countryToDelete, "delete-country", null);
                passwordConfirmation.ShowDialog();
                DataGridCountries.SelectedItem = null;
                LoadDataFromDB();
            }
        }

        private void ButtonDeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            Department? departmentToDelete = DataGridDepartments.SelectedItem as Department;

            if (departmentToDelete == null)
            {
                MessageBox.Show(DisplayMessages.Error.DEPARTMENT_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            if (DepartmentRepository.CheckIfDepartmentHasEmployees(departmentToDelete))
                errorMessage += $"{DisplayMessages.Error.DEPARTMENT_HAS_EMPLOYEES}\n";


            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"{DisplayMessages.Error.OPERATION_CANNOT_BE_APPLIED}\n" + errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.DEPARTMENT_DELETE_CONFIRMATION, departmentToDelete.Address), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PasswordConfirmation passwordConfirmation = new PasswordConfirmation(departmentToDelete, "delete-department", null);
                passwordConfirmation.ShowDialog();
                DataGridDepartments.SelectedItem = null;
                LoadDataFromDB();
            }
        }

        private void DataGridCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridDepartments.SelectedItem = null;

            if (DataGridCountries.SelectedItem != null)
            {
                ButtonEditCountry.Visibility = Visibility.Visible;
                ButtonDeleteCountry.Visibility = Visibility.Visible;
                return;
            }

            ButtonEditCountry.Visibility = Visibility.Collapsed;
            ButtonDeleteCountry.Visibility = Visibility.Collapsed;
        }

        private void DataGridDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridCountries.SelectedItem = null;

            if (DataGridDepartments.SelectedItem != null)
            {
                ButtonEditDepartment.Visibility = Visibility.Visible;
                ButtonDeleteDepartment.Visibility = Visibility.Visible;
                return;
            }
            
            ButtonEditDepartment.Visibility = Visibility.Collapsed;
            ButtonDeleteDepartment.Visibility = Visibility.Collapsed;
        }

        private void ButtonFilterCountry_Click(object sender, RoutedEventArgs e)
        {
            DataGridCountries.ItemsSource = CountryFilter.FilterCountries(
                TextBoxCountrySymbolPattern.Text, TextBoxCountryNamePattern.Text);
            DataGridCountries.Items.Refresh();
        }

        private void ButtonClearCountryFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearCountryFilter();
            LoadDataFromDBCountries();
        }

        private void ButtonFilterDepartment_Click(object sender, RoutedEventArgs e)
        {
            int convertingResult;
            DataGridDepartments.ItemsSource = DepartmentFilter.FilterDepartments(
                TextBoxDepartmentAddressPattern.Text, int.TryParse(TextBoxDepartmentCapacityPattern.Text, out convertingResult) ? convertingResult : 0,
                ComboBoxCountrySelection.SelectedItem as Country);
            DataGridDepartments.Items.Refresh();
        }

        private void ButtonClearDepartmentFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearDepartmentFilter();
            LoadDataFromDBDepartments();
        }
    }
}
