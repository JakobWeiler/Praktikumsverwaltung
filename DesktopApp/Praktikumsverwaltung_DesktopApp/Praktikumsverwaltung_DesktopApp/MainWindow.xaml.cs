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
        private GatewayDatabase db = null;

        public MainWindow()
        {
            InitializeComponent();

            db = GatewayDatabase.newInstance();
            this.LoadEntries();
            this.LoadAdminGuiElements();
            lvEntries.SelectionChanged += lvEntries_SelectionChanged;
        }

        private void LoadEntries()
        {
            StringBuilder strBuilderEntry = new StringBuilder();
            try
            {
                List<Entry> listEntries = db.GetAllEntries();          // mongoDB get all entries

                foreach (Entry entry in listEntries)
                {
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate.ToString("dd.MM.yyyy") + " bis " + entry.EndDate.ToString("dd.MM.yyyy") + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + Environment.NewLine);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");
                    
                    //StringBuilder strBuilderAddress = new StringBuilder();
                    //strBuilderAddress.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");

                    Uri locationUri = new Uri("https://www.google.at/maps/place/Villach/");

                    lvEntries.Items.Add(new { Col1 = strBuilderEntry.ToString(), Col2 = locationUri });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // if user is an admin, there are some additional "special" window elements
        private void LoadAdminGuiElements()
        {
            if (db.IsAdmin())
            {
                MenuItem mItemNewEntries = new MenuItem();
                mItemNewEntries.Header = "New Entries";

                MenuItem mItemShow = new MenuItem();
                mItemShow.Header = "Show";
                mItemShow.Click += (s, e) => { mItemShow_Click(s, e); };

                mItemNewEntries.Items.Add(mItemShow);
                this.menuBar.Items.Add(mItemNewEntries);
            }
        }

        private void lvEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEntries.SelectedValue = false;
        }

        private void mItemShow_Click(object sender, EventArgs e)
        {
            // Load new entries which have to be accepted or rejected
            NewEntriesWindow newEntries = new NewEntriesWindow();
            newEntries.Show();
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
