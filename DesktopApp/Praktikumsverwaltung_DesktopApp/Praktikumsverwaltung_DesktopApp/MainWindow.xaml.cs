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

            try
            {
                GatewayDatabase db = GatewayDatabase.newInstance();

                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");

                WebBrowser myWebBrowser = new WebBrowser();
                myWebBrowser.Height = 300;
                myWebBrowser.Width = 300;
                myWebBrowser.Navigate(strBuilder.ToString());

                List<string> listMongoDBEntries = db.GetAllEntries();          // mongoDB

                Console.WriteLine(listMongoDBEntries);



                //Label lblEntry = new Label();
                //lblEntry.Content = "dsfkadslfjlsd";

                //ListViewItem lvItem = new ListViewItem();
                //lvItem.Content = lblEntry;

                //ListViewItem lvItem2 = new ListViewItem();
                //lvItem2.Content = myWebBrowser;

                //this.lvEntries.Items.Add(lvItem);
                //this.lvEntries.Items.Add(lvItem2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private List<Entry> SplitMongoDBEntry(List<string> listMongoDBEntries)
        {
            List<Entry> listSplittedEntries = null;
            Entry myEntry = null;
            string mongoDBString = "";
            string[] mongoDBEntryFields = null;
            try
            {
                foreach (string entryMongoDB in listMongoDBEntries)
                {
                    mongoDBString = entryMongoDB;
                    mongoDBString.Replace('{', ' ');        // Ist am Anfang
                    mongoDBString.Replace('}', ' ');        // Ist am Ende
                    mongoDBString.TrimStart();
                    mongoDBString.TrimEnd();
                    mongoDBEntryFields = mongoDBString.Split(',');
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return listSplittedEntries;
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
