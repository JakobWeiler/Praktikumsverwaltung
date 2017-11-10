using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");

                WebBrowser myWebBrowser = new WebBrowser();
                myWebBrowser.Navigate(strBuilder.ToString());

                DataTable dt = new DataTable();
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Location", typeof(WebBrowser));

                DataRow myRow = dt.NewRow();
                myRow[0] = "some description";
                myRow[1] = myWebBrowser;

                dt.Rows.Add(myRow);
                this.dataGrid.DataContext = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void mItemEntryAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEntryWindow addEntryWindow = new AddEntryWindow();
                addEntryWindow.Show();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
