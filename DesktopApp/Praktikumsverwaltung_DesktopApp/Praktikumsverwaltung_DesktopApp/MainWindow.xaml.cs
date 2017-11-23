using Praktikumsverwaltung_DesktopApp.pkgData;
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

            this.LoadEntries();
        }

        private void LoadEntries()
        {
            StringBuilder strBuilderEntry = new StringBuilder();
            try
            {
                GatewayDatabase db = GatewayDatabase.newInstance();
                List<Entry> listEntries = db.GetAllEntries();          // mongoDB get all entries

                foreach (Entry entry in listEntries)
                {
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate + " bis " + entry.EndDate + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + Environment.NewLine);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");

                    Label lblEntry = new Label();
                    lblEntry.Content = strBuilderEntry;

                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Content = lblEntry;

                    this.lvEntries.Items.Add(lvItem);
                }             


                //StringBuilder strBuilderAddress = new StringBuilder();
                //strBuilderAddress.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");

                //WebBrowser myWebBrowser = new WebBrowser();
                //myWebBrowser.Height = 300;
                //myWebBrowser.Width = 300;
                //myWebBrowser.Navigate(strBuilder.ToString());

                //ListViewItem lvItem2 = new ListViewItem();
                //lvItem2.Content = myWebBrowser;
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
