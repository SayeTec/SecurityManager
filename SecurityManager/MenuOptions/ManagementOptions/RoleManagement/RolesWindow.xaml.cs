using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic.Filters;
using SecurityManager_Fun.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SecurityManager_GUI.MenuOptions.ManagementOptions.RoleManagement
{
    /// <summary>
    /// Interaction logic for RolesWindow.xaml
    /// </summary>
    public partial class RolesWindow : Window
    {
        //TODO: Improve error messages 

        public RolesWindow()
        {
            InitializeComponent();

            ButtonEdit.Visibility = Visibility.Collapsed;
            ButtonDelete.Visibility = Visibility.Collapsed;

            LoadDataFromDB();
        }

        private void LoadDataFromDB()
        {
            ClearFilter();
            LoadDataFromDBRoles();
        }

        private void LoadDataFromDBRoles()
        {
            DataGridRoles.ItemsSource = RoleRepository.GetAllRoles();
        }

        private void ClearFilter()
        {
            TextBoxRoleCodePattern.Text = string.Empty;
            TextBoxRoleNamePattern.Text = string.Empty;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditRoleWindow addOrEditRole = new AddOrEditRoleWindow();
            addOrEditRole.ShowDialog();
            LoadDataFromDB();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Role? role = DataGridRoles.SelectedItem as Role;

            if (role == null)
            {
                MessageBox.Show(DisplayMessages.Error.ROLE_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AddOrEditRoleWindow addOrEditRole = new AddOrEditRoleWindow(role);
            addOrEditRole.ShowDialog();
            DataGridRoles.SelectedItem = null;
            LoadDataFromDB();

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Role? roleToDelete = DataGridRoles.SelectedItem as Role;

            if (roleToDelete == null)
            {
                MessageBox.Show(DisplayMessages.Error.ROLE_FROM_LIST_MUST_BE_SELECTED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";

            if (RoleRepository.CheckIfRoleHasEmployees(roleToDelete))
                errorMessage += $"{DisplayMessages.Error.ROLE_HAS_EMPLOYEES}\n";


            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show($"{DisplayMessages.Error.OPERATION_CANNOT_BE_APPLIED}\n" + errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.ROLE_DELETE_CONFIRMATION, $"{roleToDelete.Code} {roleToDelete.Name}"), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                PasswordConfirmation passwordConfirmation = new PasswordConfirmation(roleToDelete, "delete-role", null);
                passwordConfirmation.ShowDialog();
                DataGridRoles.SelectedItem = null;
                LoadDataFromDB();
            }
        }

        private void DataGridRoles_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                if (DataGridRoles.SelectedItem == clickedRow.DataContext)
                {
                    DataGridRoles.SelectedItem = null;
                    e.Handled = true;
                }
            }
        }

        private void DataGridRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRoles.SelectedItem != null)
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
            DataGridRoles.ItemsSource = RoleFilter.FilterRoles(
                TextBoxRoleCodePattern.Text, TextBoxRoleNamePattern.Text);
            DataGridRoles.Items.Refresh();
        }

        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromDB();
        }
    }
}
