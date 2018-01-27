using Newtonsoft.Json;
using Praktikumsverwaltung_DesktopApp.pkgData;
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
    /// Interaction logic for EditEntry.xaml
    /// </summary>
    public partial class EditEntry : Window
    {
        private Entry selectedEntry = null;
        GatewayDatabase gwDatabase = null;

        public EditEntry(string entry)
        {
            InitializeComponent();

            try
            {
                this.gwDatabase = GatewayDatabase.newInstance();
                this.selectedEntry = JsonConvert.DeserializeObject<Entry>(entry);

                FillTextFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in InitializeComponent: " + ex.Message);
            }
        }

        private void FillTextFields()
        {
            try
            {
                this.txtTitle.Text = selectedEntry.Title;
                this.txtDescription.Text = selectedEntry.Description;
                this.txtSalary.Text = selectedEntry.Salary.ToString();                
                this.datePickerStartDate.Text = selectedEntry.StartDate.ToString();
                this.datePickerEndDate.Text = selectedEntry.EndDate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in FillTextFields: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string title = "", description = "", dpStart = "", dpEnd = "";
            double salary = -1;
            bool salaryOk = false, successfullyUpdated = false;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();


            try
            {
                lblErrorTitle.Foreground = Brushes.Red;
                lblErrorDescription.Foreground = Brushes.Red;
                lblErrorSalary.Foreground = Brushes.Red;
                lblErrorStartDate.Foreground = Brushes.Red;
                lblErrorEndDate.Foreground = Brushes.Red;

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
                    if (double.TryParse(txtSalary.Text.ToString(), out salary))        // returns the salary (with type int) or 0 if parse isn't possible
                    {
                        if (salary > 0 || txtSalary.Text.Equals("0"))       // we have to check if the value is 0, in order to differ from the result of tryParse (Because the user could type in 0).
                        {
                            salary = Double.Parse(txtSalary.Text);
                            lblErrorSalary.Content = "";
                            salaryOk = true;            // because also if salary is false e.g. 45.7 instead of 45,7 the tryparse methode writes a value in salary. This causes problems because then the if below is senseless as it calls the webservice because salary is bigger -1
                        }
                        else
                        {
                            lblErrorSalary.Content = "type in a number >= 0 (no letters). E.g. 634,7 and NOT 634.7";
                        }
                    }
                    else
                    {
                        lblErrorSalary.Content = "type in a number >= 0 (no letters). E.g. 634,7 and NOT 634.7";
                    }

                }
                else
                {
                    lblErrorSalary.Content = "mind. 1 digit";
                }

                if (datePickerStartDate.Text.Length > 0)
                {
                    dpStart = datePickerStartDate.Text;
                    startDate = datePickerStartDate.SelectedDate.Value.Date;
                    lblErrorStartDate.Content = "";
                }
                else
                {
                    lblErrorStartDate.Content = "select a date";
                }

                if (datePickerEndDate.Text.Length > 0)
                {
                    dpEnd = datePickerEndDate.Text;
                    endDate = datePickerEndDate.SelectedDate.Value.Date;
                    lblErrorEndDate.Content = "";
                }
                else
                {
                    lblErrorEndDate.Content = "select a date";
                }

                // salary > -1, because it can be 0
                if (title != null && description != null && salary > -1 && salaryOk == true && dpStart != null && dpEnd != null)
                {
                    Entry entry = new Entry(selectedEntry.Id, startDate, endDate, title, description, salary, false, false, false, selectedEntry.IdPupil, selectedEntry.IdClass, selectedEntry.IdCompany);
                    
                    successfullyUpdated = gwDatabase.UpdateEntry(entry);

                    if (successfullyUpdated == true)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Not successfully updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in UpdateEntry: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
