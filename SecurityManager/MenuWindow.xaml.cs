using SecurityManager;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using SecurityManager_GUI.MenuOptions;
using SecurityManager_GUI.MenuOptions.ManagementOptions.CountryAndDepartmentManagement;
using SecurityManager_GUI.MenuOptions.ManagementOptions.DeductionManagement;
using SecurityManager_GUI.MenuOptions.ManagementOptions.RoleManagement;
using SecurityManager_GUI.MenuOptions.PaymentOptions;
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
            loggedEmployee = Session.Instance.CurrentEmployee;

            EmployeeLabel.Content = "Logged in User: " + loggedEmployee.FullName + " : " + loggedEmployee.Role.Name;

            if (Session.Instance.CurrentEmployee.Role.Priority != 0)
            {
                GridMainAdmin.Visibility = Visibility.Collapsed;
            }

            if (Session.Instance.CurrentEmployee.Role.Priority > 1)
            {
                ButtonEmployeeManagement.Visibility = Visibility.Collapsed;
            }
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
            Session.Instance.ClearCurrentEmployee();
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
            if (Session.Instance.CurrentEmployee.Role.Priority > 1)
            {
                ArchivedPayments archivedPayments = new ArchivedPayments(Session.Instance.CurrentEmployee);
                archivedPayments.Show();
            }
            else
            {
                SalariesWindow salariesWindow = new SalariesWindow();
                salariesWindow.Show();
            }
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

        private void ButtonRoleManagement_Click(object sender, RoutedEventArgs e)
        {
            RolesWindow rolesWindow = new RolesWindow();
            rolesWindow.Show();
            Close();
        }

        private void ButtonDeductionManagement_Click(object sender, RoutedEventArgs e)
        {   
            DeductionsWindow deductionsWindow = new DeductionsWindow();
            deductionsWindow.Show();
            Close();
        }

        private void ButtonCountryAndDepartmentManagement_Click(object sender, RoutedEventArgs e)
        {
            CountryWindow countryWindow = new CountryWindow();
            countryWindow.Show();
            Close();
        }
    }
}
