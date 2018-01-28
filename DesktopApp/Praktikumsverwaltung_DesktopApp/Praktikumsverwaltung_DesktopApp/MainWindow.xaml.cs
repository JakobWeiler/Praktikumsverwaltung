using Newtonsoft.Json;
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
        private GatewayDatabase gwDatabase = null;
        private List<Entry> listAllEntries = null;      // needed for adminv
        private List<string> listAdminEntryStrings = null;      // needed for admin

        public MainWindow()
        {
            InitializeComponent();

            gwDatabase = GatewayDatabase.newInstance();
            this.listAdminEntryStrings = new List<string>();
            this.LoadEntries();
            this.LoadAdminGuiElements();
            lvEntries.SelectionChanged += lvEntries_SelectionChanged;
        }

        public void LoadEntries()
        {
            StringBuilder strBuilderEntry;
            Company company = null;
            try
            {
                Uri locationUri = new Uri("https://www.google.at/maps/place/Villach/");
                BitmapImage imgPencilEdit = new BitmapImage(new Uri("../pkgImages/Pencil.jpg", UriKind.Relative));
                BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

                this.lvEntries.Items.Clear();
                this.listAllEntries = gwDatabase.GetAllEntries();          // WebService get all entries
                List<Company> listCompanies = gwDatabase.GetAllCompanies();
                this.listAdminEntryStrings = new List<string>();     // immer neu setzen, weil methode wird auch von update von EditEntry aufgerufen

                foreach (Entry entry in listAllEntries)
                {
                    company = listCompanies.Find(delegate (Company item) { return item.Id == entry.IdCompany; });

                    strBuilderEntry = new StringBuilder();      // ACHTUNG: Unbedingt hier new StringBuilder machen, weil man ja für jedes ListViewItem einen neuen String braucht, weil sonst hängen sie zusammen und das WPF spinnt
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate.ToString("dd.MM.yyyy") + " bis " + entry.EndDate.ToString("dd.MM.yyyy") + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + " €" + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Firma: " + company.Name + Environment.NewLine);
                    strBuilderEntry.Append("Standort: " + company.Location + Environment.NewLine);
                    strBuilderEntry.Append("Kontakt: " + company.ContactPerson);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");

                    //StringBuilder strBuilderAddress = new StringBuilder();
                    //strBuilderAddress.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");

                    this.listAdminEntryStrings.Add(strBuilderEntry.ToString());          // !!! um später zuzugreifen zu können

                    // Entries only editable if admin
                    if (gwDatabase.IsAdmin == false)
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
                MessageBox.Show("Error in LoadEntries: " + ex.Message);
            }
        }

        // if user is an admin, there are some additional "special" window elements
        private void LoadAdminGuiElements()
        {
            if (gwDatabase.IsAdmin)
            {    
                MenuItem mItemNewEntries = new MenuItem();
                mItemNewEntries.Header = "New Entries";

                MenuItem mItemNewEntriesShow = new MenuItem();
                mItemNewEntriesShow.Header = "Show";
                mItemNewEntriesShow.Click += (s, e) => { mItemNewEntriesShow_Click(s, e); };
                
                mItemNewEntries.Items.Add(mItemNewEntriesShow);
                this.menuBar.Items.Insert(2, mItemNewEntries);          // Insert... adds menuitem add a specific position

                List<Entry> listUnacceptedAndUnseenEntries = this.gwDatabase.GetAllUnacceptedEntries();
                this.lblAdminAmountOfNewEntries.Visibility = Visibility.Visible;
                this.lblAdminAmountOfNewEntries.Content = listUnacceptedAndUnseenEntries.Count + " new entries";
            }
        }

        public void SetAdminNewEntries()
        {
            List<Entry> listUnacceptedAndUnseenEntries = this.gwDatabase.GetAllUnacceptedEntries();
            this.lblAdminAmountOfNewEntries.Content = listUnacceptedAndUnseenEntries.Count + " new entries";
        }

        // to disable the selection of each item of the listview
        private void lvEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEntries.SelectedValue = false;
        }

        private void mItemNewEntriesShow_Click(object sender, EventArgs e)
        {
            // Load new entries which have to be accepted or rejected
            NewEntriesWindow newEntries = new NewEntriesWindow(this);
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

        private void mItemEntryShow_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow showEntry = new ShowWindow();
            showEntry.Show();
        }

        private void mItemLogout_Click(object sender, EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
        }

        /************************************ Admin ****************************************/

        private void ClickBtnEditAdmin(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                string jsonString = null;
                Button clickedButton = (Button)sender;
                var selectedLvItem = GetAncestorOfType<ListViewItem>(sender as Button);

                string selectedEntryString = selectedLvItem.Content.ToString();
                selectedEntryString = selectedEntryString.Remove(0, 9);           // weil vorderer Teil von listview ein stringteil ist
                selectedEntryString = selectedEntryString.Split(new string[] { ", Col2 = " }, StringSplitOptions.None)[0];        // hinterer Teil ebenfalls

                index = this.listAdminEntryStrings.IndexOf(selectedEntryString);
                Entry selectedEntry = this.listAllEntries.ElementAt(index);

                jsonString = JsonConvert.SerializeObject(selectedEntry);

                EditEntry editEntry = new EditEntry(jsonString, this);
                editEntry.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClickBtnAdminEdit: " + ex.Message);
            }
        }

        private void ClickBtnRemoveAdmin(object sender, EventArgs e)
        {
            MessageBox.Show("in remove");
        }

        // Rekursive Methode. Geht solange in der Treearchitektur zu den Parents hinauf bis es den angegebenen Typ gefunden hat
        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return (T)GetAncestorOfType<T>((FrameworkElement)parent);
            return (T)parent;
        }
    }
}
