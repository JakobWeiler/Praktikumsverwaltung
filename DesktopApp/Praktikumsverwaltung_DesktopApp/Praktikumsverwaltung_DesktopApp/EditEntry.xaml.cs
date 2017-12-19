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
            this.LoadNewEntries();
            lvEntries.SelectionChanged += lvNewEntries_SelectionChanged;
        }

        private void LoadNewEntries()
        {
            Uri locationUri = new Uri("https://www.google.at/maps/place/Villach/");

            BitmapImage imgPencilEdit = new BitmapImage(new Uri("../pkgImages/Pencil.jpg", UriKind.Relative));
            BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

            lvEntries.Items.Add(new { Col1 = "test1", Col2 = locationUri, Col3 = imgPencilEdit, Col4 = imgRedCross });
            lvEntries.Items.Add(new { Col1 = "ahshdbsn", Col2 = locationUri, Col3 = imgPencilEdit, Col4 = imgRedCross });
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
