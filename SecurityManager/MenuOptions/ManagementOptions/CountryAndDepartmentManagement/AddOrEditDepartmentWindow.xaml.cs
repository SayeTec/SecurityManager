using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.CountryAndDepartmentManagement
{
    /// <summary>
    /// Interaction logic for AddOrEditDepartmentWindow.xaml
    /// </summary>
    public partial class AddOrEditDepartmentWindow : Window
    {
        //TODO: Improve error messages 

        private Department departmentToEdit;
        public AddOrEditDepartmentWindow()
        {
            InitializeComponent();
            LoadDataFromDB();
        }

        public AddOrEditDepartmentWindow(Department department)
        {
            InitializeComponent();
            departmentToEdit = department;

            LoadDataFromDepartment();
        }

        private void LoadDataFromDepartment()
        {
            TextBoxAddress.Text = departmentToEdit.Address;
            TextBoxCapacity.Text = departmentToEdit.Capacity.ToString();
            LoadDataFromDB();
            ComboboxCountry.SelectedItem = departmentToEdit.Country;
        }

        private void LoadDataFromDB() 
        {
            ComboboxCountry.ItemsSource = CountryRepository.GetAllCountries();
        }

        private Department GetDepartmentWithData()
        {
            int convertingResult;

            return new Department()
            {
                ID = departmentToEdit.ID,
                Address = TextBoxAddress.Text,
                Capacity = int.TryParse(TextBoxCapacity.Text, out convertingResult) ? convertingResult : 0,
                CountryID = (int)(ComboboxCountry.SelectedItem as Country)?.ID
            };
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxCountry.SelectedItem == null || string.IsNullOrEmpty(TextBoxAddress.Text)
                || string.IsNullOrEmpty(TextBoxCapacity.Text))
            {
                MessageBox.Show(DisplayMessages.Error.DEPARTMENT_REQUIRED_DATA_NOT_PROVIDED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";
            int convertingResult;

            Department department = new Department()
            {
                Address = TextBoxAddress.Text,
                Capacity = int.TryParse(TextBoxCapacity.Text, out convertingResult) ? convertingResult : 0,
                CountryID = (int)(ComboboxCountry.SelectedItem as Country)?.ID
            };

            if (!ValuesValidation.ValidateDepartmentAddress(TextBoxAddress.Text))
                errorMessage += $"{DisplayMessages.Error.DEPARTMENT_ADDRESS_NOT_VALID}\n";

            if (!ValuesValidation.ValidateDepartmentCapacity(TextBoxCapacity.Text))
                errorMessage += $"{DisplayMessages.Error.DEPARTMENT_CAPACITY_NOT_VALID}\n";

            if (!DepartmentRepository.CheckIfDepartmentIsUnique(department))
                errorMessage += $"{DisplayMessages.Error.DEPARTMENT_IS_NOT_UNIQUE}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string action = "add-department";

            if (departmentToEdit != null)
            {
                action = "update-department";
                department = GetDepartmentWithData();
            }

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(department, action, this);
            passwordConfirmation.ShowDialog();
            Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (departmentToEdit == null)
            {
                Close();
                return;
            }

            if (departmentToEdit.Equals(GetDepartmentWithData()))
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
    }
}
