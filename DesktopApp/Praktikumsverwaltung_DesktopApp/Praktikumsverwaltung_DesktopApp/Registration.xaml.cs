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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            String firstname = "", lastname = "", email = "", username = "", password1 = "", password2 = "";
            Boolean passwordsEqual = false;

            try
            {
                lblErrorFirstName.Foreground = Brushes.Red;
                lblErrorLastName.Foreground = Brushes.Red;
                lblErrorEmail.Foreground = Brushes.Red;
                lblErrorUsername.Foreground = Brushes.Red;
                lblErrorPassword1.Foreground = Brushes.Red;
                lblErrorPassword2.Foreground = Brushes.Red;
                lblMessageRegistration.Foreground = Brushes.Red;

                if (txtFirstname.Text.Length > 0)
                {
                    firstname = txtFirstname.Text;
                    lblErrorFirstName.Content = "";          //to "delete" previous errors
                }
                else
                {
                    lblErrorFirstName.Content = "min. 1 letter";
                }

                if (txtLastname.Text.Length > 0)
                {
                    lastname = txtLastname.Text;
                    lblErrorLastName.Content = "";          //to "delete" previous errors
                }
                else
                {
                    lblErrorLastName.Content = "min. 1 letter";
                }

                if (txtEmail.Text.Length > 0)
                {
                    email = txtEmail.Text;
                    lblErrorEmail.Content = "";          //to "delete" previous errors
                }
                else
                {
                    lblErrorEmail.Content = "min. 1 letter";
                }

                if (txtUsername.Text.Length > 0)
                {
                    username = txtUsername.Text;
                    lblErrorUsername.Content = "";          //to "delete" previous errors
                }
                else
                {
                    lblErrorUsername.Content = "min. 1 letter";
                }

                if (txtPassword1.Password.Length > 0)
                {
                    password1 = txtPassword1.Password;
                    lblErrorPassword1.Content = "";          //to "delete" previous errors
                }
                else
                {
                    lblErrorPassword1.Content = "min. 1 letter";
                }

                if (txtPassword2.Password.Length > 0)
                {
                    password2 = txtPassword2.Password;
                    lblErrorPassword2.Content = "";          //to "delete" previous errors
                }
                else
                {
                    lblErrorPassword2.Content = "min. 1 letter";
                }

                if (password1.Equals(password2) && txtPassword1.Password.Length > 0 && txtPassword2.Password.Length > 0)
                {
                    passwordsEqual = true;
                    lblErrorPassword1.Content = "";
                    lblErrorPassword2.Content = "";
                }
                else
                {
                    passwordsEqual = false;
                    lblErrorPassword1.Content = "passwords must be equal";
                    lblErrorPassword2.Content = "passwords must be equal";
                }

                // Opens MainWindow if all fields are filled with proper values
                if (passwordsEqual == true && firstname.Length > 0 && lastname.Length > 0 && email.Length > 0 && username.Length > 0)
                {
                    //Pupil pupil = new Pupil(firstname, lastname, email, username, password1);
                    //GatewayDatabase gatewayDb = GatewayDatabase.newInstance();      // Singleton

                    //bool successful = gatewayDb.AddPupil(pupil);

                    //if (successful)
                    //{
                        MainWindow myMain = new MainWindow();
                        myMain.Show();
                    //}
                    //else
                    //{
                    //    this.lblMessageRegistration.Content = "Registration not successful.";
                    //}                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
