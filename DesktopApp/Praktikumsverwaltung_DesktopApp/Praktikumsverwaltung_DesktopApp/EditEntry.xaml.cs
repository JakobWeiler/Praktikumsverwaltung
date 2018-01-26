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
    /// Interaction logic for EditEntry.xaml
    /// </summary>
    public partial class EditEntry : Window
    {
        public EditEntry()
        {
            InitializeComponent();
            this.LoadAllOwnEntries();
            lvEntries.SelectionChanged += lvNewEntries_SelectionChanged;
        }
        
        private void LoadAllOwnEntries()
        {
            List<Entry> listAllOwnEntries;
            StringBuilder strBuilderEntry = new StringBuilder();
            try
            {
                GatewayDatabase gatewayDatabase = GatewayDatabase.newInstance();
                Uri locationUri = new Uri("https://www.google.at/maps/place/Villach/");

                BitmapImage imgPencilEdit = new BitmapImage(new Uri("../pkgImages/Pencil.jpg", UriKind.Relative));
                BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

                listAllOwnEntries = gatewayDatabase.GetAllOwnEntries();         // !!! loads only the own entries

                foreach (Entry entry in listAllOwnEntries)
                {
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Title + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append(entry.Description + Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("Dauer: " + entry.StartDate.ToString("dd.MM.yyyy") + " bis " + entry.EndDate.ToString("dd.MM.yyyy") + Environment.NewLine);
                    strBuilderEntry.Append("Gehalt: " + entry.Salary + " €" + Environment.NewLine);
                    strBuilderEntry.Append(Environment.NewLine + Environment.NewLine);
                    strBuilderEntry.Append("---------------------------------------------------------------------------");

                    //StringBuilder strBuilderAddress = new StringBuilder();
                    //strBuilderAddress.Append("https://www.google.ca/maps/place/Tschinowitscher+Weg+20,+9500+Villach");
                                        
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
            MessageBox.Show("in edit");
        }

        private void ClickBtnRemove(object sender, EventArgs e)
        {
            MessageBox.Show("in remove");
        }
    }
}
