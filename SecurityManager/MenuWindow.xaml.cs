using SecurityManager;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions;
using System.Windows;

namespace SecurityManager_GUI
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public Employee loggedEmployee { get; }

        public MenuWindow()
        {
            InitializeComponent();
            SizeChanged += Window_SizeChanged;
            loggedEmployee = SessionManager.Instance.CurrentEmployee;

            EmployeeLabel.Content = loggedEmployee.ToString();
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
            SessionManager.Instance.ClearCurrentEmployee();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ButtonEmployeeManagement_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow emoloyeeWindow = new EmployeeWindow();
            emoloyeeWindow.Show();
            Close();
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            SalariesWindow salariesWindow = new SalariesWindow();
            salariesWindow.Show();
            Close();
        }

        private void buttonScheduleDesigner_Click(object sender, RoutedEventArgs e)
        {
            DutyScheduleWindow dutyScheduleWindow = new DutyScheduleWindow();
            dutyScheduleWindow.Show();
            Close();
        }

        private void ButtonDutiesManagement_Click(object sender, RoutedEventArgs e)
        {
            DutyWindow dutyWindow = new DutyWindow();
            dutyWindow.Show();
            Close();
        }
    }
}
