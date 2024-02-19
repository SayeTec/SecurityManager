using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.RoleManagement
{
    /// <summary>
    /// Interaction logic for AddOrEditRoleWindow.xaml
    /// </summary>
    public partial class AddOrEditRoleWindow : Window
    {
        private Role roleToEdit;
        public AddOrEditRoleWindow()
        {
            InitializeComponent();
        }

        public AddOrEditRoleWindow(Role role)
        {
            InitializeComponent();
            roleToEdit = role;

            LoadDataFromRole();
        }

        private void LoadDataFromRole()
        {
            TextBoxCode.Text = roleToEdit.Code;
            TextBoxName.Text = roleToEdit.Name;
            TextBoxPriority.Text = roleToEdit.PriorityView;
        }

        private Role GetRoleWithData()
        {
            int convertingResult;

            return new Role()
            {
                ID = roleToEdit.ID,
                Code = TextBoxCode.Text,
                Name = TextBoxName.Text,
                Priority = int.TryParse(TextBoxPriority.Text, out convertingResult) ? convertingResult : 0
            };
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxCode.Text) || string.IsNullOrEmpty(TextBoxName.Text)
                || string.IsNullOrEmpty(TextBoxPriority.Text))
            {
                MessageBox.Show(DisplayMessages.Error.ROLE_REQUIRED_DATA_NOT_PROVIDED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";
            int convertingResult;

            Role role = new Role()
            {
                Code = TextBoxCode.Text,
                Name = TextBoxName.Text,
                Priority = int.TryParse(TextBoxPriority.Text, out convertingResult) ? convertingResult : 0
            };

            if (!ValuesValidation.ValidateRoleCode(TextBoxCode.Text))
                errorMessage += $"{DisplayMessages.Error.ROLE_CODE_NOT_VALID}\n";

            if (!ValuesValidation.ValidateRoleName(TextBoxName.Text))
                errorMessage += $"{DisplayMessages.Error.ROLE_NAME_NOT_VALID}\n";

            if (!ValuesValidation.ValidateRolePriority(TextBoxPriority.Text))
                errorMessage += $"{DisplayMessages.Error.ROLE_PRIORITY_NOT_VALID}\n";

            if (RoleRepository.CheckIfRoleWithPriorityExistsInDB(int.TryParse(TextBoxPriority.Text, out convertingResult) ? convertingResult : -1))
                errorMessage += $"{DisplayMessages.Error.ROLE_PRIORITY_IS_NOT_UNIQUE}\n";

            if (!RoleRepository.CheckIfRoleIsUnique(role))
                errorMessage += $"{DisplayMessages.Error.ROLE_IS_NOT_UNIQUE}\n";

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string action = "add-role";

            if (roleToEdit != null)
            {
                action = "update-role";
                role = GetRoleWithData();
            }

            PasswordConfirmation passwordConfirmation = new PasswordConfirmation(role, action, this);
            passwordConfirmation.ShowDialog();
            Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (roleToEdit == null)
            {
                Close();
                return;
            }

            if (roleToEdit.Equals(GetRoleWithData()))
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
