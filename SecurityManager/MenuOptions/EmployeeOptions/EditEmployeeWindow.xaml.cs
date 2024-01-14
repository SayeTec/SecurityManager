using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;
using System.Windows.Controls;

namespace SecurityManager_GUI.MenuOptions.EmployeeOptions
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        private Employee employeeToEdit;

        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            employeeToEdit = employee;

            InitializeComboBox();
            InsertEmployeeData();
        }

        private void InitializeComboBox()
        {
            ComboBoxEmployeeRole.ItemsSource = RoleRepository.GetRolesUnderOrEqualsEmployeePriority(employeeToEdit);
            DataGridDepartments.ItemsSource = DepartmentRepository.GetAllDepartments();
        }

        private void InsertEmployeeData()
        {
            TextBoxFirstName.Text = employeeToEdit.Name;
            TextBoxLastName.Text = employeeToEdit.Surname;
            TextBoxPhoneNumber.Text = employeeToEdit.Phone;
            TextBoxEmail.Text = employeeToEdit.Email;
            TextBoxLogin.Text = employeeToEdit.Login;

            ComboBoxEmployeeRole.SelectedItem = employeeToEdit.Role;
            DataGridDepartments.SelectedItem = employeeToEdit.Department;
            //TODO: Add possibility to change employee's department
        }

        private Employee GetEmployeeWithData()
        {
            return new Employee()
            {
                ID = employeeToEdit.ID,
                Name = TextBoxFirstName.Text,
                Surname = TextBoxLastName.Text,
                Phone = TextBoxPhoneNumber.Text,
                Email = TextBoxEmail.Text,
                Login = TextBoxLogin.Text,
                Role = ComboBoxEmployeeRole.SelectedItem as Role == null ? employeeToEdit.Role : ComboBoxEmployeeRole.SelectedItem as Role,
                Department = DataGridDepartments.SelectedItem as Department == null ? employeeToEdit.Department : DataGridDepartments.SelectedItem as Department,
                Password = employeeToEdit.Password,
                GrossRate = employeeToEdit.GrossRate
            };
        }

        private void FirstLetterToUpper(TextBox textBox)
        {
            if (textBox.Text.Length <= 0) return;
            string s = textBox.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = textBox.SelectionStart;
                int curSelLength = textBox.SelectionLength;
                textBox.SelectionStart = 0;
                textBox.SelectionLength = 1;
                textBox.SelectedText = s.ToUpper();
                textBox.SelectionStart = curSelStart;
                textBox.SelectionLength = curSelLength;
            }
        }

        private void TextBoxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstLetterToUpper(TextBoxFirstName);
        }

        private void TextBoxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstLetterToUpper(TextBoxLastName);
        }

        private void ButtonPasswordChange_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(employeeToEdit);
            changePassword.ShowDialog();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (employeeToEdit.Equals(GetEmployeeWithData()))
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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Session.Instance.CurrentEmployee.Role.Priority <= employeeToEdit.Role.Priority && !(ComboBoxEmployeeRole.SelectedItem as Role).Equals(employeeToEdit.Role))
            {
                MessageBox.Show(DisplayMessages.Error.ATTEMPT_TO_UPDATE_EMPLOYEE_WITH_LOWER_ROLE, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string login = TextBoxLogin.Text;

            string errorMessage = "";

            if (!ValuesValidation.ValidateLoginMatchesPattern(login, GetEmployeeWithData()))
                errorMessage += $"{string.Format(DisplayMessages.Error.LOGIN_NOT_MATCH_PATTERN, login)}\n";

            if (!ValuesValidation.ValidateLoginIsUnique(login, employeeToEdit))
                errorMessage += $"{DisplayMessages.Error.LOGIN_NOT_UNIQUE}\n";
            
            if (!ValuesValidation.ValidatePhoneNumber(TextBoxPhoneNumber.Text))
                errorMessage += $"{DisplayMessages.Error.PHONE_NUMBER_NOT_VALID}\n";

            if (!ValuesValidation.ValidateEmail(TextBoxEmail.Text))
                errorMessage += $"{DisplayMessages.Error.EMAIL_NOT_VALID}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EmployeeRepository.UpdateEmployee(GetEmployeeWithData());
            Close();
        }
    }
}
