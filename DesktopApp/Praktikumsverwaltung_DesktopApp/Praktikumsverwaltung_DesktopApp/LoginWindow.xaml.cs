using Praktikumsverwaltung_DesktopApp.pkgData;
using Praktikumsverwaltung_DesktopApp.pkgMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    successful = gatewayDb.IsLoginOk(username, password);       // calls the webservice
                    
                    //Init der Klasse mit ip vom server
                    // !!!! ACHTUNG: Greift auf Benutzer von der HTL zu und funktioniert, allerdings haben diese Benutzer ja keine BSON ID's und passen überhaupt nicht mit der MongoDB architektur überein, deshalb verwenden wir unsere MongoDB user
                    //LdapAuthentication ldap = new LdapAuthentication("192.168.128.252");//minerva server, due to slow dns use the IP

                    //überprüfung 
                    //successful = ldap.IsAuthenticatednew("ou=Users,dc=htl-vil,dc=local", username, password);

                    if (successful)
                    {

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        this.lblMessageLogin.Content = "Login not successful. Try again.";
                    }
                }
            }
            catch (Exception ex)
            {
               this.lblMessageLogin.Content = "Error: " + ex.Message;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
        }
    }
}
