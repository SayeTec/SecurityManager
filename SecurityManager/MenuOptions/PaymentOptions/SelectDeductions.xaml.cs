using SecurityManager_Fun.Data.Repositories;
using SecurityManager_Fun.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SecurityManager_GUI.MenuOptions.PaymentOptions
{
    /// <summary>
    /// Interaction logic for SelectDeductions.xaml
    /// </summary>
    public partial class SelectDeductions : Window
    {
        private RegisterPayment registerPayment;
        private Deduction.DeductionType selectedDeductionType;

        List<Deduction> deductions;
        
        public SelectDeductions(RegisterPayment register, Deduction.DeductionType deductionType) 
        {
            InitializeComponent();
            registerPayment = register;
            selectedDeductionType = deductionType;

            switch (selectedDeductionType) {
                case Deduction.DeductionType.Tax:
                    TextBlockTitle.Text += " podatki";
                    break;

                case Deduction.DeductionType.Bonus:
                    TextBlockTitle.Text += " podwyżki";
                    break;
            }

            LoadDataForDeduction();
        }

        private void LoadDataForDeduction()
        {
            ListBoxDeductionsToSelect.ItemsSource = LoadDeductionsFromDB();
            ListBoxSelectedDeductions.ItemsSource = registerPayment.selectedDeductions.Where(d => d.Type == selectedDeductionType);
        }

        private List<Deduction> LoadDeductionsFromDB()
        {
            return DeductionRepository.GetDeductionsForSelection(
                registerPayment.selectedEmployee?.Department?.Country,
                selectedDeductionType,
                registerPayment.selectedDeductions);
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            //TODO:Implement deduction selection
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Confirm if anything is selected
            Close();
        }

        private void ButtonMove_Click(object sender, RoutedEventArgs e)
        {
            Deduction? deduction = ListBoxDeductionsToSelect.SelectedItem as Deduction;

            if (deduction == null) 
            {
                MessageBox.Show("Nie wolno!");
                return;
            }
            
            registerPayment.selectedDeductions.Add(deduction);
            LoadDataForDeduction();

            ListBoxSelectedDeductions.Items.Refresh();
            ListBoxDeductionsToSelect.Items.Refresh();
        }

        private void ButtonMoveAll_Click(object sender, RoutedEventArgs e)
        {
            registerPayment.selectedDeductions.AddRange(LoadDeductionsFromDB());
            LoadDataForDeduction();

            ListBoxSelectedDeductions.Items.Refresh();
            ListBoxDeductionsToSelect.Items.Refresh();
        }

        private void ButtonRevert_Click(object sender, RoutedEventArgs e)
        {
            Deduction? deduction = ListBoxSelectedDeductions.SelectedItem as Deduction;

            if (deduction == null)
            {
                MessageBox.Show("Nie wolno!");
                return;
            }

            //In Revert functions there might be not returning deductions, 
            //but removing them and loading from DB

            registerPayment.selectedDeductions.Remove(deduction);
            LoadDataForDeduction();

            ListBoxSelectedDeductions.Items.Refresh();
            ListBoxDeductionsToSelect.Items.Refresh();
        }

        private void ButtonRevertAll_Click(object sender, RoutedEventArgs e)
        {
            registerPayment.selectedDeductions.RemoveAll(d => d.Type == selectedDeductionType);
            LoadDataForDeduction();

            ListBoxSelectedDeductions.Items.Refresh();
            ListBoxDeductionsToSelect.Items.Refresh();
        }
    }
}
