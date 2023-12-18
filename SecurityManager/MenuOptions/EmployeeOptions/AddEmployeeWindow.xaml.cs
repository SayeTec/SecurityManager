using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Windows;
using System.Windows.Controls;
//using static SecurityManager_Fun.Model.Role;

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

            TextBoxFirstName.GotFocus += TextBox_GotFocus;
            TextBoxFirstName.LostFocus += TextBox_LostFocus;

            TextBoxLastName.GotFocus += TextBox_GotFocus;
            TextBoxLastName.LostFocus += TextBox_LostFocus;

            TextBoxPhoneNumber.GotFocus += TextBox_GotFocus;
            TextBoxPhoneNumber.LostFocus += TextBox_LostFocus;

            ComboboxEmployeeRole.GotFocus += TextBox_GotFocus;
            ComboboxEmployeeRole.LostFocus += TextBox_LostFocus;

            SetPlaceholderText();
        }

        private void InitializeComboBox()
        {
            //ComboboxEmployeeRole.ItemsSource = RoleRepository.GetRolesUnderPriority(PriorityType.Admin);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxEmployeeRole.SelectedItem == null || TextBoxFirstName.Text == null || TextBoxLastName.Text == null || TextBoxPhoneNumber == null)
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
            }
            else
            {
                //TODO: Add employee to database.



                Close();
            }

            

            //AccountService.RegisterNewEmployee(TextBoxFirstName.Text,
            //    TextBoxLastName.Text, 
            //    TextBoxPhoneNumber.Text,
            //    (ComboboxEmployeeRole.SelectedItem as Role).ID);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SetPlaceholderText()
        {
            TextBoxFirstName.Text = "Imię";
            TextBoxLastName.Text = "Nazwisko";
            TextBoxPhoneNumber.Text = "Numer telefonu";
            ComboboxEmployeeRole.Text = "Rola";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag as string)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;            
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag as string;
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        
    }
}
