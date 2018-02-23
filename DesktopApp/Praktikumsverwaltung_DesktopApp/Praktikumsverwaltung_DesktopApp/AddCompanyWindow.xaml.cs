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
    /// Interaction logic for AddCompanyWindow.xaml
    /// </summary>
    public partial class AddCompanyWindow : Window
    {
        private GatewayDatabase gwDatabase = null;
        private AddEntryWindow myAddEntryWindow = null;

        public AddCompanyWindow(AddEntryWindow addEntryWindow)
        {
            InitializeComponent();

            this.gwDatabase = GatewayDatabase.newInstance();
            this.myAddEntryWindow = addEntryWindow;
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = null, location = null, contactPerson = null;
            int numberOfEmployees = -1;
            bool numberOfEmployeesOk = false, successfullySaved = false;
            Company company = null;

            try
            {
                lblErrorName.Foreground = Brushes.Red;
                lblErrorLocation.Foreground = Brushes.Red;
                lblErrorNumberOfEmployees.Foreground = Brushes.Red;
                lblErrorContactPerson.Foreground = Brushes.Red;

                if (txtName.Text.Length > 0)
                {
                    name = txtName.Text;
                    lblErrorName.Content = "";
                }
                else
                {
                    lblErrorName.Content = "mind. 1 letter";
                }

                if (txtLocation.Text.Length > 0)
                {
                    location = txtLocation.Text;
                    lblErrorLocation.Content = "";
                }
                else
                {
                    lblErrorLocation.Content = "mind. 1 letter";
                }

                if (txtNumberOfEmployees.Text.Length > 0)
                {
                    // checks if salary doesn't consist of letters
                    if (int.TryParse(txtNumberOfEmployees.Text.ToString(), out numberOfEmployees))        // returns the numberOfEmployees (with type int) or 0 if parse isn't possible
                    {
                        if (numberOfEmployees > 0 || txtNumberOfEmployees.Text.Equals("0"))       // we have to check if the value is 0, in order to differ from the result of tryParse (Because the user could type in 0).
                        {
                            numberOfEmployees = int.Parse(txtNumberOfEmployees.Text);
                            lblErrorNumberOfEmployees.Content = "";
                            numberOfEmployeesOk = true;
                        }
                        else
                        {
                            lblErrorNumberOfEmployees.Content = "type in a number >= 0 (no letters). E.g. 7 and NOT 7,8";
                        }
                    }
                    else
                    {
                        lblErrorNumberOfEmployees.Content = "type in a number >= 0 (no letters). E.g. 7 and NOT 7,8";
                    }
                }
                else
                {
                    lblErrorNumberOfEmployees.Content = "mind. 1 digit";
                }

                if (txtContactPerson.Text.Length > 0)
                {
                    contactPerson = txtContactPerson.Text;
                    lblErrorContactPerson.Content = "";
                }
                else
                {
                    lblErrorContactPerson.Content = "mind. 1 letter";
                }

                

                if (name != null && location != null && numberOfEmployees > -1 && numberOfEmployeesOk == true && contactPerson != null)
                {
                    // id is going to be set in GatewayDatabase AddCompany()
                    company = new Company("id-1", name, location, numberOfEmployees, contactPerson);

                    successfullySaved = gwDatabase.AddCompany(company);

                    if (successfullySaved == true)
                    {
                        this.myAddEntryWindow.LoadCompanies();
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
                MessageBox.Show("Error in AddCompany: " + ex.Message);
            }
        }
    }
}
