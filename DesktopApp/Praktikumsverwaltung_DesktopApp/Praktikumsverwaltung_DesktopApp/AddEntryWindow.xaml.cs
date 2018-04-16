using MongoDB.Bson;
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
    /// Interaction logic for AddEntryWindow.xaml
    /// </summary>
    public partial class AddEntryWindow : Window
    {
        private GatewayDatabase gwDatabase = null;
        private List<Company> listCompanies = null;
        private List<string> listStringCompanies = null;

        public AddEntryWindow()
        {
            InitializeComponent();

            this.gwDatabase = GatewayDatabase.newInstance();
            this.listStringCompanies = new List<string>();
            LoadCompanies();
        }

        public void LoadCompanies()
        {
            string companyString = null;
            try
            {
                this.listCompanies = gwDatabase.GetAllCompanies();

                // Weil die LoadCompanies() Methode auch von AddCompany aufgerufen wird
                this.cbCompany.Items.Clear();
                this.listStringCompanies = new List<string>();

                foreach (Company c in listCompanies)
                {
                    companyString = c.Name + ", " + c.Location + ", " + c.ContactPerson;
                    this.cbCompany.Items.Add(companyString);
                    this.listStringCompanies.Add(companyString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadCompanies: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string title = null, description = null, dpStart = null, dpEnd = null, companyString = null;
            double salary = -1;
            bool salaryOk = false, successfullySaved = false;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            Company company = null;

            
            try
            {
                lblErrorTitle.Foreground = Brushes.Red;
                lblErrorDescription.Foreground = Brushes.Red;
                lblErrorSalary.Foreground = Brushes.Red;
                lblErrorStartDate.Foreground = Brushes.Red;
                lblErrorEndDate.Foreground = Brushes.Red;
                lblErrorCompany.Foreground = Brushes.Red;

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

                if (cbCompany.Text.Length > 0)
                {
                    companyString = cbCompany.Text;
                    int index = this.listStringCompanies.IndexOf(companyString);
                    company = this.listCompanies.ElementAt(index);
                    
                    lblErrorCompany.Content = "";
                }
                else
                {
                    lblErrorCompany.Content = "select a company";
                }

                // salary > -1, because it can be 0
                if (title != null && description != null && salary > -1 && salaryOk == true && dpStart != null && dpEnd != null && company != null)
                {
                    // id's are going to be set in GatewayDatabase AddEntry()
                    Entry entry = new Entry("id-1", startDate, endDate, title, description, salary, false, false, false, "Admin has to accept.", "id-1", "id-1", company.Id);
                    
                    successfullySaved = gwDatabase.AddEntry(entry);

                    if (successfullySaved == true)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Not successfully saved.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in AddEntry: " + ex.Message);
            }
        }

        private void btnCompanyNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddCompanyWindow addCompany = new AddCompanyWindow(this);           // !!!!!! this ... gibt dieses Window mit, um die ComboBox im anderen Window zu reloaden
                addCompany.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in AddCompany: " + ex.Message);
            }
        }
    }
}
