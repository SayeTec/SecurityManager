﻿using SecurityManager_Fun.Logic;
using SecurityManager_Fun.Model;
using SecurityManager_GUI;
using System.Windows;

namespace SecurityManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SizeChanged += Window_SizeChanged;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            if (ActualWidth < 400)
            {
                TextBoxLogin.MaxWidth = ActualWidth - 20;
                TextBoxPassword.MaxWidth = ActualWidth - 20;
            }
            else
            {
                TextBoxLogin.MaxWidth = 200;
                TextBoxPassword.MaxWidth = 200;
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Unlock this code when DB will be ready
            //Employee employee = AccountService.LogIn(TextBoxLogin.Text, TextBoxPassword.Password);

            //if (employee == null) { CleanTextBoxes(); return; }

            MenuWindow menuWindow = new MenuWindow(/*employee*/);
            menuWindow.Show();
            Close();
        }

        private void CleanTextBoxes()
        {
            TextBoxLogin.Text = string.Empty;
            TextBoxPassword.Password = string.Empty;
        }
    }
}
