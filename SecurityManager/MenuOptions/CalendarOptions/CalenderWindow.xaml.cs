using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
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
        DateTime selectedDate;
        public CalenderWindow(Employee employee)
        {
            InitializeComponent();

            selectedEmployee = employee;
            selectedDate = DateTime.Now;
            LoadData();
        }

        private void LoadData()
        {
            TextBoxSelectedMonthValue.Text = string.Empty;
            TextBoxSelectedMonthValue.Text += $"{ApplicationConstants.MONTH_LIST[selectedDate.Month]} {selectedDate.Year}";
            LabelSelectedEmployeeValue.Content = selectedEmployee.FullName;

            ListBoxWorkscheduleDays.ItemsSource = WorkScheduleRepository.GetWorkSchedulesForEmployeeByMonth(selectedDate, selectedEmployee);

            LabelTotalWorkDaysInMonthValue.Content = WorkScheduleRepository.GetSumDaysInWorkForEmployeeByMonth(selectedDate, selectedEmployee).ToString();
            LabelTotalWorkHoursInMonthValue.Content = WorkScheduleRepository.GetSumHoursInWorkForEmployeeByMonth(selectedDate, selectedEmployee).ToString();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            selectedDate = selectedDate.AddMonths(-1);
            LoadData();
        }

        private void ButtonNextMonth_Click(object sender, RoutedEventArgs e)
        {
            selectedDate = selectedDate.AddMonths(1);
            LoadData();
        }
    }
}
