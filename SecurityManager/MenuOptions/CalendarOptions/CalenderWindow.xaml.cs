using SecurityManager_Fun.Model;
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

namespace SecurityManager_GUI.MenuOptions.CalendarOptions
{
    /// <summary>
    /// Interaction logic for CalenderWindow.xaml
    /// </summary>
    public partial class CalenderWindow : Window
    {
        Employee selectedEmployee;
        int selectedMonth;
        public CalenderWindow(Employee employee)
        {
            InitializeComponent();

            selectedEmployee = employee;
            selectedMonth = DateTime.Now.Date.Month;
            TextBoxSelectedMonth.Text += DateTime.Now.Date.Month.ToString();
            LabelSelectedEmployeeValue.Content = selectedEmployee.FullName;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
