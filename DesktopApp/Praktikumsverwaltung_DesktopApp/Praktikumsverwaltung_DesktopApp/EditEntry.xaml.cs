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
        private List<Company> listCompanies = null;
        private List<string> listStringCompanies = null;

        private ShowWindow myShowWindow = null;
        private MainWindow myMainWindow = null;
        private bool flagShowWindow = false;
        private bool flagMainWindow = false;

        public EditEntry(string entry, ShowWindow showWindow)
        {
            InitializeComponent();

            try
            {
                this.gwDatabase = GatewayDatabase.newInstance();
                this.listStringCompanies = new List<string>();
                this.myShowWindow = showWindow;
                this.flagShowWindow = true;

                this.selectedEntry = JsonConvert.DeserializeObject<Entry>(entry);

                FillTextFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in InitializeComponent: " + ex.Message);
            }
        }

        public EditEntry(string entry, MainWindow mainWindow)
        {
            InitializeComponent();

            try
            {
                this.gwDatabase = GatewayDatabase.newInstance();
                this.listStringCompanies = new List<string>();
                this.myMainWindow = mainWindow;
                this.flagMainWindow = true;

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

                LoadCompanies();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in FillTextFields: " + ex.Message);
            }
        }

        public void LoadCompanies()
        {
            string companyString = null, selectedCompany = null;

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

                    if (c.Id.Equals(this.selectedEntry.IdCompany))
                    {
                        selectedCompany = companyString;
                    }
                }

                this.cbCompany.SelectedItem = selectedCompany;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadCompanies: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string title = null, description = null, dpStart = null, dpEnd = null, companyString = null;
            double salary = -1;
            bool salaryOk = false, successfullyUpdated = false;
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
                    Entry entry = null;

                    if (flagShowWindow == true)
                    {
                        entry = new Entry(selectedEntry.Id, startDate, endDate, title, description, salary, false, false, false, "Admin has to accept.", selectedEntry.IdPupil, selectedEntry.IdClass, company.Id);
                    }
                    else
                        if (flagMainWindow == true)     // Bei Admin braucht man allowedKV, allowedAV und seenByAdmin nicht auf false setzen, sonst müsste der Admin es sich selbst akzeptiern
                        {
                            entry = new Entry(selectedEntry.Id, startDate, endDate, title, description, salary, true, true, true, "Accepted.", selectedEntry.IdPupil, selectedEntry.IdClass, company.Id);
                        }
                    
                    successfullyUpdated = gwDatabase.UpdateEntry(entry);

                    if (successfullyUpdated == true)
                    {
                        // !!!!!! Schauen von welchem Window das EditEntry aufgerufen wurde (ShowWindow...Pupil, MainWindow...Admin)
                        if (this.flagShowWindow == true)
                        {
                            this.myShowWindow.LoadAllOwnEntries();          // Weil sich ja manche Werte beim Update verändert haben
                        }
                        else
                            if (this.flagMainWindow == true)
                            {
                                this.myMainWindow.LoadEntries();
                            }
                        
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
