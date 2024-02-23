using Microsoft.VisualBasic;
using SecurityManager_Fun.Data;
using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions.CalendarOptions
{
    /// <summary>
    /// Interaction logic for CalenderCreationWindow.xaml
    /// </summary>
    public partial class CalenderCreationWindow : Window
    {        
        DateTime selectedDate;
        bool editMode = false;
        WorkSchedule selectedSchedule = null;
        Employee selectedEmployee;

        public CalenderCreationWindow()
        {
            InitializeComponent();
            selectedDate = DateTime.Now;

            LoadDataFromDB();
            LoadWorkschedulesForSelectedDate();
            LoadWorkDaysInSelectedMonth();
        }

        private void LoadDataFromDB()
        {
            List<Country> countries = CountryRepository.GetAllCountries();
            countries.Insert(0, new Country());
            ComboBoxCountrySelection.ItemsSource = countries;

            LoadDefaultDataForDepartment();
            LoadDefaultDataForEmployees();
            LoadDataForSelectionStartTimeComboBox();
        }

        private void LoadDefaultDataForDepartment()
        {
            List<Department> departments = DepartmentRepository.GetAllDepartments();
            departments.Insert(0, new Department());
            ComboBoxDepartmentSelection.ItemsSource = departments;
        }

        private void LoadDefaultDataForEmployees()
        {
            List<Employee> employees = new List<Employee>() { new Employee() };
            ComboBoxEmployeeSelection.ItemsSource = employees;
        }

        private void LoadWorkschedulesForSelectedDate()
        {
            Department selectedDepartment = ComboBoxDepartmentSelection.SelectedItem as Department;
            if (selectedDepartment == null || selectedDepartment.Equals(new Department()))
            {
                ListBoxDateWorkschedules.ItemsSource = null;
                return;
            }

            ListBoxDateWorkschedules.ItemsSource = WorkScheduleRepository.GetWorkSchedulesForDateByDepartment(selectedDate, selectedDepartment);
        }

        private void LoadWorkDaysInSelectedMonth()
        {
            Department selectedDepartment = ComboBoxDepartmentSelection.SelectedItem as Department;
            if (selectedDepartment == null || selectedDepartment.Equals(new Department()))
            {
                ListBoxWorkDates.ItemsSource = null;
                return;
            }

            ListBoxWorkDates.ItemsSource = WorkScheduleRepository.GetWorkDatesForMonthByDepartment(selectedDate, selectedDepartment)                                       
                                            .Select(date => $"{date.ToString("dd.MM.yyyy")} " +
                                            $"Employees: {WorkScheduleRepository.GetSumEmployeesWithWorkForDepartment(date, selectedDepartment)} " +
                                            $"Hours: {WorkScheduleRepository.GetSumHoursInWorkWithWorkForDepartment(date, selectedDepartment)}");
        }
        
        private void LoadDataForSelectedWorkSchedule(Employee employee)
        {
            if (WorkScheduleRepository.CheckIfWorkScheduleExistForEmployeeByDate(selectedDate, employee))
            {
                editMode = true;
                selectedSchedule = WorkScheduleRepository.GetWorkScheduleForEmployeeByDate(selectedDate, employee);

                var selectedItem = ComboBoxStartTimeofDay.Items.Cast<dynamic>()
                    .FirstOrDefault(item => item.Hour.TimeOfDay == selectedSchedule.StartTime);

                if (selectedItem != null)
                {
                    ComboBoxStartTimeofDay.SelectedItem = selectedItem;
                }

                TextAmountOfWorkHours.Text = selectedSchedule.WorkHours.ToString();
                PanelWorkScheduleDeletion.Visibility = Visibility.Visible;
            }
            else
            {
                editMode = false;
                selectedSchedule = null;

                PanelWorkScheduleDeletion.Visibility = Visibility.Collapsed;
                ComboBoxStartTimeofDay.SelectedItem = null;
                TextAmountOfWorkHours.Text = string.Empty;
            }
        }

        private void LoadDataForSelectionStartTimeComboBox()
        {
            List<DateTime> hours = new List<DateTime>();

            for (int hour = 0; hour < 24; hour++)
            {
                DateTime time = new DateTime(1, 1, 1, hour, 0, 0);
                hours.Add(time);
            }

            List<object> displayList = new List<object>();
            foreach (var hour in hours)
            {
                displayList.Add(new { Hour = hour, HourString = hour.ToString("HH:mm") });
            }

            ComboBoxStartTimeofDay.ItemsSource = displayList;
        }

        private WorkSchedule GetWorkScheduleWithData()
        {
            int convertingResult;

            return new WorkSchedule()
            {
                ID = selectedSchedule.ID,
                Day = selectedSchedule.Day,
                StartTime = ((dynamic)ComboBoxStartTimeofDay.SelectedItem).Hour.TimeOfDay,
                WorkHours = int.TryParse(TextAmountOfWorkHours.Text, out convertingResult) ? convertingResult : 0,
                EmployeeID = selectedSchedule.EmployeeID
            };
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();
            Close();
        }

        private void ComboBoxCountrySelection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxDepartmentSelection.ItemsSource = null;

            Country selectedCountry = ComboBoxCountrySelection.SelectedItem as Country;

            GridWorkscheduleSection.Visibility = Visibility.Collapsed;
            ComboBoxStartTimeofDay.SelectedItem = null;
            TextAmountOfWorkHours.Text = string.Empty;
            ListBoxWorkDates.MinHeight = 250;
            ButtonSave.Visibility = Visibility.Collapsed;

            if (selectedCountry != null && !selectedCountry.Equals(new Country()))
            {
                ComboBoxDepartmentSelection.ItemsSource = DepartmentRepository.GetDepartmentsByCountry(selectedCountry);
                return;
            }

            LoadDefaultDataForDepartment();
            LoadWorkschedulesForSelectedDate();
            LoadWorkDaysInSelectedMonth();
        }

        private void ComboBoxDepartmentSelection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxEmployeeSelection.ItemsSource = null;

            Department selectedDepartment = ComboBoxDepartmentSelection.SelectedItem as Department;

            GridWorkscheduleSection.Visibility = Visibility.Collapsed;
            ComboBoxStartTimeofDay.SelectedItem = null;
            TextAmountOfWorkHours.Text = string.Empty;
            ListBoxWorkDates.MinHeight = 250;
            ButtonSave.Visibility = Visibility.Collapsed;

            if (selectedDepartment != null && !selectedDepartment.Equals(new Department()))
            {
                ComboBoxEmployeeSelection.ItemsSource = EmployeeRepository.GetEmployeesByDepartment(selectedDepartment);
                LoadWorkschedulesForSelectedDate();
                LoadWorkDaysInSelectedMonth();
                return;
            }

            LoadDefaultDataForEmployees();
            LoadWorkschedulesForSelectedDate();
            LoadWorkDaysInSelectedMonth();
        }

        private void ComboBoxEmployeeSelection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = ComboBoxEmployeeSelection.SelectedItem as Employee;

            if (selectedEmployee != null && !selectedEmployee.Equals(new Employee()))
            {
                LoadDataForSelectedWorkSchedule(selectedEmployee);

                this.selectedEmployee = selectedEmployee;

                GridWorkscheduleSection.Visibility = Visibility.Visible;
                ListBoxWorkDates.MinHeight = 150;
                ButtonSave.Visibility = Visibility.Visible;
                return;
            }

            this.selectedEmployee = null;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextAmountOfWorkHours.Text) || ComboBoxStartTimeofDay.SelectedItem == null
                || ComboBoxEmployeeSelection.SelectedItem == null || CalendarDateSelection.SelectedDate == null) 
            {
                MessageBox.Show(DisplayMessages.Error.WORK_SCHEDULE_DATA_NOT_PROVIDED, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string errorMessage = "";
            int convertingResult;

            WorkSchedule workSchedule = new WorkSchedule()
            {
                Day = (DateTime)CalendarDateSelection.SelectedDate,
                StartTime = ((dynamic)ComboBoxStartTimeofDay.SelectedItem).Hour.TimeOfDay,
                WorkHours = int.TryParse(TextAmountOfWorkHours.Text, out convertingResult) ? convertingResult : 0,
                EmployeeID = (ComboBoxEmployeeSelection.SelectedItem as Employee).ID
            };

            if (ValuesValidation.ValidateWorkHours(TextAmountOfWorkHours.Text))
                errorMessage += $"{DisplayMessages.Error.WORK_HOURS_NOT_VALID}\n";

            if (!editMode && WorkScheduleRepository.CheckIfWorkScheduleExistForEmployeeByDate(selectedDate, selectedEmployee))
            {
                MessageBox.Show(errorMessage, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.WORK_SCHEDULE_EDIT_CONFIRMATION, workSchedule.Day.Date.ToString("dd.MM.yyyy")), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (editMode)
                {
                    workSchedule = GetWorkScheduleWithData();
                    WorkScheduleRepository.UpdateWorkSchedule(workSchedule);
                } 
                else
                {
                    WorkScheduleRepository.AddNewWorkSchedule(workSchedule);
                }
                
                LoadWorkschedulesForSelectedDate();
                LoadWorkDaysInSelectedMonth();
            }
        }

        private void CalendarDateSelection_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedDate = (DateTime)CalendarDateSelection.SelectedDate;
            LoadWorkschedulesForSelectedDate();
            LoadWorkDaysInSelectedMonth();

            if (selectedEmployee != null) 
            {
                LoadDataForSelectedWorkSchedule(selectedEmployee);
            }            
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null && PaymentRepository.CheckIfActivePaymentsForEmployeeExist(selectedEmployee))
            {
                MessageBox.Show(DisplayMessages.Error.WORK_SCHEDULE_HAS_ACTIVE_PAYMENTS, "Błąd Walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show(string.Format(DisplayMessages.Confirmation.WORK_SCHEDULE_DELETE_CONFIRMATION, selectedSchedule.Day.Date.ToString("dd.MM.yyyy")), "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                WorkScheduleRepository.DeleteWorkSchedule(selectedSchedule);

                LoadWorkschedulesForSelectedDate();
                LoadWorkDaysInSelectedMonth();

                PanelWorkScheduleDeletion.Visibility = Visibility.Collapsed;
                ComboBoxStartTimeofDay.SelectedItem = null;
                TextAmountOfWorkHours.Text = string.Empty;
            }
        }
    }
}
