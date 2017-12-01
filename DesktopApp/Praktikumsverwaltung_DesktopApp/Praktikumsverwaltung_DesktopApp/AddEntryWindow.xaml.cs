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

namespace Praktikumsverwaltung_DesktopApp
{
    /// <summary>
    /// Interaction logic for AddEntryWindow.xaml
    /// </summary>
    public partial class AddEntryWindow : Window
    {
        public AddEntryWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            String title = "", description = "", formOfIntern = "", dpStart = "", dpEnd = "";
            int salary = -1;

            
            try
            {
                lblErrorTitle.Foreground = Brushes.Red;
                lblErrorDescription.Foreground = Brushes.Red;
                lblErrorSalary.Foreground = Brushes.Red;
                lblErrorStartDate.Foreground = Brushes.Red;
                lblErrorEndDate.Foreground = Brushes.Red;
                lblErrorFormOfIntern.Foreground = Brushes.Red;

                if (txtTitle.Text.Length > 0)
                {
                    title = txtTitle.Text;
                    lblErrorTitle.Content = "";
                }
                else
                {
                    lblErrorTitle.Content = "mind. 1 letter";
                }

                if (txtDescription.Text.Length > 0)
                {
                    description = txtDescription.Text;
                    lblErrorDescription.Content = "";
                }
                else
                {
                    lblErrorDescription.Content = "mind. 1 letter";
                }
                
                if (txtSalary.Text.Length > 0)
                {
                    // checks if salary doesn't consist of letters
                    int.TryParse(txtSalary.Text.ToString(), out salary);        // returns the salary (with type int) or 0 if parse isn't possible

                    if (salary > 0 || txtSalary.Text.Equals("0"))       // we have to check if the value is 0, in order to differ from the result of tryParse (Because the user could type in 0).
                    {
                        salary = int.Parse(txtSalary.Text);
                        lblErrorSalary.Content = "";
                    }
                    else
                    {
                        lblErrorSalary.Content = "type in a number (no letters)";
                    }
                }
                else
                {
                    lblErrorSalary.Content = "mind. 1 digit";
                }

                if (datePickerStartDate.Text.Length > 0)
                {
                    dpStart = datePickerStartDate.Text;
                    lblErrorStartDate.Content = "";
                }
                else
                {
                    lblErrorStartDate.Content = "select a date";
                }

                if (datePickerEndDate.Text.Length > 0)
                {
                    dpEnd = datePickerEndDate.Text;
                    lblErrorEndDate.Content = "";
                }
                else
                {
                    lblErrorEndDate.Content = "select a date";
                }

                if (txtFormOfIntern.Text.Length > 0)
                {
                    formOfIntern = txtFormOfIntern.Text;
                    lblErrorFormOfIntern.Content = "";
                }
                else
                {
                    lblErrorFormOfIntern.Content = "mind. 1 letter";
                }

                // salary > -1, because it can be 0
                if (title != null && description != null && formOfIntern != null && salary > -1 && dpStart != null && dpEnd != null)
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
