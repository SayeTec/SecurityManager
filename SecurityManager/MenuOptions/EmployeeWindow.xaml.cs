using SecurityManager_GUI.MenuOptions.EmployeeOptions;
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
    /// Interaction logic for EmoloyeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }

        private void ButtonAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.Show();
        }
    }
}
