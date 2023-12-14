using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecurityManager_GUI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
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
                ButtonEmployeeManagement.MaxWidth = ActualWidth - 62;
                ButtonStatistics.MaxWidth = ActualWidth - 62;
                buttonScheduleDesigner.MaxWidth = ActualWidth - 62;
                ButtonDutiesManagement.MaxWidth = ActualWidth - 62;
                ButtonBack.MaxWidth = ActualWidth - 62;
            }
            else
            {
                ButtonEmployeeManagement.MaxWidth = 200;
                ButtonStatistics.MaxWidth = 200;
                buttonScheduleDesigner.MaxWidth = 200;
                ButtonDutiesManagement.MaxWidth = 200;
                ButtonBack.MaxWidth = 200;
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ButtonEmployeeManagement_Click(object sender, RoutedEventArgs e)
        {
            EmoloyeeWindow emoloyeeWindow = new EmoloyeeWindow();
            emoloyeeWindow.Show();
            Close();
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            MenuOptions.SalariesWindow salariesWindow = new MenuOptions.SalariesWindow();
            salariesWindow.Show();
            Close();
        }

        private void buttonScheduleDesigner_Click(object sender, RoutedEventArgs e)
        {
            MenuOptions.DutyScheduleWindow dutyScheduleWindow = new MenuOptions.DutyScheduleWindow();
            dutyScheduleWindow.Show();
            Close();
        }

        private void ButtonDutiesManagement_Click(object sender, RoutedEventArgs e)
        {
            MenuOptions.DutyWindow dutyWindow = new MenuOptions.DutyWindow();
            dutyWindow.Show();
            Close();
        }
    }
}
