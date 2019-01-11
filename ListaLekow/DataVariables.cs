using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ListaLekow
{
    [Serializable]
    public class DataVariables
    {

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

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string Adres { get; set; }

        public DataVariables(ObservableCollection<Lek> lekiLista, string imie, string nazwisko, string pesel, string adres)
        {
            LekiLista = lekiLista;
            Imie = imie;
            Nazwisko = nazwisko;
            Pesel = pesel;
            Adres = adres;
        }
        
    }
}
