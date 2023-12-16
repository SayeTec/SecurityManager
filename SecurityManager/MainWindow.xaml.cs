using SecurityManager_Fun.Logic;
using SecurityManager_GUI;
using System.Windows;

namespace SecurityManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SizeChanged += Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (ActualWidth < 400)
            {
                TextBoxLogin.MaxWidth = ActualWidth - 20;
                TextBoxPassword.MaxWidth = ActualWidth - 20;
            }
            else
            {
                TextBoxLogin.MaxWidth = 200;
                TextBoxPassword.MaxWidth = 200;
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }
    }
}
