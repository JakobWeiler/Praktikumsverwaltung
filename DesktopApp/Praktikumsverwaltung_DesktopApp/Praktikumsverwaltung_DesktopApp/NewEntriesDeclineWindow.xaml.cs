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
    /// Interaction logic for NewEntriesDeclineWindow.xaml
    /// </summary>
    public partial class NewEntriesDeclineWindow : Window
    {
        private NewEntriesWindow newEntriesWindow = null;

        public NewEntriesDeclineWindow(NewEntriesWindow _newEntriesWindow)
        {
            InitializeComponent();

            this.newEntriesWindow = _newEntriesWindow;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string note = "";

            note = this.txtNote.Text;
            this.newEntriesWindow.SaveNote(note);

            this.Close();
        }
    }
}
