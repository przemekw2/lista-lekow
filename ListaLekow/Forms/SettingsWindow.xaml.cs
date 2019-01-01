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
using System.Windows.Forms;

namespace ListaLekow
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataFilePath.Content = "";

            //zaznaczenie wlasciwych checkboksow
            if (Globals.UseDefaultDataFileLocation)
            {
                MyDocumentsRB.IsChecked = true;
            }
            else
            {
                OtherRB.IsChecked = true;
                DataFilePath.Content = Globals.DataFileLocation;
            }

        }

        private void OtherBTN_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    DataFilePath.Content = dialog.SelectedPath;
                    Globals.DataFileLocation = dialog.SelectedPath;
                    OtherRB.IsChecked = true;
                }
            }
        }

        private void OkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (MyDocumentsRB.IsChecked == true)
            {
                Globals.UseDefaultDataFileLocation = true;
            }
            else
            {
                Globals.UseDefaultDataFileLocation = false;
                Globals.DataFileLocation = DataFilePath.Content.ToString();
            }
            this.Close();
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
