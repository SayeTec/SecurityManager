﻿using SecurityManager_GUI.MenuOptions.EmployeeOptions;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Text;
using System.Windows.Markup;

namespace SecurityManager_GUI
{
    /// <summary>
    /// Interaction logic for EmoloyeeWindow.xaml
    /// </summary>
    /// 

    //Employee list with options to add, edit and delete employees
    //First is loading all hours from all objects and if object is selected it shows only hours from this object
    //Data is loaded for month after 11th day of month (if it is March 7th it will show hours from February, [IMPORTANTE KOMANDORE BOMBARDIERO])
    //If date is selected it will show employees with their data only from this date

    public partial class EmployeeWindow : Window
    {
        private string FilePath = @"Data\employee.json";
        private List<Employee> Employees;
        public EmployeeWindow()
        {
            InitializeComponent();
            DataGridEmployees.Language = XmlLanguage.GetLanguage("pl-PL"); 
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
