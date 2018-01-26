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

        public NewEntriesWindow()
        {
            InitializeComponent();

            gatewayDatabase = GatewayDatabase.newInstance();
            this.LoadNewEntries();
            lvNewEntries.SelectionChanged += lvNewEntries_SelectionChanged;
        }

        private void LoadNewEntries()
        {
            BitmapImage imgGreenCheckMark = new BitmapImage(new Uri("../pkgImages/GreenCheckMark.jpg", UriKind.Relative));
            BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

            //lvNewEntries.Items.Add(new { Col1 = "test1", Col2 = imgGreenCheckMark, Col3 = imgRedCross });
            //lvNewEntries.Items.Add(new { Col1 = "ahshdbsn", Col2 = imgGreenCheckMark, Col3 = imgRedCross });

            try {
                StringBuilder strBuilderEntry = new StringBuilder();

                this.listUnacceptedEntries = gatewayDatabase.GetAllUnacceptedEntries();

                foreach (Entry entry in this.listUnacceptedEntries)
                {
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate.ToString("dd.MM.yyyy") + " bis " + entry.EndDate.ToString("dd.MM.yyyy") + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + " €" + Environment.NewLine);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");

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
                int index = lvNewEntries.SelectedIndex;
                Entry selectedEntry = this.listUnacceptedEntries.ElementAt(index);

                selectedEntry.AllowedTeacher = true;
                selectedEntry.AllowedAV = true;

                gatewayDatabase.UpdateEntry(selectedEntry);

                // Delete the edited entries
                this.listUnacceptedEntries.RemoveAt(index);
                this.lvNewEntries.Items.RemoveAt(index);
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
                int index = lvNewEntries.SelectedIndex;
                Entry selectedEntry = this.listUnacceptedEntries.ElementAt(index);

                selectedEntry.AllowedTeacher = false;
                selectedEntry.AllowedAV = false;

                gatewayDatabase.UpdateEntry(selectedEntry);

                // Delete the edited entries
                this.listUnacceptedEntries.RemoveAt(index);
                this.lvNewEntries.Items.RemoveAt(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in ClickBtnDecline: " + ex.Message);
            }

            MessageBox.Show("in decline");
        }
    }
}
