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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
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
                ButtonEmployeeManagement.MaxWidth = ActualWidth - 62;
                ButtonStatistics.MaxWidth = ActualWidth - 62;
                buttonScheduleDesigner.MaxWidth = ActualWidth - 62;
                ButtonDutiesManagement.MaxWidth = ActualWidth - 62;
            }
            else
            {
                ButtonEmployeeManagement.MaxWidth = 200;
                ButtonStatistics.MaxWidth = 200;
                buttonScheduleDesigner.MaxWidth = 200;
                ButtonDutiesManagement.MaxWidth = 200;
            }
        }
    }
}
