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

namespace ListaLekow
{
    /// <summary>
    /// Interaction logic for DaneOsoboweWindow.xaml
    /// </summary>
    public partial class DaneOsoboweWindow : Window
    {
        MainWindow mwInstance = (MainWindow)Application.Current.MainWindow;
        private DaneOsobowe daneOsobowe;
        public DaneOsobowe DaneOsobowe
        {
            get
            {
                if (this.daneOsobowe == null)
                {
                    this.daneOsobowe = new DaneOsobowe("", "", "", "");
                }
                return this.daneOsobowe;
            }
            set { this.daneOsobowe = value; }
        }

        public DaneOsoboweWindow(DaneOsobowe daneOsobowe)
        {
            InitializeComponent();
            DaneOsobowe = daneOsobowe;
            this.DataContext = DaneOsobowe;
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {

            mwInstance.DaneOsobowe = this.DaneOsobowe;
            this.Close();
        }
    }
}
