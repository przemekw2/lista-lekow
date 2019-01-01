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
    /// Interaction logic for LekEdycjaWindow.xaml
    /// </summary>
    public partial class LekEdycjaWindow : Window
    {
        MainWindow mwInstance = (MainWindow)Application.Current.MainWindow;
        public Lek TempLek { get; set; }
        private List<Lek> lekiLista;

        public List<Lek> LekiLista
        {
            get
            {
                if (this.lekiLista == null)
                {
                    this.lekiLista = new List<Lek>();
                }
                return this.lekiLista;
            }
            set { this.lekiLista = value; }
        }

        public LekEdycjaWindow(Lek tempLek, List<Lek> lekiLista)
        {
            InitializeComponent();
            TempLek = tempLek;
            LekiLista = lekiLista;
            this.DataContext = TempLek;
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            //zamkniecie okna
            this.Close();
        }

        private void UptdateBTN_Click(object sender, RoutedEventArgs e)
        {
            //aktualizacja leku w liscie
            
            Lek lekItem = LekiLista.FirstOrDefault(d => d.ID == TempLek.ID);
            if (lekItem != null)
            {
                lekItem.NazwaLeku = TempLek.NazwaLeku;
                lekItem.DawkowanieRano = TempLek.DawkowanieRano;
                lekItem.DawkowaniePoludnie = TempLek.DawkowaniePoludnie;
                lekItem.DawkowaniePopoludnie = TempLek.DawkowaniePopoludnie;
                lekItem.DawkowanieWieczor = TempLek.DawkowanieWieczor;
                lekItem.Ilosc = TempLek.Ilosc;
                lekItem.Wydruk = TempLek.Wydruk;
            }
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxResult result = MessageBox.Show("Lek " + TempLek.NazwaLeku + " został zaktualizowany.", "Aktualizacja Leku", buttons);
            mwInstance.LekiLista = this.LekiLista;
            this.Close();
        }
    }
}
