using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text;

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
        private string FilePath = @"Data\employee.json";
        private List<Employee> Employees;
        public EmployeeWindow()
        {
            InitializeComponent();
            Employees = LoadEmployeesFromJson();
            DataGridEmployees.ItemsSource = Employees;

        }
        private void SaveEmployeesToJson()
        {
            string json = JsonConvert.SerializeObject(Employees);
            File.WriteAllText(FilePath, json, Encoding.UTF8);
        }
        private List<Employee> LoadEmployeesFromJson()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string json = File.ReadAllText(FilePath, Encoding.UTF8);
                    List<Employee> loadedEmployees = JsonConvert.DeserializeObject<List<Employee>>(json);

                    if (loadedEmployees != null)
                    {
                        // Aktualizuj listę pracowników i odśwież widok DataGrid
                        Employees = loadedEmployees;
                        DataGridEmployees.ItemsSource = Employees;

                        return loadedEmployees;
                    }
                    else
                    {
                        Console.WriteLine("Błąd deserializacji JSON. Lista pracowników jest nullem.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas deserializacji JSON: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Plik JSON nie istnieje.");
            }

            // Domyślne dane, jeśli coś poszło nie tak
            return new List<Employee>();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            SaveEmployeesToJson();
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
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
        public string Object { get; set; }
        public int WorkedHours { get; set; }
    }
}
