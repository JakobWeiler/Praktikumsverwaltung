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
                myWebBrowser.Height = 300;
                myWebBrowser.Width = 700;
                myWebBrowser.Navigate(strBuilder.ToString());

                //Label lbl = new Label();
                //lbl.Content = "dsfkadslfjlsd";

                //object[] myRow = new object[] { lbl, myWebBrowser};

                //ListViewItem lv = new ListViewItem();

                //lv.Content = myWebBrowser;

                //this.lvEntries.Items.Add(lv);
                DataTable dt = new DataTable();
                dt.Columns.Add("Description");
                dt.Columns.Add("Location");
                DataRow dr = dt.NewRow();
                dr["Description"] = "klllk";
                dr["Location"] = myWebBrowser;
                
                dt.Rows.Add(dr);
                this.dgEntries.DataContext = dt.DefaultView;
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
