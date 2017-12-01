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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktikumsverwaltung_DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String username = "", password = "";

            try
            {
                lblErrorPassword.Foreground = Brushes.Red;
                lblErrorUsername.Foreground = Brushes.Red;
                lblMessageLogin.Foreground = Brushes.Red;

                if (txtUsername.Text.Length > 0)
                {
                    username = txtUsername.Text;
                    lblErrorUsername.Content = "";          //to "delete" previous errors                    
                }
                else
                {
                    lblErrorUsername.Content = "min. 1 letter";
                }

                if (txtPassword.Password.Length > 0)
                {
                    password = txtPassword.Password;
                    lblErrorPassword.Content = "";          //to "delete" previous errors                    
                }
                else
                {
                    lblErrorPassword.Content = "min. 1 letter";
                }

                if (username.Length > 0 && password.Length > 0)
                {
                    bool successful = false;

                    GatewayDatabase gatewayDb = GatewayDatabase.newInstance();      // Singleton

                    successful = gatewayDb.ConnectMongoDB();            // Connection MongoDB

                    if (successful == true)
                    {
                        successful = gatewayDb.CheckLogin(username, password);

                        if (successful)
                        {
                            MainWindow myMain = new MainWindow();
                            myMain.Show();
                            this.Close();
                        }
                        else
                        {
                            this.lblMessageLogin.Content = "Login not successful. Try again.";
                        }
                    }
                    else
                    {
                        this.lblMessageLogin.Content = "Connection failed.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
        }
    }
}
