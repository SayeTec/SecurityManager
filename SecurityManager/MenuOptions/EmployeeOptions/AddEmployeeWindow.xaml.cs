using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;
using static SecurityManager_Fun.Model.Role;

namespace SecurityManager_GUI.MenuOptions.EmployeeOptions
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();

            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            ComboboxEmployeeRole.ItemsSource = RoleRepository.GetRolesUnderPriority(PriorityType.Admin);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxEmployeeRole.SelectedItem == null) return;

            AccountService.RegisterNewEmployee(TextBoxFirstName.Text,
                TextBoxLastName.Text, 
                TextBoxPhoneNumber.Text,
                (ComboboxEmployeeRole.SelectedItem as Role).ID);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
