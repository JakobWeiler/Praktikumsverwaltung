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
    public partial class MainWindow : Window
    {
        public MainWindow()
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

                if (txtUsername.Text.Length > 0)
                {
                    username = txtUsername.Text;
                    lblErrorUsername.Content = "";          //to "delete" previous errors

                    if (txtPassword.Password.Length > 0)
                    {
                        password = txtPassword.Password;
                        lblErrorPassword.Content = "";          //to "delete" previous errors
                    }
                    else
                    {
                        lblErrorPassword.Content = "mind. 1 Zeichen";
                    }
                }
                else
                {
                    lblErrorUsername.Content = "mind. 1 Zeichen";                    

                    if (txtPassword.Password.Length == 0)
                    {
                        lblErrorPassword.Content = "mind. 1 Zeichen";
                    }
                    else
                    {
                        lblErrorPassword.Content = "";          //to "delete" previous errors
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
