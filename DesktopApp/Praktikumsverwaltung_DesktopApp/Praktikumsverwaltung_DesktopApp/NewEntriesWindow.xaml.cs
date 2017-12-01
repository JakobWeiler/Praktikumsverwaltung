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
        public NewEntriesWindow()
        {
            InitializeComponent();
            this.LoadNewEntries();
            lvNewEntries.SelectionChanged += lvNewEntries_SelectionChanged;
        }

        private void LoadNewEntries()
        {
            BitmapImage imgGreenCheckMark = new BitmapImage(new Uri("../pkgImages/GreenCheckMark.jpg", UriKind.Relative));
            BitmapImage imgRedCross = new BitmapImage(new Uri("../pkgImages/RedCross.jpg", UriKind.Relative));

            lvNewEntries.Items.Add(new { Col1 = "test1", Col2 = imgGreenCheckMark, Col3 = imgRedCross });
            lvNewEntries.Items.Add(new { Col1 = "ahshdbsn", Col2 = imgGreenCheckMark, Col3 = imgRedCross });
        }

        private void lvNewEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvNewEntries.SelectedValue = false;
        }

        private void ClickBtnAccept(object sender, EventArgs e)
        {
            MessageBox.Show("in accept");
        }

        private void ClickBtnDecline(object sender, EventArgs e)
        {
            MessageBox.Show("in reject");
        }
    }
}
