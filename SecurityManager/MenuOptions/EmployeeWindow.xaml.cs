using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SecurityManager_GUI
{
    /// <summary>
    /// Interaction logic for EmoloyeeWindow.xaml
    /// </summary>
    /// 

    //Employee list with options to add, edit and delete employees
    //For each employee can be two instances, one can work in different departments

    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();

            List<Employee1> employees = new List<Employee1>
            {
                new Employee1 { ID = 1, Name = "Jan", Surname = "Kowalski", Status = "Aktywny", Object = "Oddział A", WorkedHours = 160 },
                new Employee1 { ID = 2, Name = "Anna", Surname = "Nowak", Status = "Nieaktywny", Object = "Oddział B", WorkedHours = 140 },
                new Employee1 { ID = 3, Name = "Marek", Surname = "Wiśniewski", Status = "Aktywny", Object = "Oddział C", WorkedHours = 180 }
            };

            DataGridEmployees.ItemsSource = employees;
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

        private void ButtonEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.Show();
        }

        private void DataGridEmployees_Initialized(object sender, System.EventArgs e)
        {
            
        }
    }
    public class Employee1
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
        public string Object { get; set; }
        public int WorkedHours { get; set; }
    }
}
