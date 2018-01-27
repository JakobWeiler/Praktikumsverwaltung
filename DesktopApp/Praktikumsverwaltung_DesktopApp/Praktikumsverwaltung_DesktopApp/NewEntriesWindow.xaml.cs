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
    /// Interaction logic for NewEntriesWindow.xaml
    /// </summary>
    public partial class NewEntriesWindow : Window
    {
        private GatewayDatabase gatewayDatabase = null;
        private List<Entry> listUnacceptedEntries = null;
        private List<string> listEntryStrings = null;

        public NewEntriesWindow()
        {
            InitializeComponent();

            gatewayDatabase = GatewayDatabase.newInstance();
            this.listEntryStrings = new List<string>();
            this.LoadNewEntries();
            lvNewEntries.SelectionChanged += lvNewEntries_SelectionChanged;
        }

        private void LoadNewEntries()
        {
            BitmapImage imgGreenCheckMark = new BitmapImage(new Uri("../pkgImages/GreenCheckMark.jpg", UriKind.Relative));
            BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

            try {
                StringBuilder strBuilderEntry;

                this.listUnacceptedEntries = gatewayDatabase.GetAllUnacceptedEntries();

                foreach (Entry entry in this.listUnacceptedEntries)
                {
                    strBuilderEntry = new StringBuilder();
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate.ToString("dd.MM.yyyy") + " bis " + entry.EndDate.ToString("dd.MM.yyyy") + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + " €" + Environment.NewLine);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");

                    this.listEntryStrings.Add(strBuilderEntry.ToString());

                    lvNewEntries.Items.Add(new { Col1 = strBuilderEntry.ToString(), Col2 = imgGreenCheckMark, Col3 = imgRedCross });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadAllUnacceptedEntries: " + ex.Message);
            }
        }

        // to disable the selection of each item of the listview
        private void lvNewEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvNewEntries.SelectedValue = false;
        }

        private void ClickBtnAccept(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                Button clickedButton = (Button)sender;
                var selectedLvItem = GetAncestorOfType<ListViewItem>(sender as Button);

                if (selectedLvItem != null)
                {
                    string selectedEntryString = selectedLvItem.Content.ToString();
                    selectedEntryString = selectedEntryString.Remove(0, 9);           // weil vorderer Teil von listview ein stringteil ist
                    selectedEntryString = selectedEntryString.Split(',')[0];        // hinterer Teil ebenfalls

                    index = this.listEntryStrings.IndexOf(selectedEntryString);
                    Entry selectedEntry = this.listUnacceptedEntries.ElementAt(index);

                    selectedEntry.AllowedTeacher = true;
                    selectedEntry.AllowedAV = true;
                    selectedEntry.SeenByAdmin = true;

                    gatewayDatabase.UpdateEntry(selectedEntry);

                    // Delete the edited entries
                    this.listUnacceptedEntries.RemoveAt(index);
                    this.listEntryStrings.RemoveAt(index);
                    this.lvNewEntries.Items.RemoveAt(index);
                }            
                else
                {
                    MessageBox.Show("Error in ClickBtnDecline no ListViewItem found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClickBtnAccept: " + ex.Message);
            }
           
            MessageBox.Show("in accept");
        }

        private void ClickBtnDecline(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                Button clickedButton = (Button)sender;
                var selectedLvItem = GetAncestorOfType<ListViewItem>(sender as Button);

                if (selectedLvItem != null)
                {
                    string selectedEntryString = selectedLvItem.Content.ToString();
                    selectedEntryString = selectedEntryString.Remove(0, 9);           // weil vorderer Teil von listview ein stringteil ist
                    selectedEntryString = selectedEntryString.Split(',')[0];        // hinterer Teil ebenfalls

                    index = this.listEntryStrings.IndexOf(selectedEntryString);
                    Entry selectedEntry = this.listUnacceptedEntries.ElementAt(index);

                    selectedEntry.AllowedTeacher = false;
                    selectedEntry.AllowedAV = false;
                    selectedEntry.SeenByAdmin = true;

                    gatewayDatabase.UpdateEntry(selectedEntry);

                    // Delete the edited entries
                    this.listUnacceptedEntries.RemoveAt(index);
                    this.listEntryStrings.RemoveAt(index);
                    this.lvNewEntries.Items.RemoveAt(index);
                }
                else
                {
                    MessageBox.Show("Error in ClickBtnDecline no ListViewItem found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClickBtnDecline: " + ex.Message);
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
