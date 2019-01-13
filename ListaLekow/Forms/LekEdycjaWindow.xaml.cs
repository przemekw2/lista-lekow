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
using System.Collections.ObjectModel;

namespace ListaLekow
{
    /// <summary>
    /// Interaction logic for LekEdycjaWindow.xaml
    /// </summary>
    public partial class LekEdycjaWindow : Window
    {
        MainWindow mwInstance = (MainWindow)Application.Current.MainWindow;
        public Lek TempLek { get; set; }
        private Lek lekClone;
        private ObservableCollection<Lek> lekiLista;

        public ObservableCollection<Lek> LekiLista
        {
            get
            {
                if (this.lekiLista == null)
                {
                    this.lekiLista = new ObservableCollection<Lek>();
                }
                return this.lekiLista;
            }
            set { this.lekiLista = value; }
        }

        public LekEdycjaWindow(Lek tempLek, ObservableCollection<Lek> lekiLista)
        {
            InitializeComponent();
            //TempLek = tempLek;
            lekClone = (Lek)tempLek.Clone();
            LekiLista = lekiLista;
            this.DataContext = lekClone;
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            //zamkniecie okna
            this.Close();
        }

        private void UptdateBTN_Click(object sender, RoutedEventArgs e)
        {
            //aktualizacja leku

            if (!CheckForDuplicate(lekClone.NazwaLeku, lekClone.ID))
            {

                for (int i = 0; i <= LekiLista.Count; i++)
                {
                    if (LekiLista[i].ID == lekClone.ID)
                    {
                        LekiLista[i] = (Lek)lekClone.Clone();
                        mwInstance.dataGrid1.ItemsSource = LekiLista;
                        break;
                    }
                }

                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxResult result = MessageBox.Show("Lek został zaktualizowany.", "Aktualizacja Leku", buttons);
                this.Close();
            }
            else
            {
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxResult result = MessageBox.Show("Lek o nazwie " + lekClone.NazwaLeku + " już istnieje na liście.", "Aktualizacja Leku", buttons);
            }
            
        }

        private bool CheckForDuplicate(string nazwa_leku, string guid)
        {
            try
            {
                Lek lek = this.LekiLista.Single(x => x.NazwaLeku == nazwa_leku && x.ID != guid);
                return true;
            }
            catch (System.InvalidOperationException ex)
            {
                return false;
            }
        }
    }
}
