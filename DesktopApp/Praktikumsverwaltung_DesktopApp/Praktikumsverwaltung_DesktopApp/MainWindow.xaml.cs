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
        private bool isAdmin = false;

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
            StringBuilder strBuilderEntry;
            try
            {
                Uri locationUri = new Uri("https://www.google.at/maps/place/Villach/");
                BitmapImage imgPencilEdit = new BitmapImage(new Uri("../pkgImages/Pencil.jpg", UriKind.Relative));
                BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

                List<Entry> listEntries = db.GetAllEntries();          // WebService get all entries

                foreach (Entry entry in listEntries)
                {
                    strBuilderEntry = new StringBuilder();
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate.ToString("dd.MM.yyyy") + " bis " + entry.EndDate.ToString("dd.MM.yyyy") + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + " €" + Environment.NewLine);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");
                    
                    //StringBuilder strBuilderAddress = new StringBuilder();
                    //strBuilderAddress.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");
                    
                    // Entries only editable if admin
                    if (isAdmin == false)
                    {
                        this.gvColumnEditAdmin.Width = 0;
                        this.gvColumnRemoveAdmin.Width = 0;
                    }
                    else
                    {
                        this.gvColumnEditAdmin.Width = 60;
                        this.gvColumnRemoveAdmin.Width = 60;
                    }

                    lvEntries.Items.Add(new { Col1 = strBuilderEntry.ToString(), Col2 = locationUri, Col3 = imgPencilEdit, Col4 = imgRedCross });
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
            if (db.IsAdmin)
            {    
                MenuItem mItemNewEntries = new MenuItem();
                mItemNewEntries.Header = "New Entries";

                MenuItem mItemNewEntriesShow = new MenuItem();
                mItemNewEntriesShow.Header = "Show";
                mItemNewEntriesShow.Click += (s, e) => { mItemNewEntriesShow_Click(s, e); };
                
                mItemNewEntries.Items.Add(mItemNewEntriesShow);
                this.menuBar.Items.Insert(2, mItemNewEntries);          // Insert... adds menuitem add a specific position

                List<Entry> listUnacceptedAndUnseenEntries = this.db.GetAllUnacceptedEntries();
                this.lblAdminAmountOfNewEntries.Visibility = Visibility.Visible;
                this.lblAdminAmountOfNewEntries.Content = listUnacceptedAndUnseenEntries.Count + " new entries";
            }
        }

        // to disable the selection of each item of the listview
        private void lvEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEntries.SelectedValue = false;
        }

        private void mItemNewEntriesShow_Click(object sender, EventArgs e)
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

        private void mItemEntryEdit_Click(object sender, RoutedEventArgs e)
        {
            EditEntry editEntry = new EditEntry();
            editEntry.Show();
        }

        private void mItemLogout_Click(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        private void ClickBtnEditAdmin(object sender, EventArgs e)
        {
            MessageBox.Show("in edit");
        }

        private void ClickBtnRemoveAdmin(object sender, EventArgs e)
        {
            MessageBox.Show("in remove");
        }
    }
}
