using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace Praktikumsverwaltung_DesktopApp
{
    /// <summary>
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        private GatewayDatabase gwDatabase = null;
        private List<Entry> listAllOwnEntries = null;
        private List<string> listEntryStrings = null;

        public ShowWindow()
        {
            InitializeComponent();

            this.gwDatabase = GatewayDatabase.newInstance();
            this.listEntryStrings = new List<string>();
            this.LoadAllOwnEntries();
            lvEntries.SelectionChanged += lvNewEntries_SelectionChanged;
        }

        private void LoadAllOwnEntries()
        {
            StringBuilder strBuilderEntry;
            try
            {
                GatewayDatabase gatewayDatabase = GatewayDatabase.newInstance();
                Uri locationUri = new Uri("https://www.google.at/maps/place/Villach/");

                BitmapImage imgPencilEdit = new BitmapImage(new Uri("../pkgImages/Pencil.jpg", UriKind.Relative));
                BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

                this.listAllOwnEntries = gatewayDatabase.GetAllOwnEntries();         // !!! loads only the own entries

                foreach (Entry entry in listAllOwnEntries)
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

                    this.listEntryStrings.Add(strBuilderEntry.ToString());

                    lvEntries.Items.Add(new { Col1 = strBuilderEntry.ToString(), Col2 = locationUri, Col3 = imgPencilEdit, Col4 = imgRedCross });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadAllOwnEntries: " + ex.Message);
            }
        }

        // to disable the selection of each item of the listview
        private void lvNewEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEntries.SelectedValue = false;
        }

        private void ClickBtnEdit(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                string jsonString = null;
                Button clickedButton = (Button)sender;
                var selectedLvItem = GetAncestorOfType<ListViewItem>(sender as Button);

                string selectedEntryString = selectedLvItem.Content.ToString();
                selectedEntryString = selectedEntryString.Remove(0, 9);           // weil vorderer Teil von listview ein stringteil ist
                selectedEntryString = selectedEntryString.Split(',')[0];        // hinterer Teil ebenfalls

                index = this.listEntryStrings.IndexOf(selectedEntryString);
                Entry selectedEntry = this.listAllOwnEntries.ElementAt(index);

                jsonString = JsonConvert.SerializeObject(selectedEntry);

                EditEntry editEntry = new EditEntry(jsonString);
                editEntry.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClickBtnEdit: " + ex.Message);
            }            
        }

        private void ClickBtnRemove(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                bool successfull = false;
                Button clickedButton = (Button)sender;
                var selectedLvItem = GetAncestorOfType<ListViewItem>(sender as Button);

                string selectedEntryString = selectedLvItem.Content.ToString();
                selectedEntryString = selectedEntryString.Remove(0, 9);           // weil vorderer Teil von listview ein stringteil ist
                selectedEntryString = selectedEntryString.Split(',')[0];        // hinterer Teil ebenfalls

                index = this.listEntryStrings.IndexOf(selectedEntryString);
                Entry selectedEntry = this.listAllOwnEntries.ElementAt(index);

                successfull = gwDatabase.DeleteEntry(selectedEntry.Id);

                if (successfull)
                {
                    // Delete entry of the lists
                    this.listAllOwnEntries.RemoveAt(index);
                    this.listEntryStrings.RemoveAt(index);
                    this.lvEntries.Items.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show("not successfully removed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClickBtnEdit: " + ex.Message);
            }
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
