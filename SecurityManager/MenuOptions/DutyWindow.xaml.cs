using System.Windows;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for DutyWindow.xaml
    /// </summary>
    public partial class DutyWindow : Window
    {
        public DutyWindow()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
        }
    }
}
