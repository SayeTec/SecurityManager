using System.Windows;

namespace SecurityManager_GUI.MenuOptions
{
    /// <summary>
    /// Interaction logic for DutyScheduleWindow.xaml
    /// </summary>
    public partial class DutyScheduleWindow : Window
    {
        public DutyScheduleWindow()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }
    }
}
