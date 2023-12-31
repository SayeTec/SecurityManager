﻿using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static SecurityManager_Fun.Model.Role;

namespace SecurityManager_GUI.MenuOptions.EmployeeOptions
{
    /// <summary>
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private EmployeeWindow previous;
        public AddEmployeeWindow(EmployeeWindow employeeWindow)
        {
            InitializeComponent();

            InitializeComboBox();
            TextBoxFirstName.GotFocus += TextBox_GotFocus;
            TextBoxFirstName.LostFocus += TextBox_LostFocus;

            TextBoxLastName.GotFocus += TextBox_GotFocus;
            TextBoxLastName.LostFocus += TextBox_LostFocus;

            TextBoxPhoneNumber.GotFocus += TextBox_GotFocus;
            TextBoxPhoneNumber.LostFocus += TextBox_LostFocus;

            ComboboxEmployeeRole.GotFocus += TextBox_GotFocus;
            ComboboxEmployeeRole.LostFocus += TextBox_LostFocus;

            SetPlaceholderText();

            previous = employeeWindow;
        }

        private void InitializeComboBox()
        {
            ComboboxEmployeeRole.ItemsSource = RoleRepository.GetRolesUnderEmployeePriority(Session.Instance.CurrentEmployee);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxEmployeeRole.SelectedItem == null || TextBoxFirstName.Text == null || TextBoxLastName.Text == null || TextBoxPhoneNumber == null)
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
            }
            else
            {
                //TODO: Add employee to database.
                //_instance.SaveEmployeeToJson(TextBoxFirstName.Text, TextBoxLastName.Text, int.Parse(TextBoxPhoneNumber.Text));

                AccountService.RegisterNewEmployee(TextBoxFirstName.Text,
                    TextBoxLastName.Text,
                    TextBoxPhoneNumber.Text,
                    (ComboboxEmployeeRole.SelectedItem as Role).ID);

                previous.LoadEmployeesFromDB();
                Close();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SetPlaceholderText()
        {
            TextBoxFirstName.Text = "Imię";
            TextBoxLastName.Text = "Nazwisko";
            TextBoxPhoneNumber.Text = "Numer telefonu";
            ComboboxEmployeeRole.Text = "Rola";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag as string)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag as string;
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
        private void FirstLetterToUpper(TextBox textBox)
        {
            if (textBox.Text.Length <= 0) return;
            string s = textBox.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = textBox.SelectionStart;
                int curSelLength = textBox.SelectionLength;
                textBox.SelectionStart = 0;
                textBox.SelectionLength = 1;
                textBox.SelectedText = s.ToUpper();
                textBox.SelectionStart = curSelStart;
                textBox.SelectionLength = curSelLength;
            }
        }
        private void TextBoxFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstLetterToUpper(TextBoxFirstName);
        }

        private void TextBoxLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstLetterToUpper(TextBoxLastName);
        }

        private bool isUpdating = false;

        private void TextBoxPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdating)
            {
                return;
            }

            TextBox textBox = sender as TextBox;

            // Usuń wszystkie spacje z aktualnego tekstu
            string textWithoutSpaces = textBox.Text.Replace(" ", "");

            // Sprawdź, czy tekst zawiera tylko cyfry
            if (!IsNumeric(textWithoutSpaces))
            {
                // Jeśli tekst nie zawiera tylko cyfr, cofnij ostatnią zmianę
                TextChange textChange = e.Changes.ElementAt(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;

                // Wyłącz tymczasowo zdarzenie TextChanged
                isUpdating = true;
                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
                isUpdating = false;

                return;
            }

            // Wyczyszczenie obecnych spacji
            textBox.Text = textWithoutSpaces;

            // Dodanie spacji co trzy cyfry, jeśli tekst jest dłuższy niż 3 cyfry
            if (textWithoutSpaces.Length > 3)
            {
                int groupsOfThree = (textWithoutSpaces.Length - 1) / 3; // Oblicz, ile grup trzech cyfr jest potrzebnych
                int additionalSpaces = groupsOfThree > 1 ? groupsOfThree - 1 : 0; // Oblicz, ile dodatkowych spacji będzie potrzebnych

                StringBuilder formattedText = new StringBuilder();
                for (int i = 0; i < textWithoutSpaces.Length; i++)
                {
                    if (i > 0 && i % 3 == 0)
                    {
                        if (additionalSpaces > 0)
                        {
                            formattedText.Append(" ");
                            additionalSpaces--;
                        }
                    }
                    formattedText.Append(textWithoutSpaces[i]);
                }

                // Wyłącz tymczasowo zdarzenie TextChanged
                isUpdating = true;
                textBox.Text = formattedText.ToString();
                isUpdating = false;

                // Ustawienie kursora na końcu tekstu
                textBox.SelectionStart = textBox.Text.Length;
            }
        }
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }
    }
}
